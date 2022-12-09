namespace Solutions.Day1;

public class Part2 : Solution
{
    private readonly int _topThreeCalories;

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var lines = Util.GetInput(1, useTestInput);

        var elvesWithCalories = new List<int> { 0 };

        foreach (var calories in lines)
        {
            if (calories == "")
                elvesWithCalories.Add(0);
            else
                elvesWithCalories[^1] += int.Parse(calories);
        }

        _topThreeCalories = elvesWithCalories.OrderDescending().Take(3).Sum();
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"The top three elves are carrying {_topThreeCalories} calories.");
    }
}