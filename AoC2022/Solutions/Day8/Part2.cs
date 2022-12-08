namespace Solutions.Day8;

public class Part2 : ISolution
{
    private int _width;
    private int _height;
    private List<string> _input;
    private Tree[,] _trees;
    private int _highestScenicScore = 0;

    public void Run(bool useTestInput)
    {
        _input = Util.GetInput(8, useTestInput).ToList();

        _width = _input[0].Length;
        _height = _input.Count();

        _trees = new Tree[_height, _width];

        MakeGrid();
        ConnectTrees();
        _highestScenicScore =  HighestScenicScore();
    }

    private int HighestScenicScore()
    {
        var highestScenicScore = 0;

        for (var row = 0; row < _height; row++)
        {
            for (var col = 0; col < _width; col++)
            {
                var score = _trees[row, col].ScenicScore();

                if (score > highestScenicScore)
                    highestScenicScore = score;
            }
        }

        return highestScenicScore;
    }

    private void ConnectTrees()
    {
        for (var row = 0; row < _height; row++)
        {
            for (var col = 0; col < _width; col++)
            {
                var tree = _trees[row, col];

                if (col < _width - 1)
                {
                    tree.AddNeighbor(_trees[row, col + 1], Tree.Direction.Right);
                }

                if (col > 0)
                {
                    tree.AddNeighbor(_trees[row, col - 1], Tree.Direction.Left);
                }

                if (row > 0)
                {
                    tree.AddNeighbor(_trees[row - 1, col], Tree.Direction.Up);
                }

                if (row < _height - 1)
                {
                    tree.AddNeighbor(_trees[row + 1, col], Tree.Direction.Down);
                }
            }
        }
    }

    private void MakeGrid()
    {
        for (var row = 0; row < _height; row++)
        {
            for (var col = 0; col < _width; col++)
            {
                var treeHeight = TreeHeight(row, col);
                var tree = new Tree(treeHeight);

                _trees[row, col] = tree;
            }
        }
    }

    private int TreeHeight(int row, int col)
    {
        return (int)char.GetNumericValue(_input[row][col]);
    }

    public void PrintResult()
    {
        Console.WriteLine($"Highest scenic score: {_highestScenicScore}");
    }
}