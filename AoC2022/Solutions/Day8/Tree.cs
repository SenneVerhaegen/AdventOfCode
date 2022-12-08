namespace Solutions.Day8;

public class Tree
{
    private readonly int _height;
    private int? _score;

    private Tree? _leftNeighbor;
    private Tree? _rightNeighbor;
    private Tree? _upperNeighbor;
    private Tree? _lowerNeighbor;

    public Tree(int height)
    {
        _height = height;
    }

    public void AddNeighbor(Tree neighbor, Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                _leftNeighbor = neighbor;
                break;
            case Direction.Right:
                _rightNeighbor = neighbor;
                break;
            case Direction.Up:
                _upperNeighbor = neighbor;
                break;
            case Direction.Down:
                _lowerNeighbor = neighbor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public void AddLeft(Tree neighbor)
    {
        _leftNeighbor = neighbor;
    }

    public void AddRight(Tree neighbor)
    {
        _rightNeighbor = neighbor;
    }

    public void AddUpper(Tree neighbor)
    {
        _upperNeighbor = neighbor;
    }

    public void AddBottom(Tree neighbor)
    {
        _lowerNeighbor = neighbor;
    }

    public int ScenicScore()
    {
        if (_score != null) return (int)_score;

        _score =
            ViewDistance(Direction.Left, _height) *
            ViewDistance(Direction.Right, _height) *
            ViewDistance(Direction.Up, _height) *
            ViewDistance(Direction.Down, _height);

        return (int)_score;
    }

    private int ViewDistance(Direction direction, int viewHeight)
    {
        var neighbor = direction switch
        {
            Direction.Left => _leftNeighbor,
            Direction.Right => _rightNeighbor,
            Direction.Up => _upperNeighbor,
            Direction.Down => _lowerNeighbor,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        if (neighbor == null) return 0;
        if (neighbor._height >= viewHeight) return 1;

        return 1 + neighbor.ViewDistance(direction, viewHeight);
    }

    public enum Direction
    {
        Left,
        Right,
        Down,
        Up
    }
}