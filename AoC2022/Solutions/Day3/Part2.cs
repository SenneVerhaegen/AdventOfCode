namespace Solutions.Day3;

public class Part2 : ISolution
{
    private readonly Dictionary<char, int> _priorities = new(26 * 2);
    private int _priority;

    public void Run(bool useTestInput)
    {
        InitPriorityMappings();

        var rugsacks = Util.GetInput(3, useTestInput).ToList();

        for (var i = 0; i < rugsacks.Count; i += 3)
        {
            var r1 = rugsacks[i];
            var r2 = rugsacks[i + 1];
            var r3 = rugsacks[i + 2];

            var commonItem = r3.First(item => r1.Contains(item) && r2.Contains(item));
            IncreasePriority(commonItem);
        }
    }

    private void IncreasePriority(char item)
    {
        _priority += _priorities[item];
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

    public void PrintResult()
    {
        Console.WriteLine($"Priority: {_priority}");
    }
}