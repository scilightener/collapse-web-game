using System.Drawing;

namespace GameLogic;

public class Player
{
    public int Id => _id;
    public string Name { get; }

    public Color Color { get; }

    public int MovesCount { get; private set; }

    private readonly int _id;

    public Player(int id, string name, Color color)
    {
        _id = id;
        Name = name;
        Color = color;
    }

    public void MakeMove() => MovesCount++;
}