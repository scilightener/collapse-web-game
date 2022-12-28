using GameLogic;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    internal class XServer : IDisposable
    {
        internal readonly List<ConnectedClient> Clients;
        internal GameProvider Gp = null!;
        private int _currentId;

        private bool _listening;
        private bool _stopListening;
        private readonly Socket _socket;

        public XServer()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Clients = new List<ConnectedClient>();
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
                var c = new ConnectedClient(client, this, CreateNewPlayer());
                Clients.Add(c);
            }
        }

        private Player CreateNewPlayer()
        {
            var id = _currentId++;
            var color = GameProvider.GetColorForPlayer(id);
            var player = new Player(id, $"Player{id}", color);

            if (_currentId == 2)
                Gp = new GameProvider(5, 5, Clients.Select(c => c.Player).Concat(new[] { player }).ToArray());
            return player;
        }

        public void Dispose()
        {
            Stop();
            _socket.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
