namespace Solutions.Day15;

public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int DistanceTo(Point other)
    {
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var item = obj as Point;
        return X == item.X && Y == item.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}