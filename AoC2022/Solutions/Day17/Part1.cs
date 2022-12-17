using Solutions.Day17.Rocks;
using Solutions.Shared;

namespace Solutions.Day17;

public class Part1 : Solution
{
    public static int Width = 7;

    private int _maxY = -1;
    private int _nbrOfBlocks;
    private int _maxNbrOfBlocks;
    private RingQueue<int> _blockTypes = new();
    private readonly RingQueue<char> _jets;
    private readonly string[,] _grid = new string[4000, 7];

    // Positions (x, y)
    private readonly HashSet<(int, int)> _blockedPositions = new();

    public Part1(bool useTestInput) : base(useTestInput)
    {
        Helpers.InitializeHelperGrid(_grid);

        var input = Util.GetInput(17, useTestInput).ToList()[0];
        _jets = new RingQueue<char>(input);

        SetupRockTypes();
        AddBottomLayer();

        while (_nbrOfBlocks < 2022)
        {
            var rock = NextRock();

            List<(int, int)> positions = rock.GetPositions();
            // Helpers.PrintStep(_grid, positions, _maxY);

            while (true)
            {
                var direction = _jets.Dequeue();
                rock.MoveHorizontal(direction);
                positions = rock.GetPositions();

                if (positions.Any(p => _blockedPositions.Contains(p)))
                {
                    rock.RestorePosition();
                    // positions = rock.GetPositions();
                    // Helpers.PrintStep(_grid, positions, _maxY);
                }
                // else
                    // Helpers.PrintStep(_grid, positions, _maxY);

                rock.MoveDown();
                positions = rock.GetPositions();

                if (positions.Any(p => _blockedPositions.Contains(p)))
                    break;

                // Helpers.PrintStep(_grid, positions, _maxY);
            }

            rock.RestorePosition();
            positions = rock.GetPositions();
            Helpers.PrintStep(_grid, positions, _maxY, true, false);

            foreach (var position in positions)
                _blockedPositions.Add(position);

            _maxY = Math.Max(rock.Y1, _maxY);
            _nbrOfBlocks++;
        }
    }

    private void AddBottomLayer()
    {
        for (var x = 0; x < 7; x++)
            _blockedPositions.Add((x, -1));
    }

    private void SetupRockTypes()
    {
        for (var i = 1; i <= 5; i++)
            _blockTypes.Enqueue(i);
    }


    private Rock NextRock()
    {
        var rockType = _blockTypes.Dequeue();
        var yBottom = _maxY + 4;

        Rock rock = rockType switch
        {
            1 => new HorizontalRock(yBottom),
            2 => new CrossRock(yBottom),
            3 => new LRock(yBottom),
            4 => new VerticalRock(yBottom),
            5 => new SquareRock(yBottom),
            _ => throw new ArgumentOutOfRangeException()
        };

        return rock;
    }

    public override void PrintResult()
    {
        base.PrintResult();
        // Helpers.Print(_grid, 1000);

        Console.WriteLine($"Tower is {_maxY + 1} units tall");
    }
}