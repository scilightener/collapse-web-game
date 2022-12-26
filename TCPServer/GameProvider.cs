using System.Drawing;
using GameLogic;

namespace TCPServer;

public class GameProvider
{
    private readonly Board _board;
    private readonly Player[] _players;
    private int _movesCount = 0;
    public GameProvider(int rows, int columns, params Player[] players)
    {
        _board = new Board(rows, columns, players);
        _players = players;
    }

    internal bool MakeMove(int playerId, int x, int y)
    {
        var currentPlayer = _players[_movesCount % 2];
        if (currentPlayer.Id != playerId) return false;
        _movesCount++;
        return _board.MakeMove(currentPlayer, x, y);
    }

    public bool IsGameEnded => _board.Status == GameStatus.Ended;

    public int GetWinnerId() =>
        IsGameEnded ? _players.First(p => _board.GetPlayerStatus(p) == PlayerStatus.Winner).Id : -1;

    public static Color GetColorForPlayer(int id) => id switch
    {
        0 => Color.Yellow,
        1 => Color.Green,
        2 => Color.Blue,
        3 => Color.Aqua,
        4 => Color.Red,
        _ => Color.White,
    };
}
