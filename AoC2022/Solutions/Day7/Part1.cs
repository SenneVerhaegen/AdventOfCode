namespace Solutions.Day7;

public class Part1 : ISolution
{
    private int _result;

    public void Run(bool useTestInput)
    {
        var terminalHistory = Util.GetInput(7, useTestInput).ToList();

        var root = new Directory(terminalHistory[0].Split(" ")[2], null);
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

        _result = directories
            .Select(directory => directory.Size())
            .Where(size => size <= 100000)
            .Sum();
    }

    public void PrintResult()
    {
        Console.WriteLine(_result);
    }
}