using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day3
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var numbers = new List<int>();
        foreach (var line in lines)
        {
            
            var (bestFirstNumber, bestFirstIndex)  = GetBestNumber(line.Substring(0, line.Length - 1));
            var (bestSecondNumber, bestSecondIndex) = GetBestNumber(line.Substring(bestFirstIndex+1, line.Length-bestFirstIndex-1));
            
            numbers.Add(bestFirstNumber * 10 + bestSecondNumber);
        }

        foreach (var number in numbers)
        {
            Console.WriteLine($"{number}");
        }
        Console.WriteLine(numbers.Sum());
    }

    private static (int, int) GetBestNumber(string line)
    {
        var bestNumber = -1;
        var bestIndex = -1;
        for (var i = 0; i<line.Length; i++)
        {
            var thisNumber = int.Parse(line[i].ToString());
            if (thisNumber > bestNumber)
            {
                bestNumber = thisNumber;
                bestIndex = i;
                if (bestNumber == 9) break;
            }
        }
        return (bestNumber, bestIndex);
    }
}