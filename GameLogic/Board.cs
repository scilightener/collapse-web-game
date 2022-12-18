namespace GameLogic;

using static PlayerStatus;

public class Board
{
    public int Rows => _rows;
    public int Columns => _columns;
    
    private readonly int _rows;
    private readonly int _columns;
    private readonly Cell[,] _cells;
    private readonly Dictionary<int, int> _playersCellsCount = new();
    private readonly Dictionary<int, PlayerStatus> _playersGameStatus = new();
    private readonly HashSet<Cell> _onChange = new();

    public Board(int rows, int columns, params Player[] players)
    {
        _rows = rows;
        _columns = columns;
        _cells = new Cell[rows, columns];
        for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
                _cells[i, j] = new Cell(i, j);
        foreach (var player in players)
        {
            _playersCellsCount.Add(player.Id, 0);
            _playersGameStatus.Add(player.Id, Unknown);
        }
    }

    public PlayerStatus GetPlayerStatus(Player player) =>
        _playersGameStatus.TryGetValue(player.Id, out var status) ? status : NotExists;

    // Returns whether it's possible for player to make the move
    // And if so, updates the board according to the game's rules
    public bool MakeMove(Player player, int x, int y)
    {
        if (0 > x || x >= _cells.GetLength(0) ||
            0 > y || y >= _cells.GetLength(1) ||
            GetPlayerStatus(player) is not Unknown)
            return false;
        var cell = _cells[x, y];
        if (player.MovesCount == 0)
        {
            player.MakeMove();
            return cell.Owner is null && UpdateCell(3, x, y, player);
        }

        if ((cell.Owner?.Id ?? -1) != player.Id)
            return false;
        UpdateCell(1, x, y, player);
        while (!IsBoardOk()) Update();
        return true;
    }

    private void Update()
    {
        foreach (var cell in _cells)
        {
            if (cell.CountPoints < 4 || !_onChange.Contains(cell)) continue;
            UpdateCell(1, cell.X - 1, cell.Y, cell.Owner!);
            UpdateCell(1, cell.X + 1, cell.Y, cell.Owner!);
            UpdateCell(1, cell.X, cell.Y - 1, cell.Owner!);
            UpdateCell(1, cell.X, cell.Y + 1, cell.Owner!);
            cell.ResetCountPoints();
            // _onChange.Remove(cell); no need
        }
        _onChange.Clear();
        // Thread.Sleep(500); maybe for the drawing latter
    }

    private bool UpdateCell(int count, int x, int y, Player initiator)
    {
        if (0 > x || x >= _cells.GetLength(0) ||
            0 > y || y >= _cells.GetLength(1))
            return false;
        var cell = _cells[x, y];
        if (cell.Owner is null)
            _playersCellsCount[initiator.Id]++;
        else if (cell.Owner != initiator)
        {
            _playersCellsCount[cell.Owner.Id]--;
            if (_playersCellsCount[cell.Owner.Id] == 0)
                _playersGameStatus[cell.Owner.Id] = Looser;
        }
        cell.AddCountPoints(count, initiator);
        if (cell.CountPoints >= 4) _onChange.Add(cell);
        return true;
    }

    private bool IsBoardOk() => _onChange.Count == 0;

    // public int this[int x, int y] => _cells[x, y].CountPoints;
    public Cell this[int x, int y] => _cells[x, y]; // only for console coloring; should be replaced with the one above
}