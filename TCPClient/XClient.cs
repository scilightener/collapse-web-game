using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using XProtocol.Serializator;
using XProtocol;
using XProtocol.XPackets;

namespace TCPClient
{
    public class XClient
    {
        public Action<byte[]> OnPacketReceive { get; set; }

        private readonly Queue<byte[]> _packetSendingQueue = new Queue<byte[]>();

        private Socket _socket;
        private IPEndPoint _serverEndPoint;
        
        public void Connect(string ip, int port)
        {
            Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        }

        public void Connect(IPEndPoint server)
        {
            _serverEndPoint = server;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(_serverEndPoint);

            Task.Run((Action) RecievePackets);
            Task.Run((Action) SendPackets);
        }

        public void QueuePacketSend(byte[] packet)
        {
            if (packet.Length > 256)
            {
                throw new Exception("Max packet size is 256 bytes.");
            }

            _packetSendingQueue.Enqueue(packet);
        }

        private void RecievePackets()
        {
            while (true)
            {
                var buff = new byte[256];
                _socket.Receive(buff);

                buff = buff.TakeWhile((b, i) =>
                {
                    if (b != 0xFF) return true;
                    return buff[i + 1] != 0;
                }).Concat(new byte[] {0xFF, 0}).ToArray();

                OnPacketReceive?.Invoke(buff);
            }
        }

        private async void SendPackets()
        {
            while (true)
            {
                if (_packetSendingQueue.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }

                var packet = _packetSendingQueue.Dequeue();
                _socket.Send(packet);

                Thread.Sleep(100);
            }
        }

        //private void OnPacketRecieve(byte[] packet)
        //{
        //    var parsed = XPacket.Parse(packet);

        //    if (parsed != null)
        //    {
        //        ProcessIncomingPacket(parsed);
        //    }
        //}

        //private void ProcessIncomingPacket(XPacket packet)
        //{
        //    var type = XPacketTypeManager.GetTypeFromPacket(packet);

        //    switch (type)
        //    {
        //        case XPacketType.SuccessfulRegistration:
        //            ProcessSuccessfulRegistration(packet);
        //            break;
        //        case XPacketType.StartGame:
        //            ProcessStartGame(packet);
        //            break;
        //        case XPacketType.MoveResult:
        //            ProcessMoveResult(packet);
        //            break;
        //        case XPacketType.Pause:
        //            ProcessPause(packet);
        //            break;
        //        case XPacketType.PauseEnded:
        //            ProcessPauseEnded(packet);
        //            break;
        //        case XPacketType.Winner:
        //            ProcessWinner(packet);
        //            break;
        //        case XPacketType.Unknown:
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        //private void ProcessSuccessfulRegistration(XPacket packet)
        //{
        //    var successfulRegistration = XPacketConverter.Deserialize<XPacketSuccessfulRegistration>(packet);
        //    //TODO: go to game

        //}

        //private void ProcessStartGame(XPacket packet)
        //{
        //    var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

        //    QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        //}

        //private void ProcessMoveResult(XPacket packet)
        //{
        //    var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

        //    QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        //}

        //private void ProcessPause(XPacket packet)
        //{
        //    var pause = XPacketConverter.Deserialize<XPacketPause>(packet);
        //    //TODO: Pause game
        //}

        //private void ProcessPauseEnded(XPacket packet)
        //{
        //    var pauseEnded = XPacketConverter.Deserialize<XPacketPauseEnded>(packet);
        //    //TODO: Unpaused game
        //}

        //private void ProcessWinner(XPacket packet)
        //{
        //    var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

        //    QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        //}
    }
}
