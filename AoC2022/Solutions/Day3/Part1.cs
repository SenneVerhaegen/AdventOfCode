namespace Solutions.Day3;

public class Part1 : ISolution
{
    private readonly Dictionary<char, int> _priorities = new(26 * 2);
    private int _priority;

    public void Run(bool useTestInput)
    {
        InitPriorityMappings();

        var rugsacks = Util.GetInput(3, useTestInput);
        foreach (var rugsack in rugsacks)
        {
            var compartment1 = rugsack.Substring(0, rugsack.Length / 2);
            var compartment2 = rugsack.Substring(compartment1.Length, rugsack.Length / 2);

            var commonItem = compartment1.First(item => compartment2.Contains(item));
            IncreasePriority(commonItem);
        }
    }

    private void InitPriorityMappings()
    {
        const string lowercase = "abcdefghijklmnopqrstuvwxyz";
        for (var i = 0; i < lowercase.Length; i++)
        {
            var key = lowercase[i];
            _priorities[key] = i + 1;
        }

        const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (var i = 0; i < uppercase.Length; i++)
        {
            var key = uppercase[i];
            _priorities[key] = i + 1 + 26;
        }
    }

    private void IncreasePriority(char item)
    {
        _priority += _priorities[item];
    }

    public void PrintResult()
    {
        Console.WriteLine($"Priority: {_priority}");
    }
}