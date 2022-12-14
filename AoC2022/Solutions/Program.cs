namespace Solutions;

internal static class Program
{
    private static readonly Dictionary<(int, int), Func<bool, Solution>> Solutions = new()
    {
        { (1, 1), useTestInput => new Day1.Part1(useTestInput) },
        { (1, 2), useTestInput => new Day1.Part2(useTestInput) },
        
        { (2, 1), useTestInput => new Day2.Part1(useTestInput) },
        { (2, 2), useTestInput => new Day2.Part2(useTestInput) },
        
        { (3, 1), useTestInput => new Day3.Part1(useTestInput) },
        { (3, 2), useTestInput => new Day3.Part2(useTestInput) },
        
        { (4, 1), useTestInput => new Day4.Part1(useTestInput) },
        { (4, 2), useTestInput => new Day4.Part2(useTestInput) },
        
        { (5, 1), useTestInput => new Day5.Part1(useTestInput) },
        { (5, 2), useTestInput => new Day5.Part2(useTestInput) },
        
        { (6, 1), useTestInput => new Day6.Part1(useTestInput) },
        { (6, 2), useTestInput => new Day6.Part2(useTestInput) },
        
        { (7, 1), useTestInput => new Day7.Part1(useTestInput) },
        { (7, 2), useTestInput => new Day7.Part2(useTestInput) },
        
        { (8, 1), useTestInput => new Day8.Part1(useTestInput) },
        { (8, 2), useTestInput => new Day8.Part2(useTestInput) },
        
        { (9, 1), useTestInput => new Day9.Part1(useTestInput) },
        { (9, 2), useTestInput => new Day9.Part2(useTestInput) },
        
        { (10, 1), useTestInput => new Day10.Part1(useTestInput) },
        { (10, 2), useTestInput => new Day10.Part2(useTestInput) },
        
        { (11, 1), useTestInput => new Day11.Part1(useTestInput) },
        { (11, 2), useTestInput => new Day11.Part2(useTestInput) },
        
        { (12, 1), useTestInput => new Day12.Part1(useTestInput) },
        { (12, 2), useTestInput => new Day12.Part2(useTestInput) },
        
        { (13, 1), useTestInput => new Day13.Part1(useTestInput) },
        
        { (14, 1), useTestInput => new Day14.Part1(useTestInput) },
    };

    private static void Main(string[] args)
    {
        if (args.Length is not (2 or 3))
        {
            Console.WriteLine("To run the program use:\n" +
                              "dotnet run <day> <part>\n or dotnet run <day> <part> test" +
                              "For example, to run the solution for day 1 part 2: 'dotnet run 1 2' or 'dotnet run 1 2 test'");

            Environment.Exit(1);
        }

        var useTestInput = false;
        var day = int.Parse(args[0]);
        var part = int.Parse(args[1]);

        if (args.Length == 3 && args[2] == "test")
            useTestInput = true;

        var action = Solutions[(day, part)];
        var solution = action.Invoke(useTestInput);
        solution.PrintResult();
    }
}