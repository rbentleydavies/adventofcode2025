using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day5
{
    public static void Run(string fileName)
    {
        var ranges = new List<Range>();
        var ingredients = new List<long>();
        var lines = File.ReadAllLines(fileName);
        foreach (var line in lines)
        {
            var r = Range.RangeFromString(line);
            if (r != null) {
                ranges.Add(r);
            }
            else
            {
                if (long.TryParse(line, out var ingredient))
                {
                    ingredients.Add(ingredient);
                }
            }
        }

        var freshIngredientCount = 0;
        foreach (var ingredient in ingredients)
        {
            foreach (var range in ranges)
            {
                if (range.IsInRange(ingredient))
                {
                    freshIngredientCount++;
                    break;
                }
            }
        }
        Console.WriteLine($"There are {ranges.Count} ranges and {ingredients.Count} ingredients.");
        Console.WriteLine($"There are {freshIngredientCount} fresh ingredients.");
    }
    private class Range
    {
        public long Min { get; set; }
        public long Max { get; set; }
        public static Range? RangeFromString(string line)
        {
            var split = line.Split('-');
            return split.Length != 2 ? null : new Range{ Min = long.Parse(split[0]), Max= long.Parse(split[1]) };
        }
        public bool IsInRange(long value)
        {
            return value >= Min && value <= Max;
        }
    }
}