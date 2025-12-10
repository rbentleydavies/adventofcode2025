namespace advent;

public static class Day10
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var targets = new List<int>();
        var totalBestOptions = 0;
        foreach (var line in lines)
        {
            var target = 0;
            var length = 0;
            var buttons = new List<int>();
            var parts = line.Split(' ');
            foreach (var part in parts)
            {
                if (part.StartsWith('[') && part.EndsWith(']'))
                {
                    var binaryString = part.Substring(1, part.Length - 2).Replace('.', '0').Replace('#', '1');
                    length = binaryString.Length;
                    target = Convert.ToInt32(binaryString, 2);
                }

                if (part.StartsWith('(') && part.EndsWith(')'))
                {
                    var binaryString = new char[length];
                    Array.Fill(binaryString, '0');
                    foreach (var bit in part.Substring(1, part.Length - 2).Split(','))
                    {
                        var bitNumber = int.Parse(bit);
                        binaryString[bitNumber] = '1';
                    }

                    //Console.WriteLine(new string(binaryString));
                    buttons.Add(Convert.ToInt32(new string(binaryString), 2));
                }
            }

            Console.WriteLine($"target: {target}");
            Console.WriteLine($"buttons: {string.Join(",", buttons)}");
            var possibleOptionStrings = new List<char[]>();
            for (var i = 0; i < Math.Pow(2, buttons.Count); i++)
            {
                var formatString = $"B{buttons.Count}";
                var optionsString = i.ToString(formatString).ToCharArray();
                var value = buttons
                    .Where((t, x) => optionsString[x] == '1')
                    .Aggregate(0, (current, t) => current ^ t);
                if (value == target)
                    possibleOptionStrings.Add(optionsString);
            }
            foreach (var possibleOptionString in possibleOptionStrings)
                Console.WriteLine(new string(possibleOptionString));
            var bestOption = possibleOptionStrings.Min(s => s.Count(c => c == '1'));
            Console.WriteLine($"best: {bestOption}");
            totalBestOptions+=bestOption;
        }
        Console.WriteLine($"Total best options:{totalBestOptions}");
    }
}