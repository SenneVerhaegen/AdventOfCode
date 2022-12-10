namespace Solutions.Day1;

public class Part1 : Solution
{
    private int _increases;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var measurements = Util.GetInput(1, useTestInput).ToList();
        
        var previous = int.Parse(measurements[0]);

        foreach (var measurement in measurements.Select(int.Parse))
        {
            if (measurement > previous)
                _increases++;

            previous = measurement;
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"The depth increased {_increases} times");
    }
}