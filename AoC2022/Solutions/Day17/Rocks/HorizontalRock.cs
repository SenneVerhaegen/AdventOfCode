using System.Text;

namespace Solutions.Day17.Rocks;

public class HorizontalRock : Rock
{
    public HorizontalRock(int yBottom) : base(yBottom)
    {
        X2 = 5;
        Y1 = yBottom;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < Part1.Width; i++)
        {
            if (i > X1 && i < X2)
                sb.Append("#");
            else
                sb.Append(".");
        }

        return sb.ToString();
    }

    public override List<(int, int)> GetPositions()
    {
        return new List<(int, int)>
        {
            (X1, Y1),
            (X1 + 1, Y1),
            (X1 + 2, Y1),
            (X2, Y1),
        };
    }
}