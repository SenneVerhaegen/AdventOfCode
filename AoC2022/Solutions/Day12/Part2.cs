namespace Solutions.Day12;

public class Part2 : Solution
{
    private readonly Grid _grid;
    private readonly PriorityQueue<State, int> _solutions = new();
    private readonly List<State> _states = new();
    private readonly Dictionary<State, int> _visitedStates = new();

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(12, useTestInput).ToList();

        var width = input[0].Length;
        var height = input.Count;

        var oneDimensional = string.Join("", input);
        
        _grid = new Grid(oneDimensional, height, width);

        Solve();
    }

    private void Solve()
    {
        _states.AddRange(_grid.InitialStatesPart2());
        
        var newStates = new List<State>();
        do
        {
            newStates.Clear();

            foreach (var state in _states)
            {
                if (_grid.ReachedGoal(state))
                {
                    _solutions.Enqueue(state, state.Steps);
                    continue;
                }

                if (_grid.CanMoveUp(state))
                {
                    var newState = state.MoveUp();
                    TryAddState(newState, newStates);
                }

                if (_grid.CanMoveDown(state))
                {
                    var newState = state.MoveDown();
                    TryAddState(newState, newStates);
                }

                if (_grid.CanMoveLeft(state))
                {
                    var newState = state.MoveLeft();
                    TryAddState(newState, newStates);
                }

                if (_grid.CanMoveRight(state))
                {
                    var newState = state.MoveRight();
                    TryAddState(newState, newStates);
                }
            }

            _states.Clear();
            _states.AddRange(newStates);
        } while (_states.Any());
    }


    private void TryAddState(State newState, List<State> newStates)
    {
        // Only add this new state if we didn't visit this position and it is a better path (fewer steps)
        if (!_visitedStates.ContainsKey(newState) || newState.Steps < _visitedStates[newState])
        {
            newStates.Add(newState);
            _visitedStates.Add(newState, newState.Steps);
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();

        Console.WriteLine($"Found {_solutions.Count} solutions");
        Console.WriteLine(_solutions.Count > 0
            ? $"Shortest path is {_solutions.Dequeue().Steps} steps"
            : "No solution found");
    }
}