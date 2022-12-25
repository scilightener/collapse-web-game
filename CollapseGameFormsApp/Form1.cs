using TCPClient;
using XProtocol.Serializator;
using XProtocol.XPackets;
using XProtocol;

namespace CollapseGameFormsApp
{
    public partial class Form1 : Form
    {
        private XClient client;
        private Button[] buttons;
        private Dictionary<Button, XPacketMove> packets;
        
        public Form1()
        {
            InitializeComponent();
            client = new XClient();
            InitializeButtons();
            foreach(var but in buttons!)
                but.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            client.Connect("127.0.0.1", 4910);
            client.OnPacketReceive = OnPacketRecieve;
            client.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.Handshake,
                    new XPacketHandshake
                    {
                    })
                    .ToPacket());
        }

        private void InitializeButtons()
        {
            buttons = new[] {
                button2, button3, button4, button5, button6,
                button7, button8, button9, button10, button11,
                button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21,
                button22, button23, button24, button25, button26
            };
        }

        private void OnClickGameField(int x, int y)
        {
            client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Move, new XPacketMove()
            {
                X= x, Y= y
            }).ToPacket());
        }


        private void OnPacketRecieve(byte[] packet)
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
            RunInUI(() =>
            {
                foreach (var button in buttons)
                    button.Visible = true;
            });
            
            //TODO: go to game

        }

        private void RunInUI(Action action) => BeginInvoke(action);

        private void ProcessStartGame(XPacket packet)
        {
            var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

        }

        private void ProcessMoveResult(XPacket packet)
        {
            var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

            client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        }

        private void ProcessPause(XPacket packet)
        {
            var pause = XPacketConverter.Deserialize<XPacketPause>(packet);
            //TODO: Pause game
        }

        private void ProcessPauseEnded(XPacket packet)
        {
            var pauseEnded = XPacketConverter.Deserialize<XPacketPauseEnded>(packet);
            //TODO: Unpaused game
        }

        private void ProcessWinner(XPacket packet)
        {
            var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

            client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        }

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
    }
}