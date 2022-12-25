using XProtocol.Serializer;
using XProtocol;

namespace TCPClient
{
    internal class Program
    {
        private static int _handshakeMagic;

        private static void Main()
        {
            Console.Title = "XClient";
            Console.ForegroundColor = ConsoleColor.White;

            var client = new XClient();
            //client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);

            Thread.Sleep(1000);
            
            Console.WriteLine("Sending handshake packet..");

            client.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.Handshake,
                    new XPacketHandshake
                    {
                    })
                    .ToPacket());

            while(true) {}
        }

        
    }
}
