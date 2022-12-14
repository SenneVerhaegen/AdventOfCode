namespace Solutions.Day13;

public class Part1 : Solution
{
    private readonly int _sum;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var solutions = new List<bool>();

        var input = Util.GetInput(13, useTestInput).ToList();

        for (var i = 0; i < input.Count; i += 3)
        {
            var packetLeft = input[i];
            packetLeft = packetLeft.Substring(1, packetLeft.Length - 2);

            var packetRight = input[i + 1];
            packetRight = packetRight.Substring(1, packetRight.Length - 2);

            var left = CreateTree(packetLeft);
            var right = CreateTree(packetRight);

            switch (left.CompareTo(right))
            {
                case -1:
                case 0:
                    solutions.Add(true);
                    break;
                case 1:
                    solutions.Add(false);
                    break;
            }
        }

        for (var i = 0; i < solutions.Count; i++)
            if (solutions[i])
                _sum += i + 1;
    }

    private static Node CreateTree(string content)
    {
        var root = new Node();
        var depth = 0;

        for (var charIndex = 0; charIndex < content.Length;)
        {
            var c = content[charIndex];
            if (c == '[')
            {
                var node = new Node();
                root.AddChild(node, depth++);
                charIndex++;
            }
            else if (c == ']')
            {
                depth--;
                charIndex++;
            }
            else if (c == ',')
                charIndex++;
            else
            {
                var valueStr = string.Join("",
                    content.Substring(charIndex).TakeWhile(x => x is not ',' and not ']' and not '['));
                var value = int.Parse(valueStr);
                var node = new Node(value);
                root.AddChild(node, depth);

                charIndex += valueStr.Length;
            }
        }

        return root;
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"Sum {_sum}");
    }
}