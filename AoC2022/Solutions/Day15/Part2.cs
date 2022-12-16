namespace Solutions.Day15;

public class Part2 : Solution
{
    private readonly long _answer;

    private const int Min = 0;
    private readonly int _max;

    private readonly HashSet<Point> _sensorRangeEnclosingPoints = new();
    private readonly HashSet<Point> _possibleSolutions = new();
    private readonly Dictionary<Point, int> _sensorToBeaconDistances = new();
    private readonly List<Point> _sensors = new();

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(15, useTestInput);

        _max = useTestInput ? 20 : 4_000_000;

        Parse(input);
        AddPossibleSolutions();
        RemoveNonSolutions();

        var solution = _possibleSolutions.First();
        _answer = (long)(solution.X * 4_000_000) + solution.Y;
    }

    private void Parse(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(" ").ToList();

            var sensorX = int.Parse(parts[2].Split("=")[1][..^1]);
            var sensorY = int.Parse(parts[3].Split("=")[1][..^1]);
            var sensor = new Point(sensorX, sensorY);

            var beaconX = int.Parse(parts[8].Split("=")[1][..^1]);
            var beaconY = int.Parse(parts[9].Split("=")[1]);
            var beacon = new Point(beaconX, beaconY);

            _sensors.Add(sensor);

            // Distance to shortest beacon
            var d = sensor.DistanceTo(beacon);
            _sensorToBeaconDistances[sensor] = d;

            // Calculate the points right outside the boundary of the distance from sensor to its closest beacon
            // and add it to the possible solutions
            AddPointsAtDistance(sensor, d + 1);
        }
    }

    private void RemoveNonSolutions()
    {
        _possibleSolutions.RemoveWhere(p => _sensors.Any(sensor =>
        {
            var d1 = _sensorToBeaconDistances[sensor];
            var d2 = sensor.DistanceTo(p);
            var diff = d2 - d1;
            return diff < 1;
        }));

        // Make sure the solutions are within bounds
        _possibleSolutions.RemoveWhere(p => p.X < Min || p.X > _max || p.Y < Min || p.Y > _max);
    }

    private void AddPossibleSolutions()
    {
        foreach (var point in _sensorRangeEnclosingPoints)
        {
            foreach (var sensor in _sensors)
            {
                var d1 = _sensorToBeaconDistances[sensor];
                var d2 = sensor.DistanceTo(point);
                var diff = d2 - d1;

                if (diff == 1)
                    _possibleSolutions.Add(point);
            }
        }
    }

    private void AddPointsAtDistance(Point point, int distance)
    {
        Console.WriteLine($"Distance: {distance}");

        var bound = Math.Min(point.X + distance, _max);
        for (var x = point.X; x <= bound; x++)
        {
            var y = bound - x;

            _sensorRangeEnclosingPoints.Add(new Point(x, -y + point.Y));
            _sensorRangeEnclosingPoints.Add(new Point(x, y + point.Y));
        }

        for (var x = point.X; x >= 0; x--)
        {
            var y = -x;

            _sensorRangeEnclosingPoints.Add(new Point(x, -y + point.Y));
            _sensorRangeEnclosingPoints.Add(new Point(x, y + point.Y));
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine(_answer);
    }
}