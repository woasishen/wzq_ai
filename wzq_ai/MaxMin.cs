using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        private readonly Evaluate evaluate;
        private readonly CellStatus[][] curStatusArr;
        public MaxMin(CellStatus[][] curStatusArr, int width, int height)
        {
            this.curStatusArr = curStatusArr;
            evaluate = new Evaluate(width, height);
        }

        public int ComputeCurGold()
        {
            return evaluate.ComputeGole(curStatusArr);
        }

        public bool CheckIsWin()
        {
            return evaluate.CheckIsWin(curStatusArr);
        }

        public GolePos GeneBestPos(CellStatus curStatus, int depth)
        {
            return RecursionGeneBestPos(curStatus, depth);
        }

        private GolePos RecursionGeneBestPos(
            CellStatus curStatus,
            int depth)
        {
            var tempGoleList = new Dictionary<int, Stack<Pos>>();
            for (int x = 0; x < curStatusArr.Length; x++)
            {
                for (int y = 0; y < curStatusArr[x].Length; y++)
                {
                    if (!ShouldCompute(x, y))
                    {
                        continue;
                    }
                    curStatusArr[x][y] = curStatus;
                    if (depth > 0)
                    {
                        var tempGoldPos = RecursionGeneBestPos(
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

        private bool ShouldCompute(int x, int y)
        {
            return curStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
