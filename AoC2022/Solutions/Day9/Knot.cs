namespace Solutions.Day9;

public class Knot
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Knot(int x, int y)
    {
        Y = y;
        X = x;
    }

    public void MoveUp() => Y++;

    public void MoveDown() => Y--;

    public void MoveLeft() => X--;

    public void MoveRight() => X++;
}