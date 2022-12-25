using System.Net.Sockets;
using XProtocol;
using XProtocol.Serializator;
using XProtocol.XPackets;

namespace TCPServer
{
    internal class ConnectedClient
    {
        private XServer _server;
        public Socket Client { get; }

        public int Id { get; set; }

        private readonly Queue<byte[]> _packetSendingQueue = new Queue<byte[]>();

        public ConnectedClient(Socket client, XServer server)
        {
            Client = client;
            _server = server;

            Task.Run(ProcessIncomingPackets);
            Task.Run(SendPackets);
        }

        private void ProcessIncomingPackets()
        {
            while (true) // Слушаем пакеты, пока клиент не отключится.
            {
                var buff = new byte[256]; // Максимальный размер пакета - 256 байт.
                Client.Receive(buff);

                buff = buff.TakeWhile((b, i) =>
                {
                    if (b != 0xFF) return true;
                    return buff[i + 1] != 0;
                }).Concat(new byte[] {0xFF, 0}).ToArray();

                var parsed = XPacket.Parse(buff);

                if (parsed != null)
                {
                    ProcessIncomingPacket(parsed);
                }
            }
        }

        private void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.Handshake:
                    ProcessHandshake(packet);
                    break;
                case XPacketType.Move:
                    ProcessMove(packet);
                    break;
                case XPacketType.Pause:
                    ProcessPause(packet);
                    break;
                case XPacketType.PauseEnded:
                    ProcessPauseEnded(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessHandshake(XPacket packet)
        {
            var successfulRegistration = new XPacketSuccessfulRegistration()
            {
                Id = _server._clients.Count
            };

            //TODO: логика добавления игрока в игру

            QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.SuccessfulRegistration, successfulRegistration).ToPacket());
            if (_server._clients.Count > 1)
                foreach (var client in _server._clients)
                    QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.StartGame, new XPacketStartGame()).ToPacket());
        }

        private void ProcessMove(XPacket packet)
        {
            var move = XPacketConverter.Deserialize<XPacketMove>(packet);

            //TODO: валидация хода
            var result = true; // Result validation and move

            var moveResult = new XPacketMoveResult()
            {
                Successful = result,
            };

            QueuePacketSend(XPacketConverter.Serialize(XPacketType.MoveResult, moveResult).ToPacket());
        }

        //Done Send Pause to opponent
        private void ProcessPause(XPacket packet)
        {
            var pause = XPacketConverter.Deserialize<XPacketPause>(packet);
            var opponent = _server._clients.FirstOrDefault(c => c.Id != Id);
            if (opponent == null) throw new NullReferenceException("Opponent not found");

            opponent.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Pause, pause).ToPacket());
        }

        //Done Send Pause Ended to opponent
        private void ProcessPauseEnded(XPacket packet)
        {
            var pauseEnded = XPacketConverter.Deserialize<XPacketPauseEnded>(packet);

            var opponent = _server._clients.FirstOrDefault(c => c.Id != Id);
            if (opponent == null) throw new NullReferenceException("Opponent not found");

            opponent.QueuePacketSend(XPacketConverter.Serialize(XPacketType.PauseEnded, pauseEnded).ToPacket());
        }

        public void QueuePacketSend(byte[] packet)
        {
            if (packet.Length > 256)
            {
                throw new Exception("Max packet size is 256 bytes.");
            }

            _packetSendingQueue.Enqueue(packet);
        }

        private void SendPackets()
        {
            while (true)
            {
                if (_packetSendingQueue.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }

                var packet = _packetSendingQueue.Dequeue();
                Client.Send(packet);

                Thread.Sleep(100);
            }
        }

        public void Disconnect()
        {
            Client.Shutdown(SocketShutdown.Both);
        }
    }
}
