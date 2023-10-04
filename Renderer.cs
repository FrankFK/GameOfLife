using System.Collections.Generic;
using System.Threading;
using Woopec.Core;

namespace GameOfLife
{
    internal class Renderer : IRenderer
    {
        private readonly Figure[,] _cells;

        public Renderer(int rowCount, int columnCount, int cellSize)
        {
            Shape shape = CreateCellShape(cellSize);
            _cells = CreateAllCells(rowCount, columnCount, cellSize, shape);
        }

        public void SetCellState(CellPosition cellId, bool isVisible)
        {
            var figure = _cells[cellId.Row, cellId.Column];
            figure.IsVisible = isVisible;
        }

        public void ShowState()
        {
            Thread.Sleep(10);
        }

        private static Shape CreateCellShape(int cellSize)
        {
            var coor = cellSize / 2.0;
            var polygon = new List<Vec2D>() { (-coor, -coor), (-coor, coor), (coor, coor), (coor, -coor)};
            return new Shape(polygon);
        }

        private static Figure[,] CreateAllCells(int rowCount, int columnCount, int cellSize, Shape shape)
        {
            var cells = new Figure[rowCount, columnCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    cells[row, column] = CreateFigureAt(row, column, rowCount, columnCount, cellSize, shape);
                }
            }
            return cells;
        }

        private static Figure CreateFigureAt(int row, int column, int rowCount, int columnCount, int cellSize, Shape shape)
        {
            var figure = new Figure() { Shape = shape, Color = Colors.Blue };
            figure.SetPosition((column - columnCount / 2) * cellSize, (rowCount / 2 - row) * cellSize);
            return figure;
        }
    }
}
