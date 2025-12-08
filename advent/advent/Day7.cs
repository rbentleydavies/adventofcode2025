using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day7
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName).Select(x=>x.ToCharArray()).ToArray();

        var totalSplits = ProgressBeams(lines);
            
        Console.WriteLine($"Beam has split: {totalSplits} times");
        // for (var i = 0; i < lines.Length; i++)
        // {
        //     for (var j = 0; j < lines[i].Length; j++)
        //     {
        //         if(cacheSteps.ContainsKey((j, i)))
        //             Console.WriteLine($"{i}, {j}   :{cacheSteps[(j,i)]}");
        //     }
        // }

    }
    private static Dictionary<(int,int),long> cacheSteps = new();
    private static long ProgressBeams(char[][] lines)
    {
        if (lines.Length <= 1) return 1;
        Console.WriteLine($"There are: {lines.Length} steps to go");
        Console.WriteLine(new string(lines[0]));
        Console.WriteLine(new string(lines[1]));
        long splits = 1;
        for (var c=0; c<  lines[0].Length; c++)
        {
            if (lines[0][c] == 'S')
            {
                Console.WriteLine($"The beam is at {c}");
                if (cacheSteps.ContainsKey((c,lines.Length))) return cacheSteps[(c,lines.Length)];
                //found the beam, can we move it down?
                if (lines[1][c] != '^')
                {
                    Console.WriteLine($"Moving the beam down one");
                    var nextStepMap = NextStepMap(lines);
                    nextStepMap[0][c] = 'S';
                    splits = ProgressBeams(nextStepMap);
                }
                else
                {
                    Console.WriteLine($"Splitting the beam");
                    //splits++;
                    if (c == 0 || lines[1][c - 1] == '^')
                    {
                        Console.WriteLine($"Can't split, no space before {c}");
                    }
                    var leftTimeline = NextStepMap(lines);
                    
                    leftTimeline[0][c - 1] = 'S';
                    var leftTimelineSplits = ProgressBeams(leftTimeline);
                    
                    if (c == lines[1].Length - 1 || lines[1][c + 1] == '^')
                    {
                        Console.WriteLine($"Can't split, no space after {c}");
                    }
                    var rightTimeline = NextStepMap(lines);
                    
                    rightTimeline[0][c + 1] = 'S';
                    var rightTimelineSplits = ProgressBeams(rightTimeline);
                    
                    splits = rightTimelineSplits + leftTimelineSplits;
                }
                cacheSteps[(c,lines.Length)] = splits;
            }
        }

        return splits;
    }

    private static char[][] NextStepMap(char[][] lines)
    {
        var output = new char[lines.Length - 1][];
        for (var i = 1; i < lines.Length; i++)
        {
            output[i-1] = new char[lines[i].Length];
            
            Array.Copy(lines[i],output[i-1], lines[i].Length); 
        }
        Console.WriteLine($"New map has {output.Length} steps, old map had {lines.Length} steps");
        return output;
    }
}