namespace Solutions.Day17.Rocks;

public class LRock :Rock
{
    public LRock(int yBottom) : base(yBottom)
    {
        X2 = X1 + 2;
        Y1 = yBottom + 2;
    }

    public override List<(int, int)> GetPositions()
    {
        return new List<(int, int)>
        {
            (X2, Y1),
            (X2, Y1 - 1),
            (X1, Y2),
            (X1 + 1, Y2),
            (X2, Y2),
        };
    }
}