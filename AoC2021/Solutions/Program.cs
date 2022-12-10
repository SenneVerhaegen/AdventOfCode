namespace Solutions;

internal static class Program
{
    private static readonly Dictionary<(int, int), Func<bool, Solution>> Solutions = new()
    {
        { (1, 1), useTestInput => new Day1.Part1(useTestInput) },
        { (1, 2), useTestInput => new Day1.Part2(useTestInput) },
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