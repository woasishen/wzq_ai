using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var neighbors = GetNeighbors();

            neighbors.Sort(
                (i, j) => 
                _border.GetBlackPosGole(j) + 
                _border.GetWhitePosGole(j) - 
                _border.GetBlackPosGole(i) - 
                _border.GetWhitePosGole(i));

            return neighbors.Take(8).ToList();
        }

        private List<Pos> GetNeighbors()
        {
            var minX = Math.Max(_border.MinX() - SEARCH_RANGE, 0);
            var maxX = Math.Min(_border.MaxX() + SEARCH_RANGE, Configs.BORDER_SIZE);
            var minY = Math.Max(_border.MinY() - SEARCH_RANGE, 0);
            var maxY = Math.Min(_border.MaxY() + SEARCH_RANGE, Configs.BORDER_SIZE);
            var result = new List<Pos>();
            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    if (IsValidForNeighbor(i, j))
                    {
                        result.Add(new Pos(i, j));
                    }
                }
            }
            // 没有2个在附近的棋子
            if (!result.Any())
            {
                for (var i = minX; i < maxX; i++)
                {
                    for (var j = minY; j < maxY; j++)
                    {
                        if (IsValidForNeighborSpecial(i, j))
                        {
                            result.Add(new Pos(i, j));
                        }
                    }
                }
            }
            return result;
        }

        private bool IsValidForNeighbor(int x, int y)
        {
            if (_border.GetCellStatus(x, y) != CellStatus.Empty)
            {
                return false;
            }
            int neighborCount = 0;
            var minX = Math.Max(x - SEARCH_RANGE, 0);
            var maxX = Math.Min(x + SEARCH_RANGE, Configs.BORDER_SIZE - 1);
            var minY = Math.Max(y - SEARCH_RANGE, 0);
            var maxY = Math.Min(y + SEARCH_RANGE, Configs.BORDER_SIZE - 1);
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if (_border.GetCellStatus(i, j) != CellStatus.Empty)
                    {
                        neighborCount++;
                        if (neighborCount == 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsValidForNeighborSpecial(int x, int y)
        {
            if (_border.GetCellStatus(x, y) != CellStatus.Empty)
            {
                return false;
            }
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i < 0 || i >= Configs.BORDER_SIZE 
                        || j < 0 || j >= Configs.BORDER_SIZE)
                    {
                        continue;
                    }
                    if (_border.GetCellStatus(i, j) != CellStatus.Empty)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
