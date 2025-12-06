using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day6
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var numberSet = new List<long[]>();
        string[] operators = [];
        foreach (var line in lines)
        {
            if (line[0] == '+' || line[0] == '*')
            {
                operators = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
                Console.WriteLine($"Found {operators.Length} operators");
                continue;
            }
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
            numberSet.Add(numbers);
            Console.WriteLine($"Found {numbers.Length} numbers");
        }
        var numberArray = numberSet.ToArray();
        long runningTotal = 0;
        for (var i = 0; i < operators.Length; i++)
        {
            var op = operators[i];
            switch (op)
            {
                case "+":
                    var resultPlus = numberArray.Sum(x => x[i]);
                    runningTotal += resultPlus;
                    break;
                case "*":
                    var resultTimes = numberArray.Aggregate((long)1, (a, b) =>  a * b[i] );
                    runningTotal += resultTimes;
                    break;
            }
        }
        Console.WriteLine($"Total running: {runningTotal}");
    }
}