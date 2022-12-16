namespace Solutions.Day16;

public class Part1 : Solution
{
    // private const int TotalMinutes = 30;
    // private int _totalPressure = 0;

    private readonly int _nbrOfVertices;

    private readonly List<(string, string)> _edges = new();
    private readonly Dictionary<string, int> _vertexMap = new();

    /// A matrix that describes the weight (distance) from each vertex to every other vertex.
    private int[,] _distanceMatrix;

    // This matrix describes
    private string[,] _successorMatrix;

    // There are 58 nodes with at least 2 tunnels
    // Total search space: > 2^58 (288_230_376_151_711_744)

    // Moving to a tunnel costs 1 minute: path length == 30
    // Opening a valve costs 1 minute: path length <= 30
    // Process less then 1_073_741_824 nodes

    public Part1(bool useTestInput) : base(useTestInput)
    {
        var input = Util.GetInput(16, useTestInput).ToList();
        _nbrOfVertices = input.Count;

        Parse(input);
        InitializeDistanceMatrix();
        FloydWarshall();
    }

    /// <summary>
    /// Because travelling through a tunnel has a cost of 1 in all cases, all edges of the graph have a weight of 1.
    /// Non-existent edges get a weight of infinity (int.MaxValue).
    /// </summary>
    private void InitializeDistanceMatrix()
    {
        _distanceMatrix = new int[_nbrOfVertices, _nbrOfVertices];
        _successorMatrix = new string[_nbrOfVertices, _nbrOfVertices];
        for (var i = 0; i < _nbrOfVertices; i++)
        for (var j = 0; j < _nbrOfVertices; j++)
            _distanceMatrix[i, j] = int.MaxValue;

        foreach (var (from, to) in _edges)
            _distanceMatrix[_vertexMap[from], _vertexMap[to]] = 1;
    }

    /// <summary>
    /// The Floyd-Warshall algorithm calculates the shortest path from any vertex to any other vertex.
    /// Complexity: O(|V|^3)
    /// </summary>
    private void FloydWarshall()
    {
        for (var intermediate = 0; intermediate < _nbrOfVertices; intermediate++)
        {
            for (var from = 0; from < _nbrOfVertices; from++)
            {
                for (var to = 0; to < _nbrOfVertices; to++)
                {
                    var weightViaIntermediateNode =
                        _distanceMatrix[from, intermediate] + _distanceMatrix[intermediate, to];

                    if (weightViaIntermediateNode < _distanceMatrix[from, to])
                    {
                        // The weight is smaller if we go via the intermediate node, so we update the weight
                        _distanceMatrix[from, to] = weightViaIntermediateNode;

                        // Replace the current successor 'to' with the better successor 'intermediate'
                        _successorMatrix[from, to] = _successorMatrix[from, intermediate];
                    }
                }
            }
        }
    }

    private void Parse(List<string> input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(" ");
            var from = parts[1];
            var flowRate = int.Parse(parts[4].Split("=")[1][..^1]);

            if (!_vertexMap.ContainsKey(from))
                _vertexMap[from] = _vertexMap.Count;

            var index = line.IndexOf("to valve", StringComparison.Ordinal) + "to valve".Length;
            var edges = line[index..]
                .Split(" ")
                .Where(c => c is not "s" and not "")
                .Select(to => (from, to.Replace(",", "")))
                .ToList();

            _edges.AddRange(edges);

            foreach (var (_, to) in edges)
                if (!_vertexMap.ContainsKey(to))
                    _vertexMap[to] = _vertexMap.Count;
        }
    }

    // private void Foo()
    // {
    //     var pressure = new Dictionary<int, int>();
    //     pressure[1] = 0;
    //     pressure[2] = 0;
    //     pressure[3] = 20;
    //     pressure[4] = 20;
    //     pressure[5] = 20;
    //     pressure[6] = 33;
    //     pressure[7] = 33;
    //     pressure[8] = 33;
    //     pressure[9] = 33;
    //     pressure[10] = 54;
    //     pressure[11] = 54;
    //     pressure[12] = 54;
    //     pressure[13] = 54;
    //     pressure[14] = 54;
    //     pressure[15] = 54;
    //     pressure[16] = 54;
    //     pressure[17] = 54;
    //     pressure[18] = 76;
    //     pressure[19] = 76;
    //     pressure[20] = 76;
    //     pressure[21] = 76;
    //     pressure[22] = 79;
    //     pressure[23] = 79;
    //     pressure[24] = 79;
    //     pressure[25] = 81;
    //     pressure[26] = 81;
    //     pressure[27] = 81;
    //     pressure[28] = 81;
    //     pressure[29] = 81;
    //     pressure[30] = 81;
    //
    //     var totalPressure = 0;
    //     for (var minute = 0; minute < TotalMinutes; minute++)
    //     {
    //         totalPressure += pressure[minute + 1];
    //     }
    //
    //     Console.WriteLine(totalPressure);
    // }
}