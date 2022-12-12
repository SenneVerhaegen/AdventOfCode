namespace Solutions.Day12;

public class State
{
    public int Row { get; }
    public int Col { get; }
    public int Steps { get; }

    public State(int row, int col)
    {
        Row = row;
        Col = col;
    }

    private State(int row, int col, int steps)
    {
        Row = row;
        Col = col;
        Steps = steps;
    }

    public State MoveUp() => new(Row - 1, Col, Steps + 1);
    public State MoveDown() => new(Row + 1, Col, Steps + 1);
    public State MoveLeft() => new(Row, Col - 1, Steps + 1);
    public State MoveRight() => new(Row, Col + 1, Steps + 1);

    public override string ToString()
    {
        return $"({Row},{Col}) in {Steps}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not State other) return false;

        return Row == other.Row &&
               Col == other.Col;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Col);
    }
}