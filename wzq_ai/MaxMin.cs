using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        private readonly int defaultDepth;
        private readonly Evaluate evaluate;
        public MaxMin(int defaultDepth, int width, int height)
        {
            this.defaultDepth = defaultDepth;
            evaluate = new Evaluate(width, height);
        }

        public GolePos GeneBestPos(CellStatus[][] curStatusArr, CellStatus curStatus, int depth = 0)
        {
            if (depth == 0)
            {
                depth = defaultDepth;
            }
            return RecursionGeneBestPos(curStatusArr, curStatus, depth);
        }

        private GolePos RecursionGeneBestPos(CellStatus[][] curStatusArr, CellStatus curStatus, int depth)
        {
            var tempGoleList = new List<GolePos>();
            for (int x = 0; x < curStatusArr.Length; x++)
            {
                for (int y = 0; y < curStatusArr[x].Length; y++)
                {
                    if (!ShouldCompute(curStatusArr, x, y))
                    {
                        continue;
                    }
                    curStatusArr[x][y] = curStatus;
                    if (depth > 0)
                    {
                        tempGoleList.Add();RecursionGeneBestPos(curStatusArr, CellStatusHelper.Not(curStatus), depth - 1);
                    }
                    else
                    {
                        tempGoleList[evaluate.ComputeGole(curStatusArr)] = new Pos(x, y);
                    }
                    curStatusArr[x][y] = CellStatus.Empty;
                }
            }
            var key = curStatus == CellStatus.Black ? tempGoleList.Keys.Max() : tempGoleList.Keys.Min();
            posStack.Push(new GolePos(key, tempGoleList[key]));
        }

        private bool ShouldCompute(CellStatus[][] curStatusArr, int x, int y)
        {
            return curStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
