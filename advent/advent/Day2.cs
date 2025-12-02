using System.Runtime.CompilerServices;

namespace advent;

public static class Day2
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var rangeStrings = lines[0].Split(',');
        var ranges = rangeStrings.Select(StringToRange).ToArray();
        long runningTotal = 0;
        foreach (var range in ranges)
        {
            if (range == null) continue;
            Console.WriteLine($"{range.Start} : {range.End}");
            var fancyNumbers = FancyNumbersInRange(range);
            if (fancyNumbers.Any())
            {
                Console.WriteLine(fancyNumbers.Select(n => n.ToString()).Aggregate((a, b) => $"{a},{b}"));
                runningTotal += fancyNumbers.Sum();
            }
        }
        Console.WriteLine(runningTotal);
    }

    private class Range
    {
        public long Start;
        public long End;
    }

    private static Range? StringToRange(string rangeString)
    {
        var split = rangeString.Split('-');
        if (split.Length != 2) return null;
        var start = long.Parse(split[0]);
        var end = long.Parse(split[1]);
        return new Range { Start = start, End = end };
    }

    private static long[] FancyNumbersInRange(Range range)
    {
        var foundNumbers = new List<long>();
        var startString = range.Start.ToString();
        if (startString.Length % 2 == 1) startString = "0" + startString;
        var startHalf = startString.Substring(0, startString.Length / 2);
        var start = long.Parse(startHalf);
        while (true)
        {
            var num = long.Parse(start.ToString() + start.ToString());
            if (num <= range.End && num >= range.Start)
            {
                foundNumbers.Add(num);
            }

            if (num > range.End) break;
            start++;
        }

        return foundNumbers.ToArray();
    }

 

}