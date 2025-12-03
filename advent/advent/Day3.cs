using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day3
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var numbers = new List<long>();
        foreach (var line in lines)
        {
            long number = 0;
            int position = 0;
            for (int step = 1; step <= 12; step++)
            {
                var (x, i) = GetBestNumber(line.Substring(position, line.Length - position - (12 - step)));
                position += i + 1;
                number += x * (long)Math.Pow(10, 12 - step);
                Console.WriteLine($"- {number}");
            }
            numbers.Add(number);
        }

        foreach (var num in numbers)
        {
            Console.WriteLine($"* {num}");
        }
        Console.WriteLine(numbers.Sum());
    }

    private static (int, int) GetBestNumber(string line)
    {
        Console.WriteLine(line);
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