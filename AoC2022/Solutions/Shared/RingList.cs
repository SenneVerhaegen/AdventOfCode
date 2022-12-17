namespace Solutions.Shared;

public class RingList<T>
{
    public int Position { get; private set; }
    private readonly LinkedList<T> _list;
    private readonly int _resetIndex;

    public RingList(IEnumerable<T> items)
    {
        var collection = items.ToList();
        _list = new LinkedList<T>(collection);
        _resetIndex = collection.Count - 1;
    }

    public T Next()
    {
        var item = _list.First.Value;
        _list.RemoveFirst();
        _list.AddLast(item);
        Position++;

        if (Position > _resetIndex)
            Position = 0;

        return item;
    }

    // public void Enqueue(T item) => _list.Enqueue(item);
}