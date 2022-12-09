namespace Solutions.Day9;

public class Part2 : Solution
{
    private readonly List<Knot> _knots;
    private readonly Knot _last;

    private readonly HashSet<(int, int)> _visitedByTail = new();

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var moves = Util.GetInput(9, useTestInput);

        _knots = Enumerable
            .Range(0, 10)
            .Select(_ => new Knot(0, 0))
            .ToList();

        _last = _knots.Last();

        foreach (var move in moves)
        {
            var parts = move.Split(" ");
            var direction = parts[0][0];
            var steps = int.Parse(parts[1]);
            Move(direction, steps);
        }
    }

    private void Move(char direction, int steps)
    {
        var headMove = GetHeadMove(direction, _knots[0]);

        for (var i = 0; i < steps; i++)
        {
            headMove.Invoke();

            for (var j = 0; j < _knots.Count - 1; j++)
            {
                var head = _knots[j];
                var next = _knots[j + 1];

                var distX = head.X - next.X;
                var distY = head.Y - next.Y;

                MoveNext(distX, distY, next);
            }

            _visitedByTail.Add((_last.Y, _last.X));
        }
    }

    private static void MoveNext(int distX, int distY, Knot next)
    {
        if (TailCanStayStationary(distX, distY)) return;

        // Move horizontally
        if (distY == 0)
        {
            if (distX > 0) next.MoveRight();
            else next.MoveLeft();
        }

        // Move vertically
        else if (distX == 0)
        {
            if (distY > 0) next.MoveUp();
            else next.MoveDown();
        }

        // Move diagonally
        else
        {
            if (distY > 0) next.MoveUp();
            else next.MoveDown();

            if (distX > 0) next.MoveRight();
            else next.MoveLeft();
        }
    }

    private static Action GetHeadMove(char direction, Knot head)
    {
        return direction switch
        {
            'U' => head.MoveUp,
            'D' => head.MoveDown,
            'L' => head.MoveLeft,
            'R' => head.MoveRight,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private static bool TailCanStayStationary(int distX, int distY)
    {
        return Math.Abs(distX) <= 1 && Math.Abs(distY) <= 1;
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"Tail has visited {_visitedByTail.Count} unique positions.");
    }
}