namespace Solutions.Day7;

public class Directory : IFile
{
    private readonly Directory? _parent;
    private readonly string _name;
    private readonly Dictionary<string, IFile> _files = new();

    public Directory(string name, Directory? parent)
    {
        _name = name;
        _parent = parent;
    }

    public int Size()
    {
        return _files.Values.Select(f => f.Size()).Sum();
    }

    public string Name()
    {
        return _name;
    }

    public Directory Cd(string name)
    {
        if (name == "..")
        {
            if (_parent == null) throw new Exception("Cannot 'cd ..' out of root directory");
            
            return _parent;
        }

        return (Directory)_files[name];
    }

    public void Add(IFile file)
    {
        _files[file.Name()] = file;
    }
}