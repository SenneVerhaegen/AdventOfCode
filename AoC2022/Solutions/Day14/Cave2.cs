using System.Text;

namespace Solutions.Day14;

public class Cave2
{
    private readonly char[,] _tiles;
    private readonly int _width;
    private readonly int _minX;
    private readonly int _height;

    private int _sandX;
    private int _sandY;

    public int UnitsOfSandAtRest { get; private set; }

    public Cave2(int minX, int maxX, int maxY)
    {
        _minX = minX;
        _width = maxX;
        _height = maxY + 1;
        _tiles = new char[_height, _width];

        PlaceSandSource();
    }

    private void PlaceSandSource()
    {
        _sandY = 0;
        _sandX = 500 - _minX;
        _tiles[_sandY, _sandX] = '+';
    }

    public void PlaceRocks(int y1, int x1, int y2, int x2)
    {
        // Vertical
        if (x1 == x2)
        {
            for (var y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
                PlaceRock(y, x1);
        }

        // Horizontal
        if (y1 == y2)
        {
            for (var x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
                PlaceRock(y1, x);
        }
    }

    private void PlaceRock(int y, int x)
    {
        var realX = x - _minX;
        _tiles[y, realX] = '#';
    }

    private bool IsBlocked(int y, int x) => _tiles[y, x] is '#' or 'o';

    private (int, int)? FindNextSandPosition(int y, int x)
    {
        while (y < _height - 1 && x < _width - 1 && !IsBlocked(y, x))
        {
            if (!IsBlocked(y + 1, x))
            {
                // The tile below is not blocked so we continue searching.
                y++;
                continue;
            }

            if (!IsBlocked(y + 1, x - 1))
            {
                // The tile diagonally left is not blocked so we continue searching.
                y++;
                x--;
                continue;
            }

            if (!IsBlocked(y + 1, x + 1))
            {
                // The tile diagonally right is not blocked so we continue searching.
                y++;
                x++;
                continue;
            }

            
            // At this point all directions are blocked
            // So we can place the unit of sand at the current position if it is not blocked
            if (!IsBlocked(y, x))
                return (y, x);
            
            // All directions are blocked and the current position is blocked as well.
            // We cannot place a unit of sand here.
            return null;
        }

        // If the conditions in the while loop are no longer satisfied,
        // it is possible we are out of bounds (width or height) meaning the sand will fall into the abyss.
        return null;
    }

    public void FillWithSand()
    {
        var pos = FindNextSandPosition(_sandY, _sandX);
        while (pos.HasValue)
        {
            PlaceSand(pos.Value.Item1, pos.Value.Item2);
            pos = FindNextSandPosition(_sandY, _sandX);
        }
    }

    private void PlaceSand(int y, int x)
    {
        _tiles[y, x] = 'o';
        UnitsOfSandAtRest++;
        _sandY = 0;
        _sandX = 500 - _minX;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var row = 0; row < _height; row++)
        {
            for (var col = 0; col < _width; col++)
            {
                if (col == 18 && row == 19)
                {
                    sb.Append("A");
                    continue;
                }

                var c = _tiles[row, col];
                sb.Append(c is not '#' and not '+' and not 'o' ? '.' : c);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}