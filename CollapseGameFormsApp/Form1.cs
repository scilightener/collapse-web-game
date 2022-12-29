using GameLogic;
using TCPClient;
using XProtocol.Serializer;
using XProtocol.XPackets;
using XProtocol;
using System.Resources;

namespace CollapseGameFormsApp
{
    public partial class Form1
    {
        private readonly XClient _client;
        private GameProvider _gp = null!;
        private Player _player = null!;
        private readonly List<Player> _players = new();
        
        public Form1()
        {
            InitializeComponent();
            _client = new XClient();
        }

        private static (int x, int y) GetButtonCoordinates(Control b) =>
            ((b.TabIndex - 1) / 5, (b.TabIndex - 1) % 5);

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;
            _client.Connect("127.0.0.1", 4910);
            _client.OnPacketReceive = OnPacketReceive;
            _client.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.Handshake,
                    new XPacketHandshake())
                    .ToPacket());
        }

        private void OnClickGameField(int x, int y)
        {
            var move = _gp.MakeMove(_player.Id, x, y);
            if (!move) return;
            _client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Move, new XPacketMove()
            {
                X = x, Y = y
            }).ToPacket());
            RunInUi(() =>
            {
                ((ListBoxItem)players.Items[0]).SetMove(false);
                ((ListBoxItem)players.Items[1]).SetMove(true);
                players.Refresh();
            });
        }

        private void OnPacketReceive(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);
            if (parsed != null) ProcessIncomingPacket(parsed);
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
                    ProcessStartGame();
                    break;
                case XPacketType.MoveResult:
                    ProcessMoveResult(packet);
                    break;
                case XPacketType.Move:
                    ProcessMove(packet);
                    break;
                case XPacketType.Pause:
                    ProcessPause();
                    break;
                case XPacketType.PauseEnded:
                    ProcessPauseEnded();
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
            RunInUi(() => Text = _player.Name);
        }

        private void RunInUi(Action action) => BeginInvoke(action);

        private void ProcessStartGame()
        {
            RunInUi(() =>
            {
                gameField.Visible = true;
                pause.Visible = true;
            });
            var opponentId = 1 - _player.Id;
            var opponent = new Player(opponentId, $"Player{opponentId}", GameProvider.GetColorForPlayer(opponentId));
            _players.Add(opponent);
            var updater = () =>
            {
                RunInUi(() =>
                {
                    foreach (var control in gameField.Controls)
                    {
                        if (control is not Button button) continue;
                        (int x, int y) = GetButtonCoordinates(button);
                        button.Image = GetImageByCoordinates(x, y);
                    }
                });
                Thread.Sleep(300);
            };
            _gp = new GameProvider(5, 5, updater, _player, opponent);
            RunInUi(() => 
            { 
                foreach (var player in _players)
                    players.Items.Add(new ListBoxItem(player.Color, player.Name, player.Id));
                if (_player.Id == _gp.WhoMoves())
                    ((ListBoxItem)players.Items[0]).SetMove(true);
                else ((ListBoxItem)players.Items[1]).SetMove(true);
                players.Refresh();
            });
        }

        private Image? GetImageByCoordinates(int x, int y)
        {
            var count = _gp.GetCountPointsByCoordinates(x, y);
            var color = _gp.GetColorByCoordinates(x, y);
            return count switch
            {
                1 when color == Color.Blue => Properties.Resources.point1_blue,
                1 when color == Color.Red => Properties.Resources.point1_red,
                2 when color == Color.Blue => Properties.Resources.point2_blue,
                2 when color == Color.Red => Properties.Resources.point2_red,
                3 when color == Color.Blue => Properties.Resources.point3_blue,
                3 when color == Color.Red => Properties.Resources.point3_red,
                _ => null,
            };
        }

        private void ProcessMoveResult(XPacket packet)
        {
            var moveResult = XPacketConverter.Deserialize<XPacketMoveResult>(packet);

            if (moveResult.Successful) return;
            RunInUi(() =>
            {
                gameField.Visible = false;
                pause.Visible = false;
                gameResultDialog.Visible = true;
                gameResultMessage.Text = "You are cheater! Blame on you!";
            });
            EndGame();
        }
        
        private void ProcessMove(XPacket packet)
        {
            var move = XPacketConverter.Deserialize<XPacketMove>(packet);
            _gp.MakeMove(_players.First(p => p.Id != _player.Id).Id, move.X, move.Y);
            RunInUi(() =>
            {
                ((ListBoxItem)players.Items[0]).SetMove(true);
                ((ListBoxItem)players.Items[1]).SetMove(false);
                players.Refresh();
            });
        }

        private void ProcessPause() =>
            RunInUi(() =>
            {
                gameField.Enabled = false;
                pause.Visible = false;
            });

        private void ProcessPauseEnded() =>
            RunInUi(() =>
            {
                gameField.Enabled = true;
                pause.Visible = true;
            });

        private void ProcessWinner(XPacket packet)
        {
            var winner = XPacketConverter.Deserialize<XPacketWinner>(packet);
            players.Items.Clear();
            RunInUi(() =>
            {
                gameField.Visible = false;
                pause.Visible = false;
                gameResultDialog.Visible = true;
                if (winner.IdWinner != _player.Id)
                    gameResultMessage.Text = _gp.IsGameEnded ? "You are looser! :(" : "You are cheater! Blame on you!";
                else gameResultMessage.Text = "Congratulations! You are winner!";
            });
        }

        private void EndGame()
        {
            if (_client.Connected)
                _client.QueuePacketSend(
                    XPacketConverter.Serialize(
                        XPacketType.EndGame,
                        new XPacketEndGame { PlayerId = _player.Id })
                        .ToPacket());
        }

        #region GameField buttons click events
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
        #endregion

        private void continueGame_Click(object sender, EventArgs e)
        {
            RunInUi(() =>
            {
                gameField.Enabled = true;
                pause.Visible = true;
                menu.Visible = false;
                
            });
            _client.QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.PauseEnded, new XPacketPauseEnded()).ToPacket());
        }

        private void pause_Click(object sender, EventArgs e)
        {
            RunInUi(() =>
            {
                gameField.Enabled = false;
                menu.Visible = true;
                pause.Visible = false;
            });
            _client.QueuePacketSend(XPacketConverter
                .Serialize(XPacketType.Pause, new XPacketPause()).ToPacket());
        }

        private void quitGame_Click(object sender, EventArgs e) => Dispose();

        private void players_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || players.Items.Count == 0) return;
            if (players.Items[e.Index] is not ListBoxItem item) return;
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(
                item.Message,
                players.Font,
                new SolidBrush(item.ItemColor),
                e.Bounds);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AnimateWindow(Handle, 500,
                AnimateWindowFlags.AW_BLEND |
                AnimateWindowFlags.AW_VER_POSITIVE);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(Handle, 500, AnimateWindowFlags.AW_BLEND | AnimateWindowFlags.AW_HIDE);
        }

        private void gameField_Enter(object sender, EventArgs e)
        {

        }
    }
}