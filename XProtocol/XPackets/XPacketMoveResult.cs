using XProtocol.Serializer;

namespace XProtocol.XPackets
{
    public class XPacketMoveResult
    {
        [XField(1)]
        public bool Successful;
    }
}
