namespace Solutions.Day15;

public class Part1 : Solution

{
    private readonly int _count;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(15, useTestInput);

        var targetY = useTestInput ? 10 : 2_000_000;

        var points = new HashSet<Point>();

        foreach (var line in input)
        {
            var parts = line.Split(" ").ToList();

            var sensorX = int.Parse(parts[2].Split("=")[1][..^1]);
            var sensorY = int.Parse(parts[3].Split("=")[1][..^1]);
            var sensor = new Point(sensorX, sensorY);

            var beaconX = int.Parse(parts[8].Split("=")[1][..^1]);
            var beaconY = int.Parse(parts[9].Split("=")[1]);
            var beacon = new Point(beaconX, beaconY);

            var d1 = sensor.DistanceTo(beacon);
            const int w = 9_000_000;
            for (var x = -w; x < w; x++)
            {
                var p = new Point(x, targetY);
                var d2 = sensor.DistanceTo(p);
                if (d2 <= d1)
                    points.Add(p);
            }
        }

        _count = points.Count - 1;
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"In row y=10, {_count} positions cannot contain a beacon");
    }
}