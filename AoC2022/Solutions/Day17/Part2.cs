using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Solutions.Day17.Rocks;
using Solutions.Shared;

namespace Solutions.Day17;

public class Part2 : Solution
{
    // How to detect a cycle?

    // Run the program to get the 'cycle string'
    // Append (rockType, direction) to a string for every rock
    public static int Width = 7;

    private int _maxY = -1;
    private int _nbrOfBlocks;
    private int _maxNbrOfBlocks;
    private RingQueue<int> _blockTypes = new();
    private readonly RingList<char> _jets;
    private readonly string[,] _grid = new string[4000, 7];

    // Positions (x, y)
    private readonly HashSet<(int, int)> _blockedPositions = new();

    private readonly Dictionary<int, int> _highestInCol = new();
    private readonly HashSet<int> _states = new();

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var blocks = 1_000_000;
        // var blocks = 2022;
        var pmy = 0;

        // surface -> dict_max_y_for_x
        // state = (surface, spawned rock, jet index)
        // hash state and save
        // after every move, check hash of current state
        // if hash exists => cycle is detected
        // to get the jet index, i cannot use a queue (use a list)

        var input = Util.GetInput(17, useTestInput).ToList()[0];
        _jets = new RingList<char>(input);

        SetupRockTypes();
        AddBottomLayer();

        while (_nbrOfBlocks < blocks)
        {
            var (rock, rockType) = NextRock();

            List<(int, int)> positions = rock.GetPositions();
            // Helpers.PrintStep(_grid, positions, _maxY);

            while (true)
            {
                var direction = _jets.Next();
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
            // Helpers.PrintStep(_grid, positions, _maxY, true);

            foreach (var position in positions)
            {
                _blockedPositions.Add(position);
                _highestInCol[position.Item1] = Math.Max(_highestInCol[position.Item1], position.Item2);
            }

            _maxY = Math.Max(rock.Y1, _maxY);

            if (_highestInCol.Values.Distinct().Count() == 1)
            {
                Console.WriteLine("dmlfjmqlds");
            }

            var hash = HashState(rockType);
            if (_states.Contains(hash))
            {
                // Console.WriteLine("State occured");
                // Console.WriteLine($"Height: {_maxY}");
                // Console.WriteLine($"Delta {_maxY - pmy}");
                pmy = _maxY;
            }
            else
                _states.Add(hash);

            _nbrOfBlocks++;
        }
    }

    private int HashState(int rockType)
    {
        // var surface = Surface();
        var jetIndex = _jets.Position;
        // var hash = HashCode.Combine(rockType, jetIndex);
        var hash = 17;
        hash *= jetIndex;
        hash *= 13;
        hash *= rockType;
        // Console.WriteLine(hash);
        // var hash = HashCode.Combine(surface, rockType, jetIndex) * 11 * 17 * 83 * 97 * 7039;
        return hash;
    }

    private int[] Surface()
    {
        var indices = new int[7];
        var i = 0;
        var lowestY = _highestInCol.Values.Min();
        foreach (var (x, y) in _highestInCol)
        {
            indices[i++] = y - lowestY + 1;
            // Console.Write($"{y - lowestY} ");
        }

        // Console.WriteLine();

        return indices;
    }

    private void AddBottomLayer()
    {
        for (var x = 0; x < 7; x++)
        {
            _blockedPositions.Add((x, -1));
            _highestInCol[x] = -1;
        }
    }

    private void SetupRockTypes()
    {
        for (var i = 1; i <= 5; i++)
            _blockTypes.Enqueue(i);
    }


    private (Rock, int) NextRock()
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

        return (rock, rockType);
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"Tower is {_maxY + 1} units tall");
    }
}