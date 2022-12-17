namespace Solutions.Day17.Rocks;

public class SquareRock : Rock
{
    public SquareRock(int yBottom) : base(yBottom)
    {
        X2 = X1 + 1;
        Y1 = yBottom + 1;
    }

    public override List<(int, int)> GetPositions()
    {
        return new List<(int, int)>
        {
            (X1, Y1),
            (X2, Y1),
            (X1, Y2),
            (X2, Y2)
        };
    }
}