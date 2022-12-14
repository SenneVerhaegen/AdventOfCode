namespace Solutions.Day13;

public class Part1 : Solution
{
    public Part1(bool useTestInput) : base(useTestInput)
    {
        var solutions = new List<bool>();

        var packets = new SortedSet<Node>();
        
        var input = Util.GetInput(13, useTestInput).ToList();

        for (var i = 0; i < input.Count ; i += 3)
        {
            Console.WriteLine($"Iteration {i / 3 + 1}");
            
            var l1 = input[i];
            l1 = l1.Substring(1, l1.Length - 2);

            var l2 = input[i + 1];
            l2 = l2.Substring(1, l2.Length - 2);
            
            Console.WriteLine($"Input 1: {l1}");
            Console.WriteLine($"Input 2: {l2}");

            var root1 = CreateTree(l1);
            var root2 = CreateTree(l2);

            packets.Add(root1);
            packets.Add(root2);

            var comparison = root1.CompareTo(root2);
            switch (comparison)
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

        var sum = 0;
        for (var i = 0; i < solutions.Count; i++)
        {
            var outcome = solutions[i] ? "RIGHT" : "NOT";
            if (solutions[i])
                sum += i + 1;
            
            Console.WriteLine($"Pair {i+1}: {outcome}");
        }

        Console.WriteLine($"Sum {sum}");

        var sp1 = StartPacket1();
        var sp2 = StartPacket2();
        packets.Add(sp1);
        packets.Add(sp2);

        var packetsList = packets.ToList();
        var index = 1;
        for (var i = 0; i < packetsList.Count; i++)
        {
            var packet = packetsList[i];
            if (sp1.ToString() == packet.ToString())
                index *= (i+1);
            if (sp2.ToString() == packet.ToString())
                index *= (i+1);
            
            // Console.WriteLine(packetsList[i]);
        }

        Console.WriteLine($"Answer: {index}");
    }

    private Node StartPacket1()
    {
        var node = new Node();
        var n = new Node();
        n.AddChild(2, 0);
        node.AddChild(n,0);
        return node;
    }
    
    private Node StartPacket2()
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
}