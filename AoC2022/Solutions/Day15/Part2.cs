namespace Solutions.Day15;

public class Part2 : Solution
{
    private readonly int _answer;

    // const int Max = 20;
    private const int Max = 4000000;
    private const int Min = 0;

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(15, useTestInput);

        var sensorDistances = new Dictionary<Point, int>();
        var sensors = new List<Point>();
        var possiblePoints = new List<Point>();

        var solutions = new HashSet<Point>();

        foreach (var line in input)
        {
            var parts = line.Split(" ").ToList();

            var sensorX = int.Parse(parts[2].Split("=")[1][..^1]);
            var sensorY = int.Parse(parts[3].Split("=")[1][..^1]);
            var sensor = new Point(sensorX, sensorY);

            var beaconX = int.Parse(parts[8].Split("=")[1][..^1]);
            var beaconY = int.Parse(parts[9].Split("=")[1]);
            var beacon = new Point(beaconX, beaconY);

            sensors.Add(sensor);

            // Distance to shortest beacon
            var d = sensor.DistanceTo(beacon);
            sensorDistances[sensor] = d;

            possiblePoints.AddRange(PointsAtDistance(sensor, d + 1));
        }


        foreach (var possiblePoint in possiblePoints)
        {
            foreach (var sensor in sensors)
            {
                var d1 = sensorDistances[sensor];
                var d2 = sensor.DistanceTo(possiblePoint);
                var diff = d2 - d1;

                if (diff == 1)
                    solutions.Add(possiblePoint);
            }
        }

        foreach (var possiblePoint in possiblePoints)
        {
            foreach (var sensor in sensors)
            {
                var d1 = sensorDistances[sensor];
                var d2 = sensor.DistanceTo(possiblePoint);
                var diff = d2 - d1;

                if (diff < 1)
                    solutions.Remove(possiblePoint);
            }
        }

        // Make sure the solutions are within bounds
        solutions.RemoveWhere(p => p.X is < Min or > Max || p.Y is < Min or > Max);

        foreach (var solution in solutions)
        {
            Console.WriteLine(solution);
        }

        Console.WriteLine($"Count {solutions.Count}");

        var sourceBeacon = solutions.First();
        _answer = sourceBeacon.X * 4000000 + sourceBeacon.Y;
    }

    private IEnumerable<Point> PointsAtDistance(Point point, int distance)
    {
        distance = Math.Min(distance, Max);

        Console.WriteLine($"Distance: {distance}");

        var points = new HashSet<Point>();

        var bound = point.X + distance;
        for (var x = point.X; x <= bound; x++)
        {
            var y = bound - x;

            points.Add(new Point(x, -y + point.Y));
            points.Add(new Point(x, y + point.Y));
        }

        for (var x = point.X; x >= 0; x--)
        {
            var y = -x;

            points.Add(new Point(x, -y + point.Y));
            points.Add(new Point(x, y + point.Y));
        }

        Console.WriteLine($"Points generated: {points.Count}");

        return points;
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine(_answer);
    }
}