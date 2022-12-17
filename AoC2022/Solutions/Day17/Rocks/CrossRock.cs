using System.Text;

namespace Solutions.Day17.Rocks;

public class CrossRock : Rock
{
    public CrossRock(int yBottom) : base(yBottom)
    {
        X2 = 4;
        Y1 = yBottom + 2;
    }

    public override List<(int, int)> GetPositions()
    {
        return new List<(int, int)>
        {
            (X1 + 1, Y1),
            (X1, Y1 - 1),
            (X1 + 1, Y1 - 1),
            (X2, Y1 - 1),
            (X1 + 1, Y2),
        };
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (var i = 0; i < Part1.Width; i++)
        {
            if (i == X1 + 1) sb.Append("#");
            else sb.Append(".");
        }

        sb.AppendLine();

        for (var i = 0; i < Part1.Width; i++)
        {
            if (i >= X1 && i <= X2)
                sb.Append("#");
            else
                sb.Append(".");
        }

        sb.AppendLine();

        for (var i = 0; i < Part1.Width; i++)
        {
            if (i == X1 + 1) sb.Append("#");
            else sb.Append(".");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}