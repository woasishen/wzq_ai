using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace wzq_ai
{
    public class MaxMin
    {
        private readonly Border _border;
        private readonly Neighbor _neighbor;
        private readonly Random _random;
        private int _computeTimes;
        private int _abCut;
        public Action<int, TimeSpan> ComputeFinished;

        public MaxMin(Border border)
        {
            _border = border;
            _neighbor = new Neighbor(border);
            _random = new Random(DateTime.Now.Millisecond);
        }

        public Pos FindBestPos(CellStatus curStatus)
        {
            _computeTimes = 0;
            _abCut = 0;
            var tempTime = DateTime.Now;

            var maxGolePosList = new List<Pos>();
            var maxGole = int.MinValue;
            var neighbors = _neighbor.GenPossiblePos(curStatus);
            foreach (Pos pos in neighbors)
            {
                _border.PutChess(pos, curStatus, Configs.SHOW_STEP);
                var tempGole = Math.Sign(Math.Pow(-1, Configs.DEPTH - 1)) * 
                    ComputeMaxMin(0, CellStatusHelper.Not(curStatus));
                _border.UnPutChess(Configs.SHOW_STEP);
                if (tempGole == maxGole)
                {
                    maxGolePosList.Add(pos);
                }
                else if (tempGole > maxGole)
                {
                    maxGolePosList.Clear();
                    maxGole = tempGole;
                    maxGolePosList.Add(pos);
                }
            }

            var index = _random.Next(maxGolePosList.Count);
            ComputeFinished.Invoke(_computeTimes, DateTime.Now - tempTime);

            Configs.LogMsg($"搜索完成，共递归{_computeTimes}次，ab剪枝{_abCut}次");
            return maxGolePosList[index];
        }

        private int ComputeMaxMin(int deep, CellStatus curStatus, int? alpha = null, int? beta = null)
        {
            if (deep == Configs.DEPTH)
            {
                _computeTimes++;
                return _border.GetRoleGole(curStatus);
            }
            int? best = null;
            var isMaxLayer = (Configs.DEPTH - deep)%2 == 1;
            var neighbors = _neighbor.GenPossiblePos(curStatus);

            for (var i = 0; i < neighbors.Count; i++)
            {
                if (_border.PutChess(neighbors[i], curStatus, Configs.SHOW_STEP))
                {
                    _border.UnPutChess(Configs.SHOW_STEP);
                    return Math.Sign(Math.Pow(-1, Configs.DEPTH - deep)) * int.MaxValue;
                }
                var tempGole = ComputeMaxMin(
                    deep + 1,
                    CellStatusHelper.Not(curStatus),
                    isMaxLayer ? int.MinValue : best,
                    isMaxLayer ? best : int.MaxValue);
                _border.UnPutChess(Configs.SHOW_STEP);
                if (best == null)
                {
                    best = tempGole;
                }
                else
                {
                    best = isMaxLayer
                        ? Math.Max(best.Value, tempGole)
                        : Math.Min(best.Value, tempGole);
                }

                if (i != neighbors.Count - 1)
                {
                    if (alpha <= tempGole && tempGole <= beta)
                    {
                        _abCut++;
                        return tempGole;
                    }
                }
            }
            return best ?? 0;
        }
    }
}
