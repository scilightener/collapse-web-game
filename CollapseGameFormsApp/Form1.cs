using GameLogic;
using TCPClient;
using XProtocol.Serializer;
using XProtocol.XPackets;
using XProtocol;

namespace CollapseGameFormsApp
{
    public partial class Form1 : Form
    {
        private readonly XClient _client;
        private Button[] _buttons;
        private GameProvider _gp;
        private Player _player;
        private List<Player> _players = new();
        
        public Form1()
        {
            InitializeComponent();
            InitializeButtons();
            _client = new XClient();
            foreach(var but in _buttons!)
                but.Visible = false;
        }

        private static (int x, int y) GetButtonCoordinates(Control b) => ((b.TabIndex - 1) / 5, (b.TabIndex - 1) % 5);

        private void UpdateGameBoard()
        {
            while (true)
            {
                RunInUI(() =>
                {
                    foreach (var button in _buttons)
                    {
                        var coords = GetButtonCoordinates(button);
                        button.Text = _gp.GetCountPointsByCoordinates(coords.x, coords.y).ToString();
                        button.BackColor = _gp.GetColorByCoordinates(coords.x, coords.y);
                    }
                });
                Thread.Sleep(500);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            _client.Connect("127.0.0.1", 4910);
            _client.OnPacketReceive = OnPacketReceive;
            _client.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.Handshake,
                    new XPacketHandshake())
                    .ToPacket());
        }

        private void InitializeButtons()
        {
            _buttons = new[] {
                button2, button3, button4, button5, button6,
                button7, button8, button9, button10, button11,
                button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21,
                button22, button23, button24, button25, button26
            };
        }

        private void OnClickGameField(int x, int y)
        {
            var move = _gp.MakeMove(_player.Id, x, y);
            if (!move) return; // TODO: handle this case
            _client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Move, new XPacketMove()
            {
                X = x, Y = y
            }).ToPacket());
        }


        private void OnPacketReceive(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);
            
            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.SuccessfulRegistration:
                    ProcessSuccessfulRegistration(packet);
                    break;
                case XPacketType.StartGame:
                    ProcessStartGame(packet);
                    break;
                case XPacketType.MoveResult:
                    ProcessMoveResult(packet);
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
                case XPacketType.Winner:
                    ProcessWinner(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessSuccessfulRegistration(XPacket packet)
        {
            var successfulRegistration = XPacketConverter.Deserialize<XPacketSuccessfulRegistration>(packet);
            var playerId = successfulRegistration.Id;
            _player = new Player(playerId, $"Player{playerId}", GameProvider.GetColorForPlayer(playerId));
            _players.Add(_player);
        }

        private void RunInUI(Action action) => BeginInvoke(action);

        private void ProcessStartGame(XPacket packet)
        {
            //this.BackgroundImage = 
            RunInUI(() =>
            {
                foreach (var button in _buttons)
                    button.Visible = true;
                button29.Visible = true;
            });
            var opponentId = 1 - _player.Id;
            var opponent = new Player(opponentId, $"Player{opponentId}", GameProvider.GetColorForPlayer(opponentId));
            _players.Add(opponent);
            var updater = () =>
            {
                RunInUI(() =>
                {
                    foreach (var button in _buttons)
                    {
                        var coords = GetButtonCoordinates(button);
                        button.Text = _gp.GetCountPointsByCoordinates(coords.x, coords.y).ToString();
                        button.BackColor = _gp.GetColorByCoordinates(coords.x, coords.y);
                    }
                });
                Thread.Sleep(750);
            };
            _gp = new GameProvider(5, 5, updater, _player, opponent);
            //Task.Run(UpdateGameBoard);
        }

        private void ProcessMoveResult(XPacket packet)
        {
            var moveResult = XPacketConverter.Deserialize<XPacketMoveResult>(packet);

            if (!moveResult.Successful); // TODO: handle this case
        }
        
        private void ProcessMove(XPacket packet)
        {
            var move = XPacketConverter.Deserialize<XPacketMove>(packet);
            _gp.MakeMove(_players.First(p => p.Id != _player.Id).Id, move.X, move.Y);
        }

        private void ProcessPause(XPacket packet)
        {
            var pause = XPacketConverter.Deserialize<XPacketPause>(packet);
            RunInUI(() =>
            {
                foreach (var button in _buttons)
                    button.Visible = false;
                button29.Visible = false;
            });
        }

        private void ProcessPauseEnded(XPacket packet)
        {
            var pauseEnded = XPacketConverter.Deserialize<XPacketPauseEnded>(packet);
            RunInUI(() =>
            {
                foreach (var button in _buttons)
                    button.Visible = true;
                button29.Visible = true;
            });
        }

        private void ProcessWinner(XPacket packet)
        {
            var winner = XPacketConverter.Deserialize<XPacketWinner>(packet);
            if (winner.IdWinner != _player.Id)
                if (_gp.IsGameEnded) ; // TODO: looser
                else ; // TODO: cheater
            else ;// TODO: winner
        }

        private void button2_Hover(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) => OnClickGameField(0, 0);

        private void button3_Click(object sender, EventArgs e) => OnClickGameField(0, 1);

        private void button4_Click(object sender, EventArgs e) => OnClickGameField(0, 2);

        private void button5_Click(object sender, EventArgs e) => OnClickGameField(0, 3);

        private void button6_Click(object sender, EventArgs e) => OnClickGameField(0, 4);

        private void button7_Click(object sender, EventArgs e) => OnClickGameField(1, 0);

        private void button8_Click(object sender, EventArgs e) => OnClickGameField(1, 1);

        private void button9_Click(object sender, EventArgs e) => OnClickGameField(1, 2);

        private void button10_Click(object sender, EventArgs e) => OnClickGameField(1, 3);

        private void button11_Click(object sender, EventArgs e) => OnClickGameField(1, 4);

        private void button12_Click(object sender, EventArgs e) => OnClickGameField(2, 0);

        private void button13_Click(object sender, EventArgs e) => OnClickGameField(2, 1);

        private void button14_Click(object sender, EventArgs e) => OnClickGameField(2, 2);

        private void button15_Click(object sender, EventArgs e) => OnClickGameField(2, 3);

        private void button16_Click(object sender, EventArgs e) => OnClickGameField(2, 4);

        private void button17_Click(object sender, EventArgs e) => OnClickGameField(3, 0);

        private void button18_Click(object sender, EventArgs e) => OnClickGameField(3, 1);

        private void button19_Click(object sender, EventArgs e) => OnClickGameField(3, 2);

        private void button20_Click(object sender, EventArgs e) => OnClickGameField(3, 3);

        private void button21_Click(object sender, EventArgs e) => OnClickGameField(3, 4);

        private void button22_Click(object sender, EventArgs e) => OnClickGameField(4, 0);

        private void button23_Click(object sender, EventArgs e) => OnClickGameField(4, 1);

        private void button24_Click(object sender, EventArgs e) => OnClickGameField(4, 2);

        private void button25_Click(object sender, EventArgs e) => OnClickGameField(4, 3);

        private void button26_Click(object sender, EventArgs e) => OnClickGameField(4, 4);

        private void button27_Click(object sender, EventArgs e)
        {
            RunInUI(() =>
            {
                foreach (var button in _buttons)
                    button.Visible = true;
                button27.Visible = false;
                button28.Visible = false;
                button29.Visible = true;
            });
            _client.QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.PauseEnded, new XPacketPauseEnded()).ToPacket());
        }

        private void button29_Click(object sender, EventArgs e)
        {
            RunInUI(() =>
            {
                foreach (var button in _buttons)
                    button.Visible = false;
                button27.Visible = true;
                button28.Visible = true;
                button29.Visible = false;
            });
            _client.QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.Pause, new XPacketPause()).ToPacket());
        }

        private void button28_Click(object sender, EventArgs e)
        {

        }
    }
}