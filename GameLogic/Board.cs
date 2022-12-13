using System.Data;
using System.Runtime.CompilerServices;

namespace GameLogic;

public class Board
{
    public int Rows => _rows;
    public int Columns => _columns;
    
    private int _rows;
    private int _columns;
    private Cell[,] _cells;

    public Board(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        _cells = new Cell[rows, columns];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                _cells[i, j] = new Cell();
    }

    public bool Update(Player player, int x, int y)
    {
        if (player.MovesCount == 0)
        {
            if (_cells[x, y].Owner is not null)
                return false;
            _cells[x, y].ChangeOwnerTo(player);
            _cells[x, y].AddCountPoints(3);
            return true;
        }
        if ((_cells[x, y].Owner?.Id ?? -1) != player.Id)
            return false;
        // TODO: add complex logic about changing the board according to the game rules
        return true;
    }

    public int this[int x, int y]
    {
        get => _cells[x, y].CountPoints;
    }
}