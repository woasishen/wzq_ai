using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace wzq_ai
{
    public class MaxMin
    {
        private const int DEPTH = 4;
        private readonly Border _border;
        private readonly Neighbor _neighbor;
        private readonly Random _random;

        public MaxMin(Border border)
        {
            _border = border;
            _neighbor = new Neighbor(border);
            _random = new Random(DateTime.Now.Millisecond);
        }

        public Pos FindBestPos(CellStatus curStatus)
        {
            var maxGolePosList = new List<Pos>();
            var maxGole = int.MinValue;
            var neighbors = _neighbor.GenPossiblePos(curStatus);
            for (var i = 0; i < neighbors.Count; i++)
            {
                _border.PutChess(neighbors[i], curStatus);
                var tempGole = -ComputeMaxMin(CellStatusHelper.Not(curStatus), 1);
                _border.UnPutChess();
                if (tempGole == maxGole)
                {
                    maxGolePosList.Add(neighbors[i]);
                }
                else if (tempGole > maxGole)
                {
                    maxGolePosList.Clear();
                    maxGole = tempGole;
                    maxGolePosList.Add(neighbors[i]);
                }
            }

            var index = _random.Next(maxGolePosList.Count);

            return maxGolePosList[index];
        }

        private int ComputeMaxMin(CellStatus curStatus, int deep)
        {
            var maxGole = int.MinValue;
            var neighbors = _neighbor.GenPossiblePos(curStatus);

            for (var i = 0; i < neighbors.Count; i++)
            {
                var tempGole = _border.GetRoleGole(curStatus);
                if (tempGole < Evaluate.GOLE_DICT[5] && deep < DEPTH)
                {
                    _border.PutChess(neighbors[i], curStatus);
                    tempGole = -ComputeMaxMin(CellStatusHelper.Not(curStatus), deep + 1);
                    _border.UnPutChess();
                }
                if (tempGole >= Evaluate.GOLE_DICT[5])
                {
                    return tempGole;
                }
                maxGole = Math.Max(maxGole, tempGole);
            }
            return maxGole;
        }
    }
}
