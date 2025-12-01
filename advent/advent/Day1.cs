using System.Runtime.CompilerServices;

namespace advent;

public class Day1
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        int currentPosition = 50;
        int timesAtZero = 0;
        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line.Substring(1));
            currentPosition = Move(direction, distance, currentPosition);
            Console.WriteLine($"{line} : {currentPosition}");
            if (currentPosition == 0)
            {
                timesAtZero++;
            }
        }
        Console.WriteLine(timesAtZero);
    }

    private static int Move(char direction, int distance, int startPosition)
    {
        var newPosition = 0;
        if (direction == 'L')
        {
            newPosition = startPosition - (distance % 100);
            if (newPosition < 0)
            {
                newPosition += 100;
            }
        }
        else
        {
            newPosition = (startPosition + distance) % 100;
        }
        return newPosition;
    }
}