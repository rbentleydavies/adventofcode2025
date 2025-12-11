namespace advent;

public static class Day11
{
    public static void Run(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var nodes = new Dictionary<string, Node>();
        foreach (var line in lines)
        {
            var key = line.Split(": ")[0].Trim();
            if (!nodes.ContainsKey(key))
                nodes[key] = new Node { Name = key };
        }

        nodes["out"] = new Node { Name = "out" };
        foreach (var line in lines)
        {
            var key = line.Split(": ")[0].Trim();
            var outputs = line.Split(": ")[1].Trim().Split(" ").Select(s => s.Trim());
            foreach (var output in outputs) nodes[key].Outputs.Add(nodes[output]);
        }

        //var startNode = nodes["you"];
        //startNode.RoutesToHereFromStart = 1;

        //CountRoutes(startNode);
        // foreach (var node in nodes)
        // { 
        //     Console.WriteLine($"{node.Key}: {node.Value.RoutesToOutFromHere}");
        // }
        var dacTofft = nodes["dac"].RoutesFromHere("fft");
        Console.WriteLine($"dac to fft: {dacTofft}");
        var fftTodac = nodes["fft"].RoutesFromHere("dac");
        Console.WriteLine($"fft to dac: {fftTodac}");
        var svrTodac = nodes["svr"].RoutesFromHere("dac");
        Console.WriteLine($"svr to dac: {svrTodac}");
        var svrTofft = nodes["svr"].RoutesFromHere("fft");
        Console.WriteLine($"svr to fft: {svrTofft}");
        var fftToout = nodes["fft"].RoutesFromHere("out");
        Console.WriteLine($"fft to out: {fftToout}");
        var dacToout = nodes["dac"].RoutesFromHere("out");
        Console.WriteLine($"dac to out: {dacToout}");
        Console.WriteLine($"both (d->f): {svrTodac * dacTofft * fftToout}");
        Console.WriteLine($"both (f-d): {svrTofft * fftTodac * dacToout}");
        Console.WriteLine($"total both: {svrTodac * dacTofft * fftToout + svrTofft * fftTodac * dacToout}");
        //var allRoutes = nodes["svr"].RoutesToOutFromHere;
        //Console.WriteLine($"all routes: {allRoutes}");
        //var noDacFft = nodes["svr"].RoutesToOutFromHereNoDACFFT;
        //var noDac = nodes["svr"].RoutesToOutFromHereNoDAC;
        //var noFft = nodes["svr"].RoutesToOutFromHereNoFFT;
        //Console.WriteLine($"no dac fft: {noDacFft}");
        //Console.WriteLine($"no dac: {noDac}");
        //Console.WriteLine($"no fft: {noFft}");
        //Console.WriteLine($"both: {allRoutes - noDac - noFft + noDacFft}");
        //Console.WriteLine(nodes["svr"].RoutesToOutFromHereNoFFT);
        //Console.WriteLine(nodes["svr"].RoutesToOutFromHereNoDAC);
    }

    // public static void CountRoutes(Node node)
    // {
    //     //Console.WriteLine(node.Name);
    //     foreach (var output in node.Outputs)
    //     {
    //         output.RoutesToHereFromStart += node.RoutesToHereFromStart;
    //         CountRoutes(output);
    //     }
    // }

    public class Node
    {
        public string Name { get; set; }
        public List<Node> Outputs { get; set; } = [];
        private Dictionary<string, long> _cache = new();
        
        public long RoutesFromHere(string toNodeName)
        {
            if (Name == toNodeName) return 1;
            if (_cache.ContainsKey(toNodeName))
            {
                return _cache[toNodeName];
            }
            long total = 0;
            foreach (var output in Outputs) total += output.RoutesFromHere(toNodeName);
            _cache[toNodeName] = total;
            return total;
        }
    }
}