namespace Solutions.Day2;

public class Part2 : Solution
{
    private int _myScore;

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
            'A' when opponentMove == 'A' => 'D',
            'A' when opponentMove == 'B' => 'L',
            'A' when opponentMove == 'C' => 'W',

            'B' when opponentMove == 'A' => 'W',
            'B' when opponentMove == 'B' => 'D',
            'B' when opponentMove == 'C' => 'L',

            'C' when opponentMove == 'A' => 'L',
            'C' when opponentMove == 'B' => 'W',
            'C' when opponentMove == 'C' => 'D',

            _ => throw new ArgumentException("Unknown move")
        };
    }

    private static char GetMyNextMove(char outcome, char opponentMove)
    {
        return outcome switch
        {
            'X' when opponentMove == 'A' => 'C',
            'X' when opponentMove == 'B' => 'A',
            'X' when opponentMove == 'C' => 'B',

            'Y' when opponentMove == 'A' => 'A',
            'Y' when opponentMove == 'B' => 'B',
            'Y' when opponentMove == 'C' => 'C',

            'Z' when opponentMove == 'A' => 'B',
            'Z' when opponentMove == 'B' => 'C',
            'Z' when opponentMove == 'C' => 'A',

            _ => throw new ArgumentException("Unknown outcome")
        };
    }

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var games = Util.GetInput(2, useTestInput);

        foreach (var round in games)
        {
            var strategy = round.Split(' ').Select(char.Parse).ToList();
            var opponentMove = strategy[0];
            var outcome = strategy[1];
            var myMove = GetMyNextMove(outcome, opponentMove);

            var myRoundScore = PlayRound(myMove, opponentMove);
            _myScore += _scores[myMove];
            _myScore += _scores[myRoundScore];
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine($"My score: {_myScore}");
    }
}