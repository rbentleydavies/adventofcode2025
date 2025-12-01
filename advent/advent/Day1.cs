using System.Runtime.CompilerServices;

namespace advent;

public static class Day1
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
            (currentPosition, timesAtZero) = Move(direction, distance, currentPosition, timesAtZero);
            Console.WriteLine($"{line} : {currentPosition}, {timesAtZero}");
        }
        Console.WriteLine(timesAtZero);
    }

    private static (int, int) Move(char direction, int distance, int startPosition, int timesAtZero)
    {
        var newPosition = startPosition;
        var newTimesAtZero = timesAtZero;
        for (var i = 0; i < distance; i++)
        {
            (newPosition, newTimesAtZero) = Click(direction, newPosition, newTimesAtZero);
        }
        
        return (newPosition, newTimesAtZero);
    }

    private static (int, int) Click(char direction, int startPosition, int timesAtZero)
    {
        var newPosition = startPosition;
        if (direction == 'L')
        {
            newPosition--;
            if(newPosition < 0) newPosition += 100;
        }
        else
        {
            newPosition++;
            if(newPosition > 99) newPosition -= 100;
        } 
        return (newPosition, newPosition==0 ? timesAtZero + 1: timesAtZero);
    }
}