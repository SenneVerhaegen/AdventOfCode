namespace Solutions.Day4;

public class Part1 : ISolution
{
    private int _numberOfContainedRanges;

    public void Run(bool useTestInput)
    {
        var sectionAssignments = Util.GetInput(4, useTestInput);

        foreach (var pair in sectionAssignments)
        {
            var pairParts = pair.Split(",");
            var part1 = pairParts[0];
            var part2 = pairParts[1];

            if (FullyContainsSection(part1, part2))
                _numberOfContainedRanges++;
        }
    }

    private static bool FullyContainsSection(string section1, string section2)
    {
        if (section1 == section2) return true;

        var section1Bounds = section1.Split("-").Select(int.Parse).ToList();
        var section2Bounds = section2.Split("-").Select(int.Parse).ToList();

        var section1LowerBound = section1Bounds[0];
        var section1UpperBound = section1Bounds[1];
        var section2LowerBound = section2Bounds[0];
        var section2UpperBound = section2Bounds[1];

        // Section 2 fully contains section 1
        if (section1LowerBound <= section2UpperBound &&
            section1UpperBound <= section2UpperBound &&
            section1LowerBound >= section2LowerBound)
            return true;

        // Section 1 fully contains section 2
        if (section1LowerBound <= section2LowerBound &&
            section1UpperBound >= section2UpperBound)
            return true;

        return false;
    }

    public void PrintResult()
    {
        Console.WriteLine($"Number of contained ranges: {_numberOfContainedRanges}");
    }
}