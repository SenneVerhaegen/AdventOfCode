namespace Solutions;

internal static class Program
{
    private static readonly Dictionary<(int, int), ISolution> Solutions = new()
    {
        { (1, 1), new Day1.Part1() },
        { (1, 2), new Day1.Part2() },
        { (2, 1), new Day2.Part1() },
        { (2, 2), new Day2.Part2() },
        { (3, 1), new Day3.Part1() },
        { (3, 2), new Day3.Part2() },
        { (4, 1), new Day4.Part1() },
        { (4, 2), new Day4.Part2() },
        { (5, 1), new Day5.Part1() },
        { (5, 2), new Day5.Part2() },
        { (6, 1), new Day6.Part1() },
        { (6, 2), new Day6.Part2() },
        { (7, 1), new Day7.Part1() },
        { (7, 2), new Day7.Part2() },
        { (8, 1), new Day8.Part1() },
        { (8, 2), new Day8.Part2() },
        { (9, 1), new Day9.Part1() },
        { (9, 2), new Day9.Part2() },
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

        var test = false;
        var day = int.Parse(args[0]);
        var part = int.Parse(args[1]);

        if (args.Length == 3 && args[2] == "test")
            test = true;

        var solution = Solutions[(day, part)];
        solution.Run(test);
        solution.PrintResult();
    }
}