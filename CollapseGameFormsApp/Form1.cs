using TCPClient;
using XProtocol.Serializator;
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
            InitialiseButtons();
            foreach(var but in buttons)
                but.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
           foreach (var but in buttons)
                but.Visible = true;
            client.Connect("127.0.0.1", 4910);
            client.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.Handshake,
                    new XPacketHandshake
                    {
                    })
                    .ToPacket());
        }

        private void InitialiseButtons()
        {
            buttons = new[] {
                button2, button3, button4, button5, button6,
                button7, button8, button9, button10, button11,
                button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21,
                button22, button23, button24, button25, button26
            };
        }

        private void OnClickGameField()
        {
            client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Move, new XPacketMove()));
        }
    }
}