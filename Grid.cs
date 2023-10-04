using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class Grid
    {
        private readonly Cell[,] _cells;

        private readonly int _rowCount;

        private readonly int _columnCount;

        public Grid(int rowCount, int columnCount)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
            _cells = new Cell[rowCount, columnCount];
            foreach (var cellPosition in AllCellPositions())
            {
                _cells[cellPosition.Row, cellPosition.Column] = new Cell();
            }
        }

        public Cell CellAt(CellPosition cellPosition) => _cells[cellPosition.Row, cellPosition.Column];

        public IEnumerable<CellPosition> AllCellPositions()
        {
            for (int row = 0; row < _rowCount; row++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    yield return new CellPosition(row, column);
                }
            }
        }

        public List<Cell> NeighborsOfCellAt(CellPosition cellPosition)
        {
            var neighbors = new List<Cell>();
            for (int row = Math.Max(0, cellPosition.Row - 1); row <= Math.Min(_rowCount - 1, cellPosition.Row + 1); row++)
            {
                for (int col = Math.Max(0, cellPosition.Column - 1); col <= Math.Min(_columnCount - 1, cellPosition.Column + 1); col++)
                {
                    if (row != cellPosition.Row || col != cellPosition.Column)
                        neighbors.Add(CellAt( new CellPosition(row, col)));
                }
            }
            return neighbors;
        }

    }
}
