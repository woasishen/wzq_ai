using System;
using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class Neighbor
    {
        private const int SEARCH_RANGE = 2;
        private readonly Border _border;
        public Neighbor(Border border)
        {
            _border = border;
        }

        public List<Pos> GenPossiblePos(CellStatus curStatus)
        {
            var five = new List<Pos>();
            var doubleFour = new List<Pos>();
            var neighbors = GetNeighbors();

            foreach (var neighbor in neighbors)
            {
                var blackGole = _border.GetBlackPosGole(neighbor);
                var whiteGole = _border.GetWhitePosGole(neighbor);
                if (blackGole >= Evaluate.GOLE_DICT[5] || whiteGole >= Evaluate.GOLE_DICT[5])
                {
                    five.Add(neighbor);
                    continue;
                }
                if (blackGole >= 2 * Evaluate.GOLE_DICT[4] || whiteGole >= 2 * Evaluate.GOLE_DICT[4])
                {
                    doubleFour.Add(neighbor);
                }
            }
            if (five.Any())
            {
                return five;
            }
            if (doubleFour.Any())
            {
                return doubleFour;
            }
            return neighbors;
        }

        private List<Pos> GetNeighbors()
        {
            var minX = Math.Max(_border.MinX() - SEARCH_RANGE, 0);
            var maxX = Math.Min(_border.MaxX() + SEARCH_RANGE, GlobalConst.BORDER_SIZE);
            var minY = Math.Max(_border.MinY() - SEARCH_RANGE, 0);
            var maxY = Math.Min(_border.MaxY() + SEARCH_RANGE, GlobalConst.BORDER_SIZE);
            var result = new List<Pos>();
            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    if (_border.GetBlackPosGole(i, j) + _border.GetWhitePosGole(i, j) >
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
