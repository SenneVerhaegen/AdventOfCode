using System.Numerics;

namespace Solutions.Day11;

public class Part1 : Solution
{
    private readonly BigInteger _monkeyBusiness;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(11, useTestInput).ToList();

        var monkeys = new List<Monkey>();

        var startingItemStart = "  Starting items: ".Length;
        var divisibleByStart = "  Test: divisible by ".Length;
        var monkey1Start = "   If true: throw to monkey ".Length;
        var monkey2Start = "    If false: throw to monkey ".Length;

        for (var i = 0; i < input.Count; i += 7)
        {
            var startingItems = input[i + 1]
                .Substring(startingItemStart)
                .Split(", ")
                .Select(ulong.Parse);

            var operation = ParseOperation(input[i + 2]);
            var testValue = int.Parse(input[i + 3].Substring(divisibleByStart));
            var trueMonkey = int.Parse(input[i + 4].Substring(monkey1Start));
            var falseMonkey = int.Parse(input[i + 5].Substring(monkey2Start));

            var monkey = new Monkey(startingItems, operation, testValue, trueMonkey, falseMonkey);
            monkeys.Add(monkey);
        }

        var divisor = monkeys.Aggregate(1, (a, m) => a * m.TestValue);

        for (var round = 1; round <= 20; round++)
        {
            foreach (var monkey in monkeys)
            {
                var count = monkey.ItemCount();
                for (var i = 0; i < count; i++)
                {
                    monkey.InspectPart1(divisor);
                    var receiver = monkeys[monkey.GetReceiverIndex()];
                    receiver.ReceiveItem(monkey.ThrowItem());
                }
            }
        }

        var topInspections = monkeys.Select(m => m.Inspections).OrderDescending().Take(2).ToArray();

        _monkeyBusiness = new BigInteger(topInspections[0]) * new BigInteger(topInspections[1]);
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"Monkey business {_monkeyBusiness}");
    }

    private static Func<ulong, ulong> ParseOperation(string line)
    {
        var operationStart = "  Operation: new = ".Length;
        var parts = line.Substring(operationStart).Split(" ");
        var mathOperator = parts[1];

        if (parts[2] == "old")
            return old => old * old;

        var value = ulong.Parse(parts[2]);

        return mathOperator switch
        {
            "*" => old => old * value,
            "+" => old => old + value,
            "-" => old => old - value,
            "/" => old => old / value,
            _ => throw new ArgumentOutOfRangeException(nameof(mathOperator))
        };
    }
}