namespace Solutions.Day13;

public class Part2 : Solution
{
    private readonly int _decoderKey = 1;
    public Part2(bool useTestInput) : base(useTestInput)
    {
        var packets = new SortedSet<Node>();
        
        var input = Util.GetInput(13, useTestInput).ToList();

        for (var i = 0; i < input.Count ; i += 3)
        {
            var packetLeft = input[i];
            packetLeft = packetLeft.Substring(1, packetLeft.Length - 2);

            var packetRight = input[i + 1];
            packetRight = packetRight.Substring(1, packetRight.Length - 2);

            var left = CreateTree(packetLeft);
            var right = CreateTree(packetRight);

            packets.Add(left);
            packets.Add(right);
        }

        var sp1 = StartPacket1();
        var sp2 = StartPacket2();
        packets.Add(sp1);
        packets.Add(sp2);

        var packetsList = packets.ToList();
        for (var i = 0; i < packetsList.Count; i++)
        {
            var packet = packetsList[i].ToString();
            
            if (sp1.ToString() == packet)
                _decoderKey *= i+1;
            if (sp2.ToString() == packet)
                _decoderKey *= i+1;
        }
    }

    private static Node StartPacket1()
    {
        var node = new Node();
        var n = new Node();
        n.AddChild(2, 0);
        node.AddChild(n,0);
        return node;
    }
    
    private static Node StartPacket2()
    {
        var node = new Node();
        var n = new Node();
        n.AddChild(6, 0);
        node.AddChild(n,0);
        return node;
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

        Console.WriteLine($"Decoder key: {_decoderKey}");
    }
}