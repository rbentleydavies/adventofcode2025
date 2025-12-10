namespace advent;

public static class Day9
{
    private static long MAXTILESX;
    private static long MAXTILESY;

    public static void Run(string fileName)
    {
        var cornerTiles = File.ReadAllLines(fileName)
            .Select(x => x.Split(',').Select(y => long.Parse(y)).ToArray())
            .ToArray();
        MAXTILESX = cornerTiles.Max(x => x[0]);
        MAXTILESY = cornerTiles.Max(x => x[1]);
        var colours = new char[MAXTILESX + 1][];
        for(var i=0; i<=MAXTILESX; i++)
            colours[i] = new char[MAXTILESY+1];
        var tiles = new List<long[]>();
        for (var i = 0; i < cornerTiles.Length; i++)
        {
            colours[cornerTiles[i][0]][cornerTiles[i][1]] = '#';
            var tileId1 = i;
            var tileId2 = i + 1;
            if (tileId2 == cornerTiles.Length) tileId2 = 0;
            if (cornerTiles[tileId1][0] == cornerTiles[tileId2][0])
                for (var j = Math.Min(cornerTiles[tileId1][1], cornerTiles[tileId2][1]);
                     j <= Math.Max(cornerTiles[tileId1][1], cornerTiles[tileId2][1]);
                     j++)
                {
                    tiles.Add([cornerTiles[tileId1][0], j]);
                    colours[cornerTiles[tileId1][0]][ j] = 'X';
                }
            else
                for (var j = Math.Min(cornerTiles[tileId1][0], cornerTiles[tileId2][0]);
                     j <= Math.Max(cornerTiles[tileId1][0], cornerTiles[tileId2][0]);
                     j++)
                {
                    tiles.Add([j, cornerTiles[tileId1][1]]);
                    colours[j][ cornerTiles[tileId1][1]] = 'X';
                }
            
        }

        Console.WriteLine($"There are {tiles.Count} tiles in the lines");
        //foreach (var tile in tiles)
        // {
        //     Console.WriteLine($"{tile[0]}, {tile[1]}");
        // }
        Console.WriteLine($"There are {cornerTiles.Length} tiles in the corners");
        ColourTheInsideTiles(cornerTiles.ToList(), colours);
        //Console.WriteLine($"There are {insideTiles.Count} tiles inside");

        var possibleAreas = new List<TileArea>();
        var progress = 0;
        foreach (var tileA in cornerTiles)
        {
            progress++;
            Console.WriteLine(progress);
            foreach (var tileB in cornerTiles)
                if (AllTilesAreHere(tileA, tileB, colours))
                    possibleAreas.Add(new TileArea(tileA, tileB));
        }

        //foreach (var area in possibleAreas.OrderBy(x => x.Area))
        var area = possibleAreas.OrderBy(x => x.Area).Last();
            Console.WriteLine($"{area.TileA[0]}, {area.TileA[1]} - {area.TileB[0]}, {area.TileB[1]} : {area.Area}");
    }

    public static bool AllTilesAreHere(long[] tileA, long[] tileB, char[][] colours)
    {
        if (tileA[0] == tileB[0] && tileA[1] == tileB[1]) return false;
        var areaX1 = Math.Min(tileA[0], tileB[0]);
        var areaY1 = Math.Min(tileA[1], tileB[1]);
        var areaX2 = Math.Max(tileA[0], tileB[0]);
        var areaY2 = Math.Max(tileA[1], tileB[1]);
        for (var i = areaX1; i <= areaX2; i++)
        {
            if (!(colours[i][ areaY1]=='#' || colours[i][ areaY1] == 'X'))
                return false;
            if (!(colours[i][ areaY2]=='#' || colours[i][ areaY2] == 'X'))
                return false;
        }

        for (var j = areaY1; j <= areaY2; j++)
        {
            if (!(colours[areaX1][ j]=='#' || colours[areaX1][ j] == 'X'))
                return false;
            if (!(colours[areaX2][ j]=='#' || colours[areaX2][ j] == 'X'))
                return false;
        }


        return true;
    }

    public static bool AreaContainsTile(long[] tileA, long[] tileB, IEnumerable<long[]> allTiles)
    {
        if (tileA[0] == tileB[0] && tileA[1] == tileB[1]) return true;
        var areaX1 = Math.Min(tileA[0], tileB[0]);
        var areaY1 = Math.Min(tileA[1], tileB[1]);
        var areaX2 = Math.Max(tileA[0], tileB[0]);
        var areaY2 = Math.Max(tileA[1], tileB[1]);

        foreach (var tile in allTiles)
            if (tile[0] > areaX1 && tile[0] < areaX2 && tile[1] > areaY1 && tile[1] < areaY2)
                return true;

        return false;
    }

    public static long CarpetAreaSize(long[] tileA, long[] tileB)
    {
        if (tileA.Length != 2 || tileB.Length != 2) return 0;
        return Math.Abs((tileA[0] - tileB[0] + 1) * (tileA[1] - tileB[1] + 1));
    }

    private static void ColourTheInsideTiles(List<long[]> cornerTiles, char[][] colours)
    {
        //find the first inside tile...
        var topLeftCorner = cornerTiles.OrderBy(t => Math.Pow(t[0], 2) + Math.Pow(t[1], 2)).First();
        Console.WriteLine($"Top Left Corner: {topLeftCorner[0]}, {topLeftCorner[1]}");
        //var insideTiles = new List<long[]>();
        var uncheckedInsideTiles = new List<long[]>();
        //insideTiles.Add([topLeftCorner[0] + 1, topLeftCorner[1] + 1]);
        uncheckedInsideTiles.Add([topLeftCorner[0] + 1, topLeftCorner[1] + 1]);
        long insideTilesCount = 0;
        while (uncheckedInsideTiles.Count >0)
        {
            //insideTilesCount = insideTiles.Count;
            var newUncheckedInsideTiles = new List<long[]>();
            
            Console.WriteLine($"Inside Tiles to check: {uncheckedInsideTiles.Count}");
            foreach (var tile in uncheckedInsideTiles.ToArray())
            foreach (var neighbourTile in TileNeighbours(tile))
                if (colours[neighbourTile[0]][ neighbourTile[1]] != '#' && colours[neighbourTile[0]][ neighbourTile[1]] != 'X')
                {
                    //Console.WriteLine($"{neighbourTile[0]}, {neighbourTile[1]} - is not a line or corner");
                    colours[neighbourTile[0]][ neighbourTile[1]] = 'X';
                    newUncheckedInsideTiles.Add(neighbourTile);
                }
            uncheckedInsideTiles = newUncheckedInsideTiles;
        }

    }

    private static bool IsInList(List<long[]> list, long[] tile)
    {
        return list.Any(x => x[0] == tile[0] && x[1] == tile[1]);
    }

    private static long[][] TileNeighbours(long[] tile)
    {
        var neighbours = new List<long[]>();
        if (tile[0] > 0) neighbours.Add([tile[0] - 1, tile[1]]);
        if (tile[1] > 0) neighbours.Add([tile[0], tile[1] - 1]);
        if (tile[0] < MAXTILESX) neighbours.Add([tile[0] + 1, tile[1]]);
        if (tile[1] < MAXTILESY) neighbours.Add([tile[0], tile[1] + 1]);
        return neighbours.ToArray();
    }

    public class Tile
    {
        public long X { get; set; }
        public long Y { get; set; }
        public char Colour { get; set; }
        public Tile(long x, long y, char colour)
        {
            X = x;
            Y = y;
            Colour = colour;
        }
    }
    public class TileArea
    {
        public TileArea(long[] tileA, long[] tileB)
        {
            TileA = tileA;
            TileB = tileB;
            Area = CarpetAreaSize(tileA, tileB);
        }

        public long[] TileA { get; }
        public long[] TileB { get; }
        public long Area { get; }
    }
}