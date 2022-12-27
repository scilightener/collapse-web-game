namespace XProtocol
{
    public enum XPacketType
    {
        Unknown,
        Handshake,
        SuccessfulRegistration,
        StartGame,
        Move,
        MoveResult,
        Pause,
        PauseEnded,
        Winner,
        EndGame
    }
}
