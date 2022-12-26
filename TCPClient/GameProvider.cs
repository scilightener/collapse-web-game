using System.Drawing;
using GameLogic;

namespace TCPClient;

public class GameProvider
{
    private readonly Board _board;
    private readonly Player[] _players;
    private int _movesCount;
    public GameProvider(int rows, int columns, Action updateUI, params Player[] players)
    {
        _board = new Board(rows, columns, players);
        _players = players;
        _movesCount = _players[0].Id;
        _board.UpdateUI = updateUI;
    }

    public bool MakeMove(int playerId, int x, int y)
    {
        var currentPlayer = _players[_movesCount % 2];
        if (currentPlayer.Id != playerId) return false;
        if (_board.MakeMove(currentPlayer, x, y))
        {
            _movesCount++;
            return true;
        }
        return false;
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
