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
        var connectedJunctions = new List<Junction>();
        var lastShortestJunction = new Junction[2];
        var possibleConnections = new List<Connection>();
        foreach (var junctionA in junctions)
        {
            foreach (var junctionB in junctions)
            {
                if (junctionA.Equals(junctionB)) continue;
                //if (connections.Any(x => x.Any(y => y.Equals(junctionA))
                //                         && x.Any(y => y.Equals(junctionB)))) continue;
                
                var distance = DistanceBetweenJunctions(junctionA, junctionB);
                var possibleConnection = new Connection
                {
                    Junctions = new Junction[] { junctionA, junctionB }
                        .OrderBy(j => j.x)
                        .ThenBy(j => j.y)
                        .ThenBy(j => j.z)
                        .ToArray(),
                    Distance = distance
                };
                // if (!possibleConnections.Any(x =>
                //         x.Junctions[0] == possibleConnection.Junctions[0] &&
                //         x.Junctions[1] == possibleConnection.Junctions[1]))
                {
                    possibleConnections.Add(possibleConnection);
                }
            }
        }
        Console.WriteLine(possibleConnections.Count);
        
        // while(connectedJunctions.Count < junctions.Length)
        // {
        //     foreach (var junctionA in junctions)
        //     {
        //         foreach (var junctionB in junctions)
        //         {
        //             if (junctionA.Equals(junctionB)) continue;
        //             if (connections.Any(x => x.Any(y => y.Equals(junctionA))
        //                                   && x.Any(y => y.Equals(junctionB)))) continue;
        //             var distance = DistanceBetweenJunctions(junctionA, junctionB);
        //             if (distance < shortestDistance)
        //             {
        //                 shortestDistance = distance;
        //                 shortestJunctions = [junctionA, junctionB];
        //             }
        //         }
        //     }
        //
        //     if (shortestJunctions[0] == null) break;
        //     if (!connectedJunctions.Any(y => y.Equals(shortestJunctions[0])))
        //     {
        //         connectedJunctions.Add(shortestJunctions[0]);
        //     }
        //     if (!connectedJunctions.Any(y => y.Equals(shortestJunctions[1])))
        //     {
        //         connectedJunctions.Add(shortestJunctions[1]);
        //     }
        //     //Console.WriteLine($"Shortest distance: {shortestDistance}");
        //     Console.WriteLine($"Shortest Junctions: {shortestJunctions[0]},  {shortestJunctions[1]}");
        //     connections.Add(shortestJunctions);
        //     //AddToCircuit(circuits, shortestJunctions);
        //     shortestDistance = double.MaxValue;
        //     lastShortestJunction = shortestJunctions;
        //     shortestJunctions = new Junction[2];
        //     //Console.WriteLine($"There are {circuits.Count} circuits");
        //     //foreach (var circuit in circuits.OrderByDescending(c=>c.Count))
        //     //{
        //     //    Console.Write($"{circuit.Count} ,");
        //     //}
        //     //Console.WriteLine();
        //     Console.WriteLine(connectedJunctions.Count);
        //
        // }
        //Console.WriteLine(circuits.OrderByDescending(c=>c.Count).Take(3).Aggregate(1,(a,b)=>a * b.Count));
        foreach (var connection in possibleConnections.OrderBy(x => x.Distance))
        {
            AddToCircuit(circuits, connection.Junctions);
            lastShortestJunction = connection.Junctions;
            Console.WriteLine($"Shortest Junctions: {lastShortestJunction[0]},  {lastShortestJunction[1]}");
            Console.WriteLine($"There are {circuits.Count} circuits");
            foreach (var circuit in circuits.OrderByDescending(c=>c.Count))
            {
                Console.Write($"{circuit.Count} ,");
            }
            Console.WriteLine();
            var nonEmptyCircuits = circuits.Where(x => x.Count > 0);
            if (nonEmptyCircuits.Count() == 1 && nonEmptyCircuits.First().Count == junctions.Length)
                break;
        }
        
        Console.WriteLine(junctions.Length);
        Console.WriteLine(lastShortestJunction[0].x * lastShortestJunction[1].x);
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

    public class Connection
    {
        public Junction[] Junctions { get; set; }
        public double Distance { get; set; }
    }
    public class Junction
    {
        public Junction(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 3) throw new Exception();
            x = long.Parse(parts[0]);
            y = long.Parse(parts[1]);
            z = long.Parse(parts[2]);
        }

        public override string ToString()
        {
            return $"{x},{y},{z}";
        }

        public bool Equals(Junction obj)
        {
            return x == obj.x && y == obj.y && z == obj.z;
        }

        public long x = 0;
        public long y = 0;
        public long z = 0;
    }
}