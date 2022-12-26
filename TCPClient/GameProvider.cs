using System.Drawing;
using GameLogic;

namespace TCPClient;

public class GameProvider
{
    private readonly Board _board;
    private readonly Player[] _players;
    private int _movesCount;
    public GameProvider(int rows, int columns, params Player[] players)
    {
        _board = new Board(rows, columns, players);
        _players = players;
        _movesCount = _players[0].Id;
    }

    public bool MakeMove(int playerId, int x, int y)
    {
        var currentPlayer = _players[(_movesCount + 1) % 2];
        if (currentPlayer.Id != playerId) return false;
        _movesCount++;
        return _board.MakeMove(currentPlayer, x, y);
    }

    public bool IsGameEnded => _board.Status == GameStatus.Ended;

    public static Color GetColorForPlayer(int id) => id switch
    {
        0 => Color.Yellow,
        1 => Color.Green,
        2 => Color.Blue,
        3 => Color.Brown,
        4 => Color.Red,
        _ => Color.White,
    };

    public Color GetColorByCoordinates(int x, int y) => _board[x, y].Owner?.Color ?? default;

    public int GetCountPointsByCoordinates(int x, int y) => _board[x, y].CountPoints;
}
