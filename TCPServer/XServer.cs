using GameLogic;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    internal class XServer
    {
        private readonly Socket _socket;
        internal readonly List<ConnectedClient> clients;
        internal GameProvider gp;

        private bool _listening;
        private bool _stopListening;

        public XServer()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<ConnectedClient>();
        }

        public void Start()
        {
            if (_listening)
            {
                throw new Exception("Server is already listening incoming requests.");
            }

            _socket.Bind(new IPEndPoint(IPAddress.Any, 4910));
            _socket.Listen(10);

            _listening = true;
        }

        public void Stop()
        {
            if (!_listening)
            {
                throw new Exception("Server is already not listening incoming requests.");
            }

            _stopListening = true;
                
            _listening = false;
        }

        public void AcceptClients()
        {
            while (true)
            {
                if (_stopListening)
                {
                    return;
                }

                Socket client;

                try
                {
                    client = _socket.Accept();
                } catch { return; }

                Console.WriteLine($"[!] Accepted client from {client.RemoteEndPoint as IPEndPoint}");

                var id = clients.Count;
                var color = GameProvider.GetColorForPlayer(id);
                var player = new Player(id, $"Player{id}", color);

                var c = new ConnectedClient(client, this, player);
                clients.Add(c);
            }
        }
    }
}
