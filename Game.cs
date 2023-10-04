using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife
{
    public class Game
    {
        private readonly Grid _grid;
        private readonly IRenderer _renderer;

        public Game(int rowCount, int columnCount, IEnumerable<CellPosition> seed, IRenderer renderer)
        {
            _grid = new Grid(rowCount, columnCount);
            foreach (var cellPosition in seed)
                _grid.CellAt(cellPosition).IsAlive = true;
            _renderer = renderer;
        }

        public void Run(int iterationCount)
        {
            ShowState();
            for (int i = 0; i < iterationCount; i++)
            {
                CalculateNextIteration();
                ShowState();
            }
        }

        private void CalculateNextIteration()
        {
            foreach (var cellPosition in _grid.AllCellPositions())
            {
                int livingNeighbors = _grid.NeighborsOfCellAt(cellPosition).Count(neighborCell => neighborCell.IsAlive);
                _grid.CellAt(cellPosition).CalculateNextState(livingNeighbors);
            }
            foreach (var cellId in _grid.AllCellPositions())
            {
                _grid.CellAt(cellId).SwitchToNextState();
            }
        }

        private void ShowState()
        {
            foreach (var cellPosition in _grid.AllCellPositions())
            {
                _renderer.SetCellState(cellPosition, _grid.CellAt(cellPosition).IsAlive);
            }
            _renderer.ShowState();
        }
    }
}
