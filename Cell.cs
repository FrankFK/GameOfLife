namespace GameOfLife
{
    public class Cell
    {
        private bool _willBeAlive;

        public bool IsAlive { get; set; }

        public void CalculateNextState(int livingNeighbors)
        {
            _willBeAlive = WillBeAlive(livingNeighbors);
        }

        public void SwitchToNextState()
        {
            IsAlive = _willBeAlive;
        }

        private bool WillBeAlive(int livingNeighbors)
        {
            if (IsAlive)
            {
                if (livingNeighbors == 2 || livingNeighbors == 3)
                    return true;
                else 
                    return false;
            }
            else
            {
                if (livingNeighbors == 3)
                    return true;
                else 
                    return false;
            }

        }

    }
}
