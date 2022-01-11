namespace Common.Models;

public class FileName
{
    public FileName(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ArgumentNullException.ThrowIfNull(path);
            throw new ArgumentException(path);
        }

        var fullPath = path;
        if (fullPath.StartsWith('~'))
        {
            fullPath = fullPath.Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }
        
        FullPath = Path.GetFullPath(fullPath);
    }

    public string FullPath { get; }

    public override string ToString()
    {
        return FullPath;
    }
}