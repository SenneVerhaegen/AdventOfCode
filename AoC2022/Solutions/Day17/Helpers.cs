namespace Solutions.Day17;

public static class Helpers
{
    public static void PrintStep(string[,] grid, List<(int, int)> positions, int maxY, bool rest = false)
    {
        AddPositionsToGrid(positions, grid, rest);
        Print(grid, maxY);
        RemovePositionsFromGrid(positions, grid);
    }

    public static void InitializeHelperGrid(string[,] grid)
    {
        for (var row = 0; row < 3068; row++)
        {
            for (var col = 0; col < 7; col++)
            {
                grid[row, col] = ".";
            }
        }
    }

    private static void AddPositionsToGrid(List<(int, int)> positions, string[,] grid, bool rest = false)
    {
        var c = rest ? "#" : "@";
        foreach (var (x, y) in positions)
        {
            grid[y, x] = c;
        }
    }

    private static void RemovePositionsFromGrid(List<(int, int)> positions, string[,] grid)
    {
        foreach (var (x, y) in positions)
        {
            grid[y, x] = ".";
        }
    }

    private static void Print(string[,] grid, int maxY)
    {
        var bound = maxY + 3 + 4;
        for (var row = bound; row >= 0; row--)
        {
            for (var col = 0; col <= 6; col++)
            {
                Console.Write(grid[row, col]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}