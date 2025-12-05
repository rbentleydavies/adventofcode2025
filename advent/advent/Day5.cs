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

        long totalFreshIngredients = 0;
        var firstNumber = ranges.Min(r => r.Min);
        var lastNumber = ranges.Max(r => r.Max);
        var orderedRanges = ranges.OrderBy(x => x.Min).ThenBy(x => x.Max).ToArray();
        long previousMin = 0;
        long previousMax = 0;
        foreach (var range in orderedRanges)
        {
            if (previousMin == range.Min)
            {
                Console.WriteLine($"duplicate range starts:{range.Min}");
            }
            if (previousMax == range.Max)
            {
                Console.WriteLine($"duplicate range ends:{range.Max}");
            }
            previousMin = range.Min;
            if (previousMax >= range.Max)
            {
                //new range completely overlapped by previous range
                continue;
            }
            if (previousMax < range.Max && range.Min <= previousMax)
            {
                //there's an overlap
                totalFreshIngredients += (range.Max - previousMax);
                
            }
            else
            {
                totalFreshIngredients += (range.Max - range.Min + 1);
            }
            previousMax = range.Max;
        }
        var freshIngredientCount = 0;
        // foreach (var ingredient in ingredients)
        // {
        //     foreach (var range in ranges)
        //     {
        //         if (range.IsInRange(ingredient))
        //         {
        //             freshIngredientCount++;
        //             break;
        //         }
        //     }
        // }

        // for (var ingredient = firstNumber; ingredient <= lastNumber; ingredient++)
        // {
        //     foreach (var range in ranges)
        //     {
        //         if (range.IsInRange(ingredient))
        //         {
        //             freshIngredientCount++;
        //             break;
        //         }
        //     }
        // }
        Console.WriteLine($"There are {ranges.Count} ranges and {ingredients.Count} ingredients.");
        Console.WriteLine($"There are {freshIngredientCount} fresh ingredients.");
        Console.WriteLine($"There are {totalFreshIngredients} fresh ingredients altogether.");
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