namespace Solutions.Day2;

public class Part1 : Solution
{
    private readonly int _myScore;

    private readonly Dictionary<char, int> _scores = new()
    {
        { 'A', 1 },
        { 'X', 1 },

        { 'B', 2 },
        { 'Y', 2 },

        { 'C', 3 },
        { 'Z', 3 },

        { 'L', 0 },
        { 'D', 3 },
        { 'W', 6 }
    };

    private static char PlayRound(char myMove, char opponentMove)
    {
        return myMove switch
        {
            'X' when opponentMove == 'A' => 'D',
            'X' when opponentMove == 'B' => 'L',
            'X' when opponentMove == 'C' => 'W',

            'Y' when opponentMove == 'A' => 'W',
            'Y' when opponentMove == 'B' => 'D',
            'Y' when opponentMove == 'C' => 'L',

            'Z' when opponentMove == 'A' => 'L',
            'Z' when opponentMove == 'B' => 'W',
            'Z' when opponentMove == 'C' => 'D',

            _ => throw new ArgumentException("Unknown move")
        };
    }


    public Part1(bool useTestInput) : base(useTestInput)
    {
        var games = Util.GetInput(2, useTestInput);

        foreach (var round in games)
        {
            var strategy = round.Split(' ').Select(char.Parse).ToList();
            var opponentMove = strategy[0];
            var myMove = strategy[1];

            _myScore += _scores[myMove];

            var myRoundScore = PlayRound(myMove, opponentMove);
            _myScore += _scores[myRoundScore];
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"My score: {_myScore}");
    }
}