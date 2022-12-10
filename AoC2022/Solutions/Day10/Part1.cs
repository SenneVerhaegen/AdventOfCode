namespace Solutions.Day10;

public class Part1 : Solution
{
    private readonly CrtPart1 _crt = new();

    public Part1(bool useTestInput) : base(useTestInput)
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

            if (_crt.InterestingSignalStrengthsMeasured())
                return;
        }
    }


    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"Sum of signal strengths: {_crt.SummedSignalStrength}");
    }
}