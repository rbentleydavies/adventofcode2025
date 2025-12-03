// See https://aka.ms/new-console-template for more information

using advent;

var day = 0;
var sample = false;
foreach (var arg in args)
{
    if (arg.StartsWith("-day"))
    {
        day = int.Parse(arg.Substring("-day".Length));
    }
    if (arg.StartsWith("-sample")) 
    {
        sample = true; 
    }
}
Console.WriteLine($"Day {day}: {sample}");
var filename = sample ? "sample.txt" : "main.txt";
var filepath = $"../../../inputs/day{day}/{filename}";

switch (day)
{
    case 1:
        Day1.Run(filepath);
        break;
    case 2:
        Day2.Run(filepath);
        break;
    case 3:
        Day3.Run(filepath);
        break;
    case 4:
        Day4.Run(filepath);
        break;
    case 5:
        Day5.Run(filepath);
        break;
    case 6:
        Day6.Run(filepath);
        break;
    case 7:
        Day7.Run(filepath);
        break;
    case 8:
        Day8.Run(filepath);
        break;
    case 9:
        Day9.Run(filepath); 
        break;
    case 10:
        Day10.Run(filepath);
        break;
    case 11:
        Day11.Run(filepath);
        break;
    case 12:
        Day12.Run(filepath);
        break;
}
Console.WriteLine("Completed");