using System.Collections.Generic;

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

        public Pos FindBestPos(CellStatus curStatus)
        {
            var neighbors = neighbor.GenPossiblePos(curStatus);
            foreach (var neighbor in neighbors)
            {

            }
        }

        private int MaxMin(CellStatus curStatus, int deep)
        {
            List<Pos> maxGolePosList;
            int maxGole = int.MinValue;
            var neighbors = neighbor.GenPossiblePos(curStatus);
            for (int i = 0; i < neighbors.Count; i++)
            {
                int tempGole;
                if (deep == DEPTH)
                {
                    tempGole = 
                }
                border.PutChess(neighbors[i], curStatus);
                
            }
        }
    }
}
