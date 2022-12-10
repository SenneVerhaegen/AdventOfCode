using System.Diagnostics;

namespace Solutions;

public abstract class Solution
{
    private readonly Stopwatch _sw;

    protected Solution(bool useTestInput)
    {
        _sw = new Stopwatch();
        _sw.Start();
    }

    public virtual void PrintResult()
    {
        PrintElapsedTime();
    }

    private void PrintElapsedTime()
    {
        _sw.Stop();
        Console.WriteLine($"Program finished in {_sw.ElapsedMilliseconds} ms");
    }
}