namespace GameLogic;

public class Cell
{
    public int X { get; }
    public int Y { get; }
    public int CountPoints { get; private set; }

    public Player? Owner { get; private set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Parametrized with initiator to ensure that there will be no cells with points > 0 and no owner
    public void AddCountPoints(int count, Player initiator)
    {
        CountPoints += count;
        Owner = initiator;
    }

    public void ResetCountPoints() => CountPoints = Math.Max(CountPoints - 4, 0);
}