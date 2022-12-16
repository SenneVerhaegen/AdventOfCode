using System.Reflection;

namespace Solutions;

public static class Util
{
    public static IEnumerable<string> GetInput(int day, bool useTestInput)
    {
        var fileName = useTestInput ? "test.txt" : "input.txt";
        var path = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
            throw new Exception($"No input file '{fileName}' is present"), $"Day{day}/Inputs/{fileName}");
        return File.ReadLines(path);
    }
}