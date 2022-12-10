namespace Solutions.Day1;

public class Part2 : Solution
{
    private readonly int _increases;

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var measurements = Util.GetInput(1, useTestInput).ToList();

        var m = new Dictionary<int, int>();
        var window = 0;

        for(var i = 0; i < measurements.Count - 2; i++)
        {
            var m1 = int.Parse(measurements[i]);
            var m2 = int.Parse(measurements[i + 1]);
            var m3 = int.Parse(measurements[i + 2]);

            m[window] = m1 + m2 + m3;
            window++;
        }

        var previous = m[0];
        foreach (var windowedMeasurement in m.Values)
        {
            if (windowedMeasurement > previous)
                _increases++;

            previous = windowedMeasurement;
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"The depth increased {_increases} times");
    }
}