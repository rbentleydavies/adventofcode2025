using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day7
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName).Select(x=>x.ToCharArray()).ToArray();

        var totalSplits = 0;
        while (lines.Length > 1)
        {
            (lines, var splits) = ProgressBeams(lines);
            totalSplits += splits;
        }
        Console.WriteLine($"Beam has split: {totalSplits} times");

    }

    private static (char[][], int) ProgressBeams(char[][] lines)
    {
        Console.WriteLine($"There are: {lines.Length} steps to go");
        Console.WriteLine(new string(lines[0]));
        Console.WriteLine(new string(lines[1]));
        var splits = 0;
        for (var c=0; c<  lines[0].Length; c++)
        {
            if (lines[0][c] == 'S')
            {
                //found the beam, can we move it down?
                if (lines[1][c] != '^')
                {
                    lines[1][c] = 'S';
                }
                else
                {
                    splits++;
                    if (c == 0 || lines[1][c - 1] == '^')
                    {
                        Console.Write($"Can't split, no space before {c}");
                    }

                    lines[1][c - 1] = 'S';

                    if (c == lines[1].Length - 1 || lines[1][c + 1] == '^')
                    {
                        Console.Write($"Can't split, no space after {c}");
                    }
                    
                    lines[1][c + 1] = 'S';
                }
            }
        }

        return (lines.Skip(1).ToArray(), splits);
    }
}