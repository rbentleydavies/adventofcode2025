namespace advent;

public static class FileParser
{
    public static char[][] ReadFileAsTwoDimensionalArray(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        return lines.Select(line => line.ToCharArray()).ToArray();
    }
}