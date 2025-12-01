namespace advent;

public static class FileParser
{
    public static string[] ReadFileAsLineArray(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        return lines;
    }
}