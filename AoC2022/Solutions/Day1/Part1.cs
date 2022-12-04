namespace Solutions.Day1;

public class Part1 : ISolution
{
    private int _maxCalories;

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

        _maxCalories = elvesWithCalories.Max();
    }

    public void PrintResult()
    {
        Console.WriteLine($"The elf carrying the most calories is carrying {_maxCalories} calories");
    }
}