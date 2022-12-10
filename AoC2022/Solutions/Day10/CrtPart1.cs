namespace Solutions.Day10;

public class CrtPart1
{
    private int _x = 1;
    private int _cycle;

    private readonly Queue<int> _measurePoints = new();
    public int SummedSignalStrength { get; private set; }

    public CrtPart1()
    {
        _measurePoints.Enqueue(20);
        _measurePoints.Enqueue(60);
        _measurePoints.Enqueue(100);
        _measurePoints.Enqueue(140);
        _measurePoints.Enqueue(180);
        _measurePoints.Enqueue(220);
    }

    public void NoOp()
    {
        if (_cycle == _measurePoints.Peek())
        {
            SummedSignalStrength += SignalStrength();
            _measurePoints.Dequeue();
        }

        _cycle++;
    }

    public void AddX(int value)
    {
        for (var i = 0; i < 2; i++)
        {
            _cycle++;
            
            if (_cycle == _measurePoints.Peek())
            {
                SummedSignalStrength += SignalStrength();
                _measurePoints.Dequeue();
            }
        }

        _x += value;
    }

    private int SignalStrength()
    {
        return _cycle * _x;
    }

    public bool InterestingSignalStrengthsMeasured() => _measurePoints.Count == 0;
    

    public void Draw()
    {
        
    }
}