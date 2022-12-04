namespace Solutions.Day1;

public class Part2 : ISolution
{
    private int _topThreeCalories;

    public void Run(bool useTestInput)
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

    public void PrintResult()
    {
        Console.WriteLine($"The top three elves are carrying {_topThreeCalories} calories.");
    }
}