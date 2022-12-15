namespace Solutions.Day15;

public class Part1 : Solution

{
    private readonly int _count;
    private readonly int _targetY;
    private int _counter;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(15, useTestInput);

        _targetY = useTestInput ? 10 : 2000000;

        var targetBeacon = new Point(2, 10);
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
            var w = 9000000;
            for (var x = -w; x < w; x++)
            {
                var p = new Point(x, _targetY);
                var d2 = sensor.DistanceTo(p);
                if (d2 <= d1)
                    points.Add(p);
            }
        }

        _count = points.Count - 1;
    }

    private IEnumerable<Point> GetPointsWithinDistance(Point beacon, int distance)
    {
        Console.WriteLine($"Distance: {distance}");
        var points = new List<Point>();
        for (var x = 0; x <= distance; x++)
        {
            for (var y = 0; y <= distance - x; y++)
            {
                if (beacon.Y + y == _targetY)
                {
                    points.Add(new Point(beacon.X + x, beacon.Y + y));
                    points.Add(new Point(beacon.X - x, beacon.Y + y));
                }
                else if (beacon.Y - y == _targetY)
                {
                    points.Add(new Point(beacon.X + x, beacon.Y - y));
                    points.Add(new Point(beacon.X - x, beacon.Y - y));
                }
            }
        }

        return points;
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"In row y=10, {_count} positions cannot contain a beacon");
        Console.WriteLine(_counter + 1);
    }
}