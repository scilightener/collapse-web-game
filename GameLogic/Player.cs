using System.Drawing;

namespace GameLogic;

public class Player
{
    public int Id => _id;
    public string Name => _name;
    public Color Color => _color;
    public int MovesCount => _movesCount;

    private readonly string _name;
    private readonly int _id;
    private readonly Color _color;
    private int _movesCount;

    public Player(int id, string name, Color color)
    {
        _id = id;
        _name = name;
        _color = color;
    }
}