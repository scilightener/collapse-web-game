using XProtocol.Serializator;

namespace XProtocol.XPackets
{
    public class XPacketMoveResult
    {
        [XField(1)]
        public bool Successful;
    }
}
