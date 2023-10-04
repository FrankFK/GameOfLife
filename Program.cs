using GameOfLife;
using System.Collections.Generic;
using Woopec.Core;

internal class Program
{
    public static void WoopecMain()
    {
        int rowCount = 120;
        int columnCount = 120;
        var renderer = new Renderer(rowCount, columnCount, 6);
        string gosperGilderGun = @"
        .........................X
        .......................X.X
        .............XX......XX............XX
        ............X...X....XX............XX
        .XX........X.....X...XX
        .XX........X...X.XX....X.X
        ...........X.....X.......X
        ............X...X........
        .............XX.";
        var seed = AliveCellPositionsOfSeed(gosperGilderGun);
        var game = new Game(rowCount, columnCount, seed, renderer);
        game.Run(iterationCount: 76);
        Screen.Default.Bye();   
    }

    private static IEnumerable<CellPosition> AliveCellPositionsOfSeed(string seedString)
    {
        const char aliveMarker = 'X';
        var lines = seedString.Split('\n');
        int row = 0;
        foreach (var line in lines)
        {
            int column = line.IndexOf(aliveMarker);
            while (column != -1)
            {
                yield return new CellPosition(row, column);
                column = line.IndexOf(aliveMarker, column + 1);
            }
            row++;
        }
    }

}