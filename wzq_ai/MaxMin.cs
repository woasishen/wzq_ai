namespace wzq_ai
{
    public class MaxMin
    {
        private const int DEPTH = 4;
        private readonly Border border;
        private readonly Neighbor neighbor;

        public MaxMin(Border border)
        {
            this.border = border;
            neighbor = new Neighbor(border);
        }

        public Pos FindBestPos()
        {
            var neighbors = neighbor.GenNeighbors();
            foreach (var neighbor in neighbors)
            {
                
            }
        }
    }
}
