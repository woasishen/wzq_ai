using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class Neighbor
    {
        private const int SEARCH_RANGE = 2;
        private readonly Border border;
        public Neighbor(Border border)
        {
            this.border = border;
        }

        //public List<Pos> GenPossiblePos()
        //{
        //    var neighbors = GetNeighbors();


        //}

        private List<Pos> GetNeighbors()
        {
            var minX = border.MinX() - SEARCH_RANGE;
            var maxX = border.MaxX() + SEARCH_RANGE;
            var minY = border.MinY() - SEARCH_RANGE;
            var maxY = border.MaxY() + SEARCH_RANGE;
            var result = new List<Pos>();
            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    if (border.GetBlackPosGole(i, j) + border.GetWhitePosGole(i, j) >
                        Evaluate.GOLE_DICT[2] * 2)
                    {
                        result.Add(new Pos(i, j));
                    }
                }
            }
            return result;
        }
    }
}
