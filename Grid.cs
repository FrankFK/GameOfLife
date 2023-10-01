using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Woopec.Core;

namespace GameOfLife
{
    internal class Cell
    {
        private bool _willBeAlive;

        public bool IsAlive { get; set; }

        public void SetNextState(int livingNeighbours)
        {
            _willBeAlive = WillBeAlive(livingNeighbours);
        }

        public void SwitchToNextState()
        {
            IsAlive = _willBeAlive;
        }

        private bool WillBeAlive(int livingNeighbours)
        {
            if (IsAlive)
            {
                if (livingNeighbours == 2 || livingNeighbours == 3)
                    return true;
                else 
                    return false;
            }
            else
            {
                if (livingNeighbours == 3)
                    return true;
                else 
                    return false;
            }

        }

    }

    internal record CellId(int Row, int Column);


    internal class Grid
    {
        private readonly Cell[,] _cells;

        public int MaxRow { get { return _cells.GetLength(0); } }
        public int MaxColumn { get { return _cells.GetLength(0); } }

        public Grid(int rowCount, int columnCount)
        {
            _cells= new Cell[rowCount, columnCount];
            foreach (var cellId in AllCellsIds())
            {
                _cells[cellId.Row, cellId.Column] = new Cell();
            }
        }

        public IEnumerable<Cell> NeighboursOfCellAt(CellId cellId)
        {
            var neighbours = new List<Cell>();
            for (int row = Math.Max(0, cellId.Row - 1); row <= Math.Min(MaxRow-1, cellId.Row + 1); row++)
            {
                for (int col = Math.Max(0, cellId.Column - 1); col <= Math.Min(MaxColumn-1, cellId.Column + 1); col++)
                {
                    if (row != cellId.Row || col != cellId.Column)
                        neighbours.Add(_cells[row, col]);
                }
            }
            return neighbours;
        }

        public Cell CellAt(CellId cellId) => _cells[cellId.Row, cellId.Column];

        public IEnumerable<CellId> AllCellsIds()
        {
            for (int row = 0; row < MaxRow; row++)
            {
                for (int column = 0; column < MaxColumn; column++)
                {
                    yield return new CellId(row, column);
                }
            }
        }
    }

    internal class Game
    {
        private readonly Grid _grid;

        public Game(int rowCount, int columnCount)
        {
            _grid = new Grid(rowCount, columnCount);
        }

        public void NextIteration()
        {
            foreach (var cellId in _grid.AllCellsIds())
            {
                int livingNeighbours = _grid.NeighboursOfCellAt(cellId).Count(neighbourCell => neighbourCell.IsAlive);
                _grid.CellAt(cellId).SetNextState(livingNeighbours);
            }
            foreach (var cellId in _grid.AllCellsIds())
            {
                _grid.CellAt(cellId).SwitchToNextState();
            }
        }

        public void SetSeed(string seedString)
        {
            foreach(var cellId in AliveCellIdsOfSeed(seedString))
                _grid.CellAt(cellId).IsAlive = true;
        }

        public IEnumerable<CellId> AllCellsIds() => _grid.AllCellsIds();

        public Cell CellAt(CellId cellId) => _grid.CellAt(cellId);

        private IEnumerable<CellId> AliveCellIdsOfSeed(string seedString)
        {
            const char aliveMarker = 'X';
            var lines = seedString.Split('\n');
            int row = 0;
            foreach(var line in lines) 
            {
                int column = line.IndexOf(aliveMarker);
                while (column != -1)
                {
                    yield return new CellId(row, column);
                    column = line.IndexOf(aliveMarker, column + 1);
                }
                row++;
            }

        }
    }

    internal class Renderer
    {
        private readonly Figure[,] _cells;

        private const string _shapeName = "cellShape";

        public Renderer(int rowCount, int columnCount, int cellSize) 
        {
            CreateShape(cellSize);
            _cells= new Figure[rowCount, columnCount];
            for(int row = 0; row < rowCount; row++)
                for(int col = 0; col < columnCount; col++)
                {
                    _cells[row, col] = CreateFigureAt(new CellId(row, col), rowCount, columnCount, cellSize);
                }
        }

        public void SetCell(CellId cellId, bool isVisible) 
        {
            var figure = _cells[cellId.Row, cellId.Column];
            figure.IsVisible = isVisible;
        }

        private void CreateShape(int cellSize)
        {
            var pen = new Pen();
            pen.Heading = 90;
            pen.BeginPoly();
            for (int i = 0; i < 4; i++)
            {
                pen.Move(cellSize);
                pen.Rotate(90);
            }
            Shapes.Add(_shapeName, new Shape(pen.EndPoly()));
        }

        private Figure CreateFigureAt(CellId cellId, int rowCount, int columnCount, int cellSize)
        {
            var figure = new Figure() { Shape = Shapes.Get(_shapeName), Color = Colors.Blue };
            figure.SetPosition(((cellId.Column - columnCount / 2) * cellSize, (rowCount / 2 - cellId.Row) * cellSize));
            figure.IsVisible = false;
            return figure;
        }
    }
}
