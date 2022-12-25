﻿using GameLogic;
using System.Net.Sockets;
using XProtocol;
using XProtocol.Serializer;
using XProtocol.XPackets;

namespace TCPServer
{
    internal class ConnectedClient
    {
        private readonly XServer _server;
        private Socket Client { get; }
        private Player Player { get; }

        private readonly Queue<byte[]> _packetSendingQueue = new();

        public ConnectedClient(Socket client, XServer server, Player player)
        {
            Client = client;
            _server = server;
            Player = player;

            Task.Run(ProcessIncomingPackets);
            Task.Run(SendPackets);
        }

        private void ProcessIncomingPackets()
        {
            while (true) // Слушаем пакеты, пока клиент не отключится.
            {
                if (!Client.Connected) Console.WriteLine("client disconnected");
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
            var successfulRegistration = new XPacketSuccessfulRegistration
            {
                Id = Player.Id
            };
            //TODO: логика добавления игрока в игру

            QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.SuccessfulRegistration, successfulRegistration).ToPacket());
            if (_server.clients.Count > 1)
                foreach (var client in _server.clients)
                    client.QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.StartGame, new XPacketStartGame()).ToPacket());
            Console.WriteLine($"Received Handshake from {Player.Id}");
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
            Console.WriteLine($"Received Move from {Player.Id} with {move.X}, {move.Y}");
            QueuePacketSend(XPacketConverter.Serialize(XPacketType.MoveResult, moveResult).ToPacket());
        }

        //Done Send Pause to opponent
        private void ProcessPause(XPacket packet)
        {
            var pause = XPacketConverter.Deserialize<XPacketPause>(packet);
            var opponent = _server.clients.FirstOrDefault(c => c.Player.Id != Player.Id);
            if (opponent == null) throw new NullReferenceException("Opponent not found");

            opponent.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Pause, pause).ToPacket());
        }

        //Done Send Pause Ended to opponent
        private void ProcessPauseEnded(XPacket packet)
        {
            var pauseEnded = XPacketConverter.Deserialize<XPacketPauseEnded>(packet);

            var opponent = _server.clients.FirstOrDefault(c => c.Player.Id != Player.Id);
            if (opponent == null) throw new NullReferenceException("Opponent not found");

            opponent.QueuePacketSend(XPacketConverter.Serialize(XPacketType.PauseEnded, pauseEnded).ToPacket());
        }

        private void EndGameForAllPlayers(int id)
        {
            foreach (var client in _server.clients)
                client.QueuePacketSend(XPacketConverter
                    .Serialize(XPacketType.Winner, new XPacketWinner { IdWinner = id }).ToPacket());
        }

        private void QueuePacketSend(byte[] packet)
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
            Client.Disconnect(true);
        }
    }
}
