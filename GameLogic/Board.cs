namespace GameLogic;

using static PlayerStatus;
using static GameStatus;

public class Board
{
    public int Rows => _rows;
    public int Columns => _columns;
    public GameStatus Status { get; private set; } = Started;
    public Action UpdateUI { get; set; } = () => { };

    private readonly int _rows;
    private readonly int _columns;
    private bool _isBoardOk;
    private readonly Cell[,] _cells;
    private readonly Dictionary<int, int> _playersOwnedCells = new(); // id -> cells count
    private readonly Dictionary<int, PlayerStatus> _playersGameStatus = new(); // id -> status
    
    // set of cells that have been changed during this update
    // they should not be considered to update again during this iteration
    private readonly HashSet<Cell> _changedCells = new();
    private readonly HashSet<Player> _currentPlayers = new();

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
            _playersOwnedCells.Add(player.Id, 0);
            _playersGameStatus.Add(player.Id, Unknown);
            _currentPlayers.Add(player);
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
            !_currentPlayers.Contains(player) ||
            Status == Ended)
            return false;
        var cell = _cells[x, y];
        if (player.MovesCount == 0 && cell.Owner is null && UpdateCell(3, x, y, player))
        {
            player.MakeMove();
            Update();
            return true;
        }
        
        if ((cell.Owner?.Id ?? -1) != player.Id)
            return false;
        UpdateCell(1, x, y, player);
        while (!_isBoardOk)
            Update();
        _isBoardOk = false;
        return true;
    }

    private void Update()
    {
        foreach (var cell in _cells)
        {
            if (cell.CountPoints < 4 || _changedCells.Contains(cell)) continue;
            UpdateCell(1, cell.X - 1, cell.Y, cell.Owner!);
            UpdateCell(1, cell.X + 1, cell.Y, cell.Owner!);
            UpdateCell(1, cell.X, cell.Y - 1, cell.Owner!);
            UpdateCell(1, cell.X, cell.Y + 1, cell.Owner!);
            _playersOwnedCells[cell.Owner!.Id]--;
            cell.ResetCountPoints();
        }

        _isBoardOk = _changedCells.Count == 0;
        _changedCells.Clear();
        UpdateUI.Invoke();
    }

    private bool UpdateCell(int count, int x, int y, Player initiator)
    {
        void UpdatePlayersInfo(Cell cell, Player player)
        {
            if (cell.Owner is null)
                _playersOwnedCells[player.Id]++;
            else if (cell.Owner != player)
            {
                _playersOwnedCells[cell.Owner.Id]--;
                _playersOwnedCells[initiator.Id]++;
                if (_playersOwnedCells[cell.Owner.Id] == 0)
                {
                    _playersGameStatus[cell.Owner.Id] = Looser;
                    _currentPlayers.Remove(cell.Owner);
                    if (_currentPlayers.Count == 1)
                    {
                        _playersGameStatus[_currentPlayers.First().Id] = Winner;
                        Status = Ended;
                    }
                }
            }
        }
        
        if (0 > x || x >= _cells.GetLength(0) ||
            0 > y || y >= _cells.GetLength(1))
            return false;
        UpdatePlayersInfo(_cells[x, y], initiator);
        _cells[x, y].AddCountPoints(count, initiator);
        _changedCells.Add(_cells[x, y]);
        return true;
    }

    // public int this[int x, int y] => _cells[x, y].CountPoints;
    public Cell this[int x, int y] => _cells[x, y]; // only for console coloring; should be replaced with the one above
}