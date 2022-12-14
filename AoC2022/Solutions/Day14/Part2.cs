namespace Solutions.Day14;

public class Part2 : Solution
{
    private readonly Cave2 _cave;
    public Part2(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(14, useTestInput).ToList();

        var tuples = string.Join(" -> ", input).Split("->").Select(x =>
        {
            var coordinates = x.Trim().Split(",").Select(int.Parse).ToList();
            return (coordinates[1], coordinates[0]);
        }).ToList();

        var minX = 0;
        var maxX = tuples.Select(t => t.Item2).Max() * 2;
        var maxY = tuples.Select(t => t.Item1).Max() + 2; // Add 2 for the new ground in part 2

        _cave = new Cave2(minX, maxX, maxY);

        foreach (var coordinates in input.Select(line => line.Split(" -> ").ToList()))
        {
            for (var i = 0; i < coordinates.Count - 1; i++)
            {
                var from = coordinates[i].Split(",").Select(int.Parse).ToList();
                var to = coordinates[i + 1].Split(",").Select(int.Parse).ToList();
                _cave.PlaceRocks(from[1], from[0], to[1], to[0]);
            }
        }

        // Place an 'infinite' line at the bottom
        _cave.PlaceRocks(maxY, minX, maxY, maxX - 1);

        _cave.FillWithSand();
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"{_cave.UnitsOfSandAtRest} units of sand have come to rest.");
    }
}