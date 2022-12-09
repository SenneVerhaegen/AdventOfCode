namespace Solutions.Day8;

public class Part1 : Solution
{
    private readonly int _visibleTrees;
    private readonly int _width;
    private readonly int _height;
    private readonly List<string> _trees;

    public Part1(bool useTestInput) : base(useTestInput)
    {
        _trees = Util.GetInput(8, useTestInput).ToList();

        _width = _trees[0].Length;
        _height = _trees.Count;

        _visibleTrees += (2 * _width) + 2 * (_height - 2);

        for (var row = 1; row < _height - 1; row++)
        {
            for (var col = 1; col < _width - 1; col++)
            {
                var treeHeight = TreeHeight(row, col);

                if (!IsVisible(row, col, treeHeight))
                    continue;

                _visibleTrees++;
            }
        }
    }

    private bool IsVisible(int row, int col, int treeHeight)
    {
        return IsVisibleFromTheBottom(row, col, treeHeight) ||
               IsVisibleFromTheTop(row, col, treeHeight) ||
               IsVisibleFromTheLeft(row, col, treeHeight) ||
               IsVisibleFromTheRight(row, col, treeHeight);
    }

    private bool IsVisibleFromTheLeft(int row, int col, int treeHeight)
    {
        for (var x = 0; x < col; x++)
        {
            var height = TreeHeight(row, x);

            if (height >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsVisibleFromTheRight(int row, int col, int treeHeight)
    {
        for (var x = col + 1; x < _width; x++)
        {
            var height = TreeHeight(row, x);

            if (height >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsVisibleFromTheTop(int row, int col, int treeHeight)
    {
        for (var y = 0; y < row; y++)
        {
            var height = TreeHeight(y, col);

            if (height >= treeHeight)
            {
                return false;
            }
        }


        return true;
    }

    private bool IsVisibleFromTheBottom(int row, int col, int treeHeight)
    {
        for (var y = row + 1; y < _height; y++)
        {
            var height = TreeHeight(y, col);

            if (height >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    private int TreeHeight(int row, int col)
    {
        return (int)char.GetNumericValue(_trees[row][col]);
    }


    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"Visible trees: {_visibleTrees}");
    }
}