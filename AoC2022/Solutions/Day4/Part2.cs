namespace Solutions.Day4;

public class Part2 : Solution
{
    private int _numberOfContainedRanges;

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var sectionAssignments = Util.GetInput(4, useTestInput);

        foreach (var pair in sectionAssignments)
        {
            var pairParts = pair.Split(",");
            var part1 = pairParts[0];
            var part2 = pairParts[1];

            if (HasOverlap(part1, part2))
                _numberOfContainedRanges++;
        }
    }

    private static bool HasOverlap(string section1, string section2)
    {
        var section1Bounds = section1.Split("-").Select(int.Parse).ToList();
        var section2Bounds = section2.Split("-").Select(int.Parse).ToList();

        var section1LowerBound = section1Bounds[0];
        var section1UpperBound = section1Bounds[1];
        var section2LowerBound = section2Bounds[0];
        var section2UpperBound = section2Bounds[1];

        if (FullyContainsSection(section1LowerBound, section2UpperBound, section1UpperBound, section2LowerBound))
            return true;

        if (HasSlightOverlap(section1LowerBound, section2UpperBound, section1UpperBound, section2LowerBound))
            return true;

        return false;
    }

    private static bool FullyContainsSection(int section1LowerBound, int section2UpperBound, int section1UpperBound,
        int section2LowerBound)
    {
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

    private static bool HasSlightOverlap(int section1LowerBound, int section2UpperBound, int section1UpperBound,
        int section2LowerBound)
    {
        return
            (section1LowerBound < section2LowerBound && section1UpperBound >= section2LowerBound &&
             section1UpperBound < section2UpperBound) ||
            (section1LowerBound > section2LowerBound && section1LowerBound <= section2UpperBound &&
             section1UpperBound >= section2UpperBound);
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"Number of contained ranges: {_numberOfContainedRanges}");
    }
}