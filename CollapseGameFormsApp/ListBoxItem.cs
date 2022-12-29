public class ListBoxItem
{
    public ListBoxItem(Color c, string m, int id)
    {
        ItemColor = c;
        Message = m;
        _id = id;
    }
    public Color ItemColor { get; set; }

    public string Message { get; set; }

    private int _id;

    public void SetMove(bool move) => Message = move ? $"Player{_id} - your move" : $"Player{_id}";
}