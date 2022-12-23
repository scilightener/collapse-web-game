using GameLogic;

namespace TCPServer;

public class GameProvider
{
    private Board _board;
    private Player[] _players;
    private int movesCount = 1;
    public GameProvider(int rows, int columns, params Player[] players)
    {
        _board = new Board(rows, columns, players);
        _players = players;
    }

    internal bool MakeMove(int playerID, int x, int y)
        => _board.MakeMove(_players.FirstOrDefault(p => p.Id == playerID), x, y);
}
