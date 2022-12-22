using XProtocol.Serializator;

namespace XProtocol.XPackets
{
    public class XPacketStartGame
    {
        [XField(1)]
        public bool GameStarted;
    }
}
