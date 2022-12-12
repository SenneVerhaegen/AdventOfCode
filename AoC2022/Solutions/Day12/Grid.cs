namespace Solutions.Day12;

public class Grid
{
    private string _grid;
    private readonly int _width;
    private readonly int _height;
    private readonly State _endState;

    public Grid(string grid, int height, int width)
    {
        _grid = grid;
        _height = height;
        _width = width;

        var i = _grid.IndexOf('E');
        var row = i / _width;
        var col = i % _width;
        _endState = new State(row, col);

        // Replace 'E' with level so we don't need an extra conditional case later
        _grid = _grid.Replace('E', 'z');
    }

    public State InitialStatePart1()
    {
        var i = _grid.IndexOf('S');
        var row = i / _width;
        var col = i % _width;

        // Replace 'S' with level so we don't need an extra conditional case later
        _grid = _grid.Replace('S', 'a');

        return new State(row, col);
    }

    public IEnumerable<State> InitialStatesPart2()
    {
        return _grid.Select((elevation, i) =>
        {
            if (elevation != 'a') return null;

            var row = i / _width;
            var col = i % _width;
            return new State(row, col);
        }).Where(x => x != null)!;
    }

    private static bool CanMove(char current, char next)
    {
        // Can descend multiple levels
        if (next < current) return true;

        // Can only ascend 1 level
        return next - current <= 1;
    }

    public bool CanMoveUp(State state)
    {
        if (state.Row == 0) return false;

        var current = _grid[_width * state.Row + state.Col];
        var next = _grid[_width * (state.Row - 1) + state.Col];

        return CanMove(current, next);
    }

    public bool CanMoveDown(State state)
    {
        if (state.Row == _height - 1) return false;

        var current = _grid[_width * state.Row + state.Col];
        var next = _grid[_width * (state.Row + 1) + state.Col];

        return CanMove(current, next);
    }

    public bool CanMoveLeft(State state)
    {
        if (state.Col == 0) return false;

        var current = _grid[_width * state.Row + state.Col];
        var next = _grid[_width * state.Row + state.Col - 1];

        return CanMove(current, next);
    }

    public bool CanMoveRight(State state)
    {
        if (state.Col == _width - 1) return false;

        var current = _grid[_width * state.Row + state.Col];
        var next = _grid[_width * state.Row + state.Col + 1];

        return CanMove(current, next);
    }

    public bool ReachedGoal(State state) => state.Row == _endState.Row && state.Col == _endState.Col;
}