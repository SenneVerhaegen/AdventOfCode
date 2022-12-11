using System.Numerics;

namespace Solutions.Day11;

public class Monkey
{
    public int Inspections { get; private set; }
    public readonly int TestValue;

    private readonly Queue<Item> _items = new();
    private readonly Func<ulong, ulong> _operation;
    private readonly int _trueMonkey;
    private readonly int _falseMonkey;

    public Monkey(IEnumerable<ulong> startingItems, Func<ulong, ulong> operation, int testValue, int trueMonkey,
        int falseMonkey)
    {
        foreach (var worryLevel in startingItems)
            _items.Enqueue(new Item { WorryLevel = worryLevel });

        TestValue = testValue;

        _operation = operation;
        _trueMonkey = trueMonkey;
        _falseMonkey = falseMonkey;
    }

    public void InspectPart1(int divisor)
    {
        if (_items.Count == 0) return;

        ExecuteOperation(divisor);
        DivideWorryLevel();

        Inspections++;
    }

    public void InspectPart2(int divisor)
    {
        if (_items.Count == 0) return;

        ExecuteOperation(divisor);

        Inspections++;
    }

    private void ExecuteOperation(int divisor)
    {
        var previous = _items.Peek();
        var newValue = _operation.Invoke(previous.WorryLevel);
        previous.WorryLevel = newValue % (ulong)divisor;
    }

    private bool IsPrime()
    {
        return _items.Peek().WorryLevel % (ulong)TestValue == 0;
    }

    private void DivideWorryLevel()
    {
        _items.Peek().WorryLevel /= 3;
    }

    public void ReceiveItem(Item item)
    {
        _items.Enqueue(item);
    }

    public Item ThrowItem()
    {
        return _items.Dequeue();
    }

    public int GetReceiverIndex()
    {
        return IsPrime() ? _trueMonkey : _falseMonkey;
    }

    public int ItemCount() => _items.Count;

    public void PrintItems()
    {
        if (_items.Count == 0) return;
        foreach (var item in _items)
        {
            Console.Write($"{item.WorryLevel}, ");
        }

        Console.WriteLine();
    }
}