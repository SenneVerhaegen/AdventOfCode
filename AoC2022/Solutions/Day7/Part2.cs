namespace Solutions.Day7;

public class Part2 : Solution
{
    private int _result;

    private const int totalSpace = 70000000;
    private const int neededSpace = 30000000;

    public Part2(bool useTestInput) : base(useTestInput)
    {
        var terminalHistory = Util.GetInput(7, useTestInput).ToList();

        var root = new Directory(terminalHistory.First().Split(" ")[2], null);
        var workingDirectory = root;
        var directories = new List<Directory> { root };

        foreach (var line in terminalHistory.Skip(1))
        {
            if (line.StartsWith("$ ls"))
                continue;

            var parts = line.Split(" ");

            if (line.StartsWith("$ cd"))
            {
                var dirName = parts[2];
                workingDirectory = workingDirectory.Cd(dirName);
            }

            else if (line.StartsWith("dir"))
            {
                var dirName = parts[1];
                var newDir = new Directory(dirName, parent: workingDirectory);
                workingDirectory.Add(newDir);

                directories.Add(newDir);
            }

            else
            {
                var size = int.Parse(parts[0]);
                var name = parts[1];
                workingDirectory.Add(new File(name, size));
            }
        }

        var usedSpace = root.Size();
        var unusedSpace = totalSpace - usedSpace;

        _result = directories
            .Select(directory => directory.Size())
            .Where(dirSize => neededSpace <= unusedSpace + dirSize)
            .Min();
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine(_result);
    }
}