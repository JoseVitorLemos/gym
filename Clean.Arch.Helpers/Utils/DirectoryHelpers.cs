namespace Clean.Arch.Helpers.Utils;

public static class DirectoryHelpers
{
    public static void SaveTextFile(string directoryPath, string fileName, string content)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, fileName);

        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine(content);
        }
    }
}
