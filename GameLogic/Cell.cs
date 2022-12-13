namespace GameLogic;

public class Cell
{
    private int _countPoints;
    private Player _owner;
    public int CountPoints {
        get
        {
            return this._countPoints; 
        } 
    }
    public Player Owner
    {
        get
        {
            return this._owner;
        }
    }

    public void AddCountPoints() => _countPoint = _countPoints++;

    public void ResetCountPoints() => _countPoints = 0;

    public void ChangeOwner(Player owner) => _owner = owner;
}