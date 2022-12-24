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
    {
        var currentPlayer = movesCount % 2 == 1 ? _players[0] : _players[1];
        if (currentPlayer.Id != playerID) return false;
        movesCount++;
        return _board.MakeMove(currentPlayer, x, y);
    }

    public bool IsGameEnded => _board.Status == GameStatus.Ended;

    public int GetWinnerId() => IsGameEnded ? _players.First(p => _board.GetPlayerStatus(p) == PlayerStatus.Winner).Id : -1;
}
