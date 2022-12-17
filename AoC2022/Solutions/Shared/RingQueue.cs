namespace Solutions.Shared;

public class RingQueue<T>
{
    private readonly Queue<T> _queue;

    public RingQueue()
    {
        _queue = new Queue<T>();
    }

    public RingQueue(IEnumerable<T> items)
    {
        _queue = new Queue<T>(items);
    }

    public T Dequeue()
    {
        var item = _queue.Dequeue();
        _queue.Enqueue(item);
        return item;
    }

    public void Enqueue(T item) => _queue.Enqueue(item);
}