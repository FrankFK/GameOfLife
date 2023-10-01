using GameOfLife;
using System.Threading;
using Woopec.Core;

internal class Program
{
    public static void WoopecMain()
    {
        int rowCount = 120;
        int columnCount = 120;
        var game = new Game(rowCount, columnCount);
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
       .............XX.
";
        game.SetSeed(gosperGilderGun);
        while(true)
        {
            foreach (var cellId in game.AllCellsIds())
            {
                renderer.SetCell(cellId, game.CellAt(cellId).IsAlive);
            }
            game.NextIteration();
            Thread.Sleep(5);
        }
    }

}