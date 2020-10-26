namespace Refactoring.Conway
{
    class Cell
    {
        public bool IsAlive { get; set; }

        public Cell(bool isAlive)
        {
            IsAlive = isAlive;
        }
        
        public override string ToString()
        {
            return (IsAlive ? " . " : " 0 ");
        }
    }
}
