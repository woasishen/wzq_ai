using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        public static int evaluateTimes = 0;
        private readonly int defaultDepth;
        private readonly Evaluate evaluate;
        public MaxMin(int defaultDepth, int width, int height)
        {
            this.defaultDepth = defaultDepth;
            evaluate = new Evaluate(width, height);
        }

        public GolePos GeneBestPos(CellStatus[][] curStatusArr, CellStatus curStatus, int depth = 0)
        {
            evaluateTimes = 0;
            if (depth == 0)
            {
                depth = defaultDepth;
            }
            return RecursionGeneBestPos(curStatusArr, curStatus, depth);
        }

        private GolePos RecursionGeneBestPos(
            CellStatus[][] curStatusArr, 
            CellStatus curStatus,
            int depth)
        {
            var tempGoleList = new Dictionary<int, Stack<Pos>>();
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
                        var tempGoldPos = RecursionGeneBestPos(
                            curStatusArr,
                            CellStatusHelper.Not(curStatus),
                            depth - 1);
                        tempGoldPos.PosStack.Push(new Pos(x, y));
                        tempGoleList[tempGoldPos.Gole] = tempGoldPos.PosStack;
                    }
                    else
                    {
                        var tempStack = new Stack<Pos>();
                        tempStack.Push(new Pos(x, y));
                        tempGoleList[evaluate.ComputeGole(curStatusArr)] = tempStack;
                    }
                    curStatusArr[x][y] = CellStatus.Empty;
                }
            }
            var gole = curStatus == CellStatus.Black 
                ? tempGoleList.Keys.Max()
                : tempGoleList.Keys.Min();

            return new GolePos(gole, tempGoleList[gole]);
        }

        private bool ShouldCompute(CellStatus[][] curStatusArr, int x, int y)
        {
            return curStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
