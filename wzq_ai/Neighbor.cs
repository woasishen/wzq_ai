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
            var five = new List<GolePos>();
            var other = new List<GolePos>();
            for (int i = 0; i < Configs.BORDER_SIZE; i++)
            {
                for (int j = 0; j < Configs.BORDER_SIZE; j++)
                {
                    var posGole = _border.GetPosGole(i, j, curStatus);
                    if (posGole >= Evaluate.GOLE_DICT[5])
                    {
                        five.Add(new GolePos(posGole, new Pos(i, j)));
                    }
                    else
                    {
                        other.Add(new GolePos(posGole, new Pos(i, j)));
                    }

                }
            }
            if (five.Any())
            {
                return five.Select(s=>s.Pos).ToList();
            }
            other.Sort((i, j) => j.Gole - i.Gole);
            return other.Take(8).Select(s=>s.Pos).ToList();
        }
    }
}
