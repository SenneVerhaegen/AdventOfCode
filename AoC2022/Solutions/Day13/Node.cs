using System.Text;

namespace Solutions.Day13;

public class Node : IComparable<Node>
{
    public int Value { get; }
    
    private readonly bool _listNode;

    private readonly List<Node> _children = new();

    public Node(int value)
    {
        Value = value;
        _listNode = false;
    }

    public Node()
    {
        Value = -1;
        _listNode = true;
    }

    public void AddChild(Node node, int depth)
    {
        if (depth == 0)
            _children.Add(node);
        else
            _children.Last().AddChild(node, depth - 1);
    }

    public void AddChild(int value, int depth)
    {
        var childNode = new Node(value);
        AddChild(childNode, depth);
    }

    public int CompareTo(Node? right)
    {
        if (right == null) throw new ArgumentException();

        if (IsValueNode() && right.IsValueNode())
            return Value.CompareTo(right.Value);


        if (IsValueNode() && !right.IsValueNode())
        {
            var node = new Node();
            node.AddChild(Value, 0);
            return node.CompareTo(right);
        }

        if (!IsValueNode() && right.IsValueNode())
        {
            var node = new Node();
            node.AddChild(right.Value, 0);
            return this.CompareTo(node);
        }

        var bound = Math.Min(_children.Count, right._children.Count);
        for (var i = 0; i < bound; i++)
        {
            var comparison = _children[i].CompareTo(right._children[i]);

            if (comparison != 0)
                return comparison;
        }

        return _children.Count.CompareTo(right._children.Count);
    }

    private bool IsValueNode() => !_listNode;

    public override string ToString()
    {
        if (IsValueNode())
            return Value.ToString();

        var sb = new StringBuilder("[");
        foreach (var child in _children)
            sb.Append(child).Append(",");

        return sb.Append("]").ToString();
    }
}