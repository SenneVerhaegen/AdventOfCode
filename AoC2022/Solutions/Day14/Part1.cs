namespace Solutions.Day14;

public class Part1 : Solution
{
    private readonly Cave _cave;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(14, useTestInput).ToList();

        var tuples = string.Join(" -> ", input).Split("->").Select(x =>
        {
            var coordinates = x.Trim().Split(",").Select(int.Parse).ToList();
            return (coordinates[1], coordinates[0]);
        }).ToList();

        var minX = tuples.Select(t => t.Item2).Min();
        var maxX = tuples.Select(t => t.Item2).Max();
        var maxY = tuples.Select(t => t.Item1).Max();

        _cave = new Cave(minX, maxX, maxY);

        foreach (var coordinates in input.Select(line => line.Split(" -> ").ToList()))
        {
            for (var i = 0; i < coordinates.Count - 1; i++)
            {
                var from = coordinates[i].Split(",").Select(int.Parse).ToList();
                var to = coordinates[i + 1].Split(",").Select(int.Parse).ToList();
                _cave.PlaceRocks(from[1], from[0], to[1], to[0]);
            }
        }

        _cave.FillWithSand();
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"{_cave.UnitsOfSandAtRest} units of sand have come to rest before the sand starts flowing into the abyss.");
    }
}