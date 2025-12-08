using System.Runtime.CompilerServices;
using System.Text;

namespace advent;

public static class Day8
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var junctions = lines.Select(x => new Junction(x)).ToArray();
        var shortestDistance = double.MaxValue;
        var shortestJunctions = new Junction[2];
        var circuits = new List<List<Junction>>();
        var connections = new List<Junction[]>();
        for (var i = 0; i < 1000; i++)
        {
            foreach (var junctionA in junctions)
            {
                foreach (var junctionB in junctions)
                {
                    if (junctionA.Equals(junctionB)) continue;
                    if (connections.Any(x => x.Any(y => y.Equals(junctionA))
                                          && x.Any(y => y.Equals(junctionB)))) continue;
                    var distance = DistanceBetweenJunctions(junctionA, junctionB);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        shortestJunctions = [junctionA, junctionB];
                    }
                }
            }

            //Console.WriteLine($"Shortest distance: {shortestDistance}");
            Console.WriteLine($"Shortest Junctions: {shortestJunctions[0]},  {shortestJunctions[1]}");
            connections.Add(shortestJunctions);
            AddToCircuit(circuits, shortestJunctions);
            shortestDistance = double.MaxValue;
            Console.WriteLine($"There are {circuits.Count} circuits");
            foreach (var circuit in circuits.OrderByDescending(c=>c.Count))
            {
                Console.Write($"{circuit.Count} ,");
            }
            Console.WriteLine();
            
        }
        Console.WriteLine(circuits.OrderByDescending(c=>c.Count).Take(3).Aggregate(1,(a,b)=>a * b.Count));

        
        Console.WriteLine(junctions.Length);
    }

    public static void AddToCircuit(List<List<Junction>> circuits, Junction[] junctions)
    {
        var existingCircuits =
            circuits.Where(x => x.Any(y => y.Equals(junctions[0]) || y.Equals(junctions[1])));
        if (existingCircuits.Count() == 2)
        {
            //join the circuits together
            existingCircuits.First().AddRange(existingCircuits.Last());
            existingCircuits.Last().Clear();
        }
        var existingCircuit = existingCircuits.FirstOrDefault();
        if (existingCircuit != null)
        {
            if (!existingCircuit.Any(y => y.Equals(junctions[0])))
            {
                existingCircuit.Add(junctions[0]);
                Console.WriteLine($"Circuit {junctions[0]} added to existing circuit");
                foreach (var junction in existingCircuit)
                {
                    Console.WriteLine($" - {junction}");
                }
                
            }

            if (!existingCircuit.Any(y => y.Equals(junctions[1])))
            {
                existingCircuit.Add(junctions[1]);
                Console.WriteLine($"Circuit {junctions[1]} added to existing circuit:");
                foreach (var junction in existingCircuit)
                {
                    Console.WriteLine($" - {junction}");
                }
            }
        }
        else
        {
            circuits.Add([junctions[0], junctions[1]]);
            Console.WriteLine($"New circuit created");
        }
    }

    public static double DistanceBetweenJunctions(Junction junctionA, Junction junctionB)
    {
        return Math.Sqrt(Math.Pow(junctionA.x - junctionB.x, 2) + Math.Pow(junctionA.y - junctionB.y, 2) +
                         Math.Pow(junctionA.z - junctionB.z, 2));
    }

    public class Junction
    {
        public Junction(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 3) throw new Exception();
            x = int.Parse(parts[0]);
            y = int.Parse(parts[1]);
            z = int.Parse(parts[2]);
        }

        public override string ToString()
        {
            return $"{x},{y},{z}";
        }

        public bool Equals(Junction obj)
        {
            return x == obj.x && y == obj.y && z == obj.z;
        }

        public int x = 0;
        public int y = 0;
        public int z = 0;
    }
}