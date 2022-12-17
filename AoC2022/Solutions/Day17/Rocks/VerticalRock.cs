namespace Solutions.Day17.Rocks;

public class VerticalRock : Rock
{
    public VerticalRock(int yBottom) : base(yBottom)
    {
        X2 = X1;
        Y1 = yBottom + 3;
    }

    public override List<(int, int)> GetPositions()
    {
        return new List<(int, int)>
        {
            (X1, Y1),
            (X1, Y1 - 1),
            (X1, Y1 - 2),
            (X1, Y2)
        };
    }
}