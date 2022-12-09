namespace Solutions.Day5;

public class Part1 : Solution
{
    private string _message = "";
    private int _amountOfStacks;
    private readonly List<Stack<char>> _stacks = new();

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(5, useTestInput).ToList();

        var indexOfSeparator = input.ToList().IndexOf("");
        var crateLines = input.GetRange(0, indexOfSeparator - 1);
        var instructions = input.GetRange(indexOfSeparator + 1, input.Count - crateLines.Count - 2);

        _amountOfStacks = int.Parse(input[indexOfSeparator - 1].Split("  ").Last());

        InitializeStacks();
        SetInitialConfiguration(crateLines);
        ExecuteInstructions(instructions);

        _message = string.Concat(_stacks.Select(s => s.Pop()));
    }

    // Initialize the stacks data structure
    private void InitializeStacks()
    {
        for (var i = 0; i < _amountOfStacks; i++)
            _stacks.Add(new Stack<char>());
    }

    // Put the initial configuration of the crates in the stacks
    private void SetInitialConfiguration(List<string> crateLines)
    {
        crateLines.Reverse();

        foreach (var crateLine in crateLines)
        {
            for (var i = 0; i < _amountOfStacks; i++)
            {
                var crate = crateLine.Substring(i * 4, 3);

                if (string.IsNullOrWhiteSpace(crate))
                    continue;

                _stacks[i].Push(crate[1]);
            }
        }
    }

    private void ExecuteInstructions(List<string> instructions)
    {
        foreach (var instruction in instructions)
        {
            MoveCrates(instruction);

            // PrintStacks();
        }
    }

    private void MoveCrates(string instruction)
    {
        var parts = instruction.Split(" ");
        var amount = int.Parse(parts[1]);
        var from = int.Parse(parts[3]) - 1;
        var to = int.Parse(parts[5]) - 1;

        for (var i = 0; i < amount; i++)
        {
            var crate = _stacks[from].Pop();
            _stacks[to].Push(crate);
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"The message is: {_message}");
    }
    
    private void PrintStacks()
    {
        for (var i = 0; i < _stacks.Count; i++)
        {
            var stack = _stacks[i];
            Console.WriteLine($"Stack {i + 1}: ");

            foreach (var c in stack)
                Console.Write(c);

            Console.WriteLine();
        }
    }
}