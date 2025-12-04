using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day4
{
    public static void Run(string fileName)
    {
        var map = FileParser.ReadFileAsTwoDimensionalArray(fileName);
        var accessiblePaperRollCount = 0;
        
        var (xmap, xaccessiblePaperRollCount) = MovePaperAroundMap(map);
        Console.WriteLine(xaccessiblePaperRollCount);
    }

    private static (char[][], int) MovePaperAroundMap(char[][] map)
    {
        var newMap = new char[map.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            newMap[i] = new char[map[i].Length];
            Array.Copy(map[i], newMap[i], map[i].Length);
        }

        var accessiblePaperRollCount = 0;
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] != '@') continue;

                if (CountAdjacentPaperRolls(map, i, j) < 4)
                {
                    Console.WriteLine($"{i},{j},{map[i][j]}");
                    newMap[i][j] = '.';
                    accessiblePaperRollCount++;
                }
                
            }
        }
        return (newMap, accessiblePaperRollCount);
    }
    private static int CountAdjacentPaperRolls(char[][] map, int i, int j)
    {
        var count = 0;
        for (var ii = i - 1; ii <= i + 1; ii++)
        {
            if (ii < 0 || ii >= map.Length) continue;
            for (var jj = j - 1; jj <= j + 1; jj++)
            {
                if (jj < 0 || jj >= map.Length) continue;
                if (ii == i && jj == j) continue;
                if (map[ii][jj] == '@') count++;
            }
        }
        return count;
    }
}