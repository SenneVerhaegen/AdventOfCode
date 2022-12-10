namespace Solutions.Day10;

public class CrtPart2
{
    private int _x = 1;
    private int _col;

    public void NoOp()
    {
        Draw();
    }

    public void AddX(int value)
    {
        Draw();
        Draw();
        _x += value;
    }

    private void Draw()
    {
        if (_col % 40 == 0)
        {
            Console.WriteLine();
            _col = 0;
        }

        var c = OverlapsWithCrt(_col) ? "#" : ".";

        _col++;

        Console.Write(c);
    }

    private bool OverlapsWithCrt(int horizontalPos)
    {
        return horizontalPos == _x ||
               horizontalPos == _x - 1 ||
               horizontalPos == _x + 1;
    }
}