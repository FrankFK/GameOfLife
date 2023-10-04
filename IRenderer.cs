namespace GameOfLife
{
    public interface IRenderer
    {
        void SetCellState(CellPosition cellId, bool isVisible);

        void ShowState();
    }
}