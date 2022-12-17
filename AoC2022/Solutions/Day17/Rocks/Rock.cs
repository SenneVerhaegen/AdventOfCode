namespace Solutions.Day17.Rocks;

public abstract class Rock
{
    public int X1 { get; set; }
    public int Y1 { get; set; }

    public int X2 { get; set; }
    public int Y2 { get; set; }

    private (int, int, int, int) _oldPosition;

    public Rock(int yBottom)
    {
        X1 = 2;
        Y2 = yBottom;
    }

    public abstract List<(int, int)> GetPositions();

    public void RestorePosition()
    {
        X1 = _oldPosition.Item1;
        X2 = _oldPosition.Item2;
        Y1 = _oldPosition.Item3;
        Y2 = _oldPosition.Item4;
    }

    private void SavePosition() => _oldPosition = (X1, X2, Y1, Y2);

    public void MoveHorizontal(char direction)
    {
        SavePosition();
        if (direction == '<')
            MoveLeft();
        else if (direction == '>')
            MoveRight();
        else
            throw new ArgumentOutOfRangeException($"'{direction}' is not a valid direction");
    }

    private void MoveLeft()
    {
        if (X1 == 0) return;
        X1--;
        X2--;
    }

    private void MoveRight()
    {
        if (X2 == 6) return;
        X1++;
        X2++;
    }

    public void MoveDown()
    {
        SavePosition();
        Y1--;
        Y2--;
    }
}