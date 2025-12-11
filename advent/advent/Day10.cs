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
            int[] target = [];
            var length = 0;
            var buttons = new List<int[]>();
            var parts = line.Split(' ');
            foreach (var part in parts)
            {
                if (part.StartsWith('[') && part.EndsWith(']'))
                {
                    var binaryString = part.Substring(1, part.Length - 2).Replace('.', '0').Replace('#', '1');
                    length = binaryString.Length;
                    //target = Convert.ToInt32(binaryString, 2);
                }

                if (part.StartsWith('(') && part.EndsWith(')'))
                {
                    var binaryString = new int[length];
                    Array.Fill(binaryString, 0);
                    foreach (var bit in part.Substring(1, part.Length - 2).Split(','))
                    {
                        var bitNumber = int.Parse(bit);
                        binaryString[bitNumber] = 1;
                    }

                    //Console.WriteLine(new string(binaryString));
                    buttons.Add(binaryString);
                }

                if (part.StartsWith('{') && part.EndsWith('}'))
                {
                    var targetArray = new List<int>();
                    foreach (var bit in part.Substring(1, part.Length - 2).Split(','))
                    {
                        var bitNumber = int.Parse(bit);
                        targetArray.Add(bitNumber);
                    }

                    target = targetArray.ToArray();
                }
            }

            Console.WriteLine($"targets: {string.Join(",", target.Select(x => x.ToString()))}");
            //Console.WriteLine($"buttons: {string.Join(",", buttons)}");

            var bestOptionLowerBound = target.Max();
            var bestOptionUpperBound = target.Sum();
            Console.WriteLine($"bestOptionLowerBound: {bestOptionLowerBound}");
            Console.WriteLine($"bestOptionUpperBound: {bestOptionUpperBound}");
            // for (var allButtonClicks = bestOptionUpperBound; allButtonClicks >= bestOptionLowerBound; allButtonClicks--)
            // {
            //     var startValues = new int[target.Length];
            //     Array.Fill(startValues, 0);
            // Console.WriteLine(allButtonClicks);
            //     
            //     if (isPossible(startValues, target, buttons, allButtonClicks))
            //     {
            //         Console.WriteLine($"Success: {allButtonClicks}");
            //         totalBestOptions += allButtonClicks;
            //         break;
            //     }
            // }
            var A = new int[length, buttons.Count];
            for (var s = 0; s < buttons.Count; s++)
            for (var t = 0; t < length; t++)
                A[t, s] = buttons[s][t];

            var B = target.Select(t => (int)t).ToArray();
            
            // Use optimized method to find only the minimum sum solution
            var result = LinearEquationSolver.FindMinimumSumSolution(A, B);
            
            if (result != null)
            {
                var bestOption = result.Sum();
                Console.WriteLine($"Best result: {bestOption}");
                totalBestOptions += bestOption;
            }
            else
            {
                Console.WriteLine("No solution found!");
            }
        }

        Console.WriteLine($"Total best options:{totalBestOptions}");
    }

    private static bool isPossible(int[] startValues, int[] targetValues, IEnumerable<int[]> buttons, int maxPresses)
    {
        var nextButton = buttons.FirstOrDefault();
        if (nextButton == null) return false;

        var success = false;
        for (var i = maxPresses; i >= 0; i--)
        {
            var trialValues = new int[startValues.Length];
            Array.Copy(startValues, trialValues, startValues.Length);
            var fail = false;
            var notMatchYet = false;
            for (var j = 0; j < startValues.Length; j++)
            {
                trialValues[j] += nextButton[j] * i;
                //Console.WriteLine(string.Join(",", trialValues));
                if (trialValues[j] > targetValues[j])
                {
                    fail = true;
                    break;
                }

                if (trialValues[j] < targetValues[j]) notMatchYet = true;
            }

            if (fail) continue;
            if (!notMatchYet)
            {
                Console.WriteLine("this matches");
                success = true;
                break;
            }

            if (isPossible(trialValues, targetValues, buttons.Skip(1), maxPresses - i))
            {
                success = true;
                break;
            }
        }

        return success;
    }

    public class Button
    {
        public int Index { get; set; }
        public int[] Changes { get; set; }
        public int[] PossibleOptions { get; set; }
    }
}