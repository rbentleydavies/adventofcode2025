using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day6
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var numberSet = new List<long[]>();
        string[] operators = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
        var numbers = new List<long>();
        for (var c = 0; c < lines[0].Length; c++)
        {
            var numberString = "";
            for (var r = 0; r < lines.Length - 1; r++)
            {
                numberString += lines[r][c];
            }

            if (long.TryParse(numberString, out var number))
            {
                numbers.Add(number);
            }
            else
            {
                numberSet.Add(numbers.ToArray());
                numbers.Clear();
            }
        }

        if (numberSet.Count != 0)
        {
            numberSet.Add(numbers.ToArray());
        }
        var numberArray = numberSet.ToArray();
        Console.WriteLine($"There are {numberArray.Length} numbers in this set.");
        Console.WriteLine($"There are {operators.Length} operators in this set.");
        long runningTotal = 0;
        for (var i = 0; i < operators.Length; i++)
        {
            var op = operators[i];
            switch (op)
            {
                case "+":
                    var resultPlus = numberArray[i].Sum();
                    runningTotal += resultPlus;
                    break;
                case "*":
                    var resultTimes = numberArray[i].Aggregate((long)1, (a, b) =>  a * b );
                    runningTotal += resultTimes;
                    break;
            }
        }
        Console.WriteLine($"Total running: {runningTotal}");
    }
}