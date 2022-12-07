namespace Solutions.Day7;

public class File : IFile
{
    private string _name;
    private int _size;

    public File(string name, int size)
    {
        _name = name;
        _size = size;
    }

    public int Size()
    {
        return _size;
    }

    public string Name()
    {
        return _name;
    }
}