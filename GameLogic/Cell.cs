namespace GameLogic;

public class Cell
{
    public int CountPoints => _countPoints;
    public Player? Owner => _owner;

    private int _countPoints;
    private Player? _owner;

    public void AddCountPoints(int count) => _countPoints += count;

    public void ResetCountPoints() => _countPoints = 0;

    public void ChangeOwnerTo(Player owner) => _owner = owner;
}