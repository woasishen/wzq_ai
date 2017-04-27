using System;
using System.Collections.Generic;
using System.Linq;
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
            var isMaxLayer = Configs.Depth % 2 == 1;

            var posGole = new List<GolePos>();
            var neighbors = _neighbor.GenPossiblePos(curStatus);
            foreach (Pos pos in neighbors)
            {
                _border.PutChess(pos, curStatus, Configs.ShowStep);
                var tempGole = ComputeMaxMin(0, CellStatusHelper.Not(curStatus));
                _border.UnPutChess(Configs.ShowStep);
                posGole.Add(new GolePos(tempGole, pos));
            }
            if (isMaxLayer)
            {
                posGole.Sort((i, j) => i.Gole > j.Gole ? -1 : 1);
            }
            else
            {
                posGole.Sort((i, j) => j.Gole > i.Gole ? -1 : 1);
            }
            ComputeFinished.Invoke(_computeTimes, DateTime.Now - tempTime);

            Configs.LogMsg($"搜索完成，共递归{_computeTimes}次，ab剪枝{_abCut}次");
            return posGole.First().Pos;
        }

        private int ComputeMaxMin(int deep, CellStatus curStatus, int? alpha = null, int? beta = null)
        {
            if (deep == Configs.Depth)
            {
                _computeTimes++;
                return _border.GetRoleGole(curStatus);
            }
            int? best = null;
            var isMaxLayer = (Configs.Depth - deep)%2 == 0;
            var neighbors = _neighbor.GenPossiblePos(curStatus);
            for (int i = 0; i < neighbors.Count; i++)
            {
                switch (curStatus)
                {
                    case CellStatus.Black:
                        if (_border.GetPosBlackGole(neighbors[i]) >= Evaluate.GOLE_DICT[5])
                            return Math.Sign(Math.Pow(-1, Configs.Depth - deep)) * (int.MaxValue - 1);
                        break;
                    case CellStatus.White:
                        if (_border.GetPosWhiteGole(neighbors[i]) >= Evaluate.GOLE_DICT[5])
                            return Math.Sign(Math.Pow(-1, Configs.Depth - deep)) * (int.MaxValue - 1);
                        break;
                }
            }

            for (var i = 0; i < neighbors.Count; i++)
            {
                _border.PutChess(neighbors[i], curStatus, Configs.ShowStep);
                var tempGole = ComputeMaxMin(
                    deep + 1,
                    CellStatusHelper.Not(curStatus),
                    isMaxLayer ? int.MinValue : best,
                    isMaxLayer ? best : int.MaxValue);
                _border.UnPutChess(Configs.ShowStep);
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
