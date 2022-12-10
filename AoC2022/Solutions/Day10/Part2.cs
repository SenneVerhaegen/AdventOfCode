namespace Solutions.Day10;

public class Part2 : Solution
{
    private readonly CrtPart2 _crt = new();

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var instructions = Util.GetInput(10, useTestInput);

        foreach (var instruction in instructions)
        {
            var parts = instruction.Split(" ");

            if (parts.Length == 1)
                _crt.NoOp();
            else
            {
                var value = int.Parse(parts[1]);
                _crt.AddX(value);
            }
        }
    }
}