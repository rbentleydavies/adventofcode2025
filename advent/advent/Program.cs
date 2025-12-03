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
}
Console.WriteLine("Completed");