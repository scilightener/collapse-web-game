using XProtocol.Serializator;

namespace XProtocol.XPackets
{
    public class XPacketMove
    {
        [XField(1)]
        public int X;

        [XField(2)]
        public int Y;
    }
}
