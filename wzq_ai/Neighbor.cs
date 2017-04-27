using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace wzq_ai
{
    public class Neighbor
    {
        private readonly Border _border;
        public Neighbor(Border border)
        {
            _border = border;
        }

        public List<Pos> GenPossiblePos(CellStatus curStatus)
        {
            var five = new List<Pos>();
            var four = new List<Pos>();
            var other = new List<GolePos>();
            for (int i = 0; i < Configs.BORDER_SIZE; i++)
            {
                for (int j = 0; j < Configs.BORDER_SIZE; j++)
                {
                    var posGole = _border.GetPosGole(i, j, curStatus);
                    if (posGole >= Evaluate.GOLE_DICT[4])
                    {
                        var black = _border.GetPosBlackGole(i, j);
                        var white = _border.GetPosWhiteGole(i, j);

                        if (black >= Evaluate.GOLE_DICT[5] || white >= Evaluate.GOLE_DICT[5])
                        {
                            if ((curStatus == CellStatus.Black && black >= Evaluate.GOLE_DICT[5]) ||
                                (curStatus == CellStatus.White && white >= Evaluate.GOLE_DICT[5]))
                            {
                                five.Clear();
                                five.Add(new Pos(i, j));
                                break;
                            }
                            five.Add(new Pos(i, j));
                            continue;
                        }
                        if (black + white >= 2 * Evaluate.GOLE_DICT[4])
                        {
                            if ((curStatus == CellStatus.Black && black >= 2* Evaluate.GOLE_DICT[4]) ||
                                (curStatus == CellStatus.White && white >= 2* Evaluate.GOLE_DICT[4]))
                            {
                                four.Clear();
                                four.Add(new Pos(i, j));
                                break;
                            }
                            four.Add(new Pos(i, j));
                            continue;
                        }
                    }
                    other.Add(new GolePos(posGole, new Pos(i, j)));
                }
            }
            if (five.Any())
            {
                return five;
            }
            if (four.Any())
            {
                return four;
            }
            other.Sort((i, j) => j.Gole - i.Gole);
            return other.Take(8).Select(s => s.Pos).ToList();
        }
    }
}
