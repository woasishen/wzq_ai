using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        private readonly Evaluate evaluate;
        private readonly CellStatus[][] cellStatusArr;
        private int recusionTimes;

        public Action<TimeSpan, int> ComputeFinish;

        public Evaluate Evaluate => evaluate;

        public MaxMin(CellStatus[][] curStatusArr, int width, int height)
        {
            cellStatusArr = curStatusArr;
            evaluate = new Evaluate(cellStatusArr, width, height);
        }

        public GolePos GeneBestPos(CellStatus cellStatus)
        {
            var startTime = DateTime.Now;
            recusionTimes = 0;
            var result = GeneBestPos(cellStatus, 2);
            ComputeFinish.Invoke(startTime - DateTime.Now, recusionTimes);
            return result;
        }

        /// <summary>
        /// 推测最佳位置
        /// </summary>
        /// <param name="cellStatus">当前走棋方</param>
        /// <param name="depth">计算深度</param>
        /// <returns></returns>
        private GolePos GeneBestPos(CellStatus cellStatus, int depth)
        {
            recusionTimes++;
            var oldSelfGole = evaluate.ComputeGole(cellStatus);
            var oldOtherGole = evaluate.ComputeGole(CellStatusHelper.Not(cellStatus));
            var tempGoleList = new Dictionary<int, Stack<Pos>>();
            for (int x = 0; x < cellStatusArr.Length; x++)
            {
                for (int y = 0; y < cellStatusArr[x].Length; y++)
                {
                    if (!ShouldCompute(x, y))
                    {
                        continue;
                    }
                    cellStatusArr[x][y] = cellStatus;
                    var tempPos = new Pos(x, y);
                    var selfAdd = evaluate.ComputePosGoleForSelf(cellStatus, tempPos);
                    if (selfAdd == Evaluate.GOLE_DICT[5])
                    {
                        var tempStack = new Stack<Pos>();
                        tempStack.Push(tempPos);
                        cellStatusArr[x][y] = CellStatus.Empty;
                        return new GolePos(Evaluate.GOLE_DICT[5], tempStack);
                    }
                    var otherMinus = evaluate.ComputePosGoleForOther(cellStatus, tempPos);
                    var totalGole = evaluate.ComputeTotalGole(
                        oldSelfGole, oldOtherGole, selfAdd, otherMinus);
                    if (otherMinus >= Evaluate.GOLE_DICT[4])
                    {
                        var tempStack = new Stack<Pos>();
                        tempStack.Push(tempPos);
                        cellStatusArr[x][y] = CellStatus.Empty;
                        return new GolePos(totalGole, tempStack);
                    }

                    if (depth == 0 || (otherMinus == 0 && selfAdd < 20))
                    {
                        var tempStack = new Stack<Pos>();
                        tempStack.Push(tempPos);
                        tempGoleList[totalGole] = tempStack;
                    }
                    else
                    {
                        var tempGolePos = GeneBestPos(CellStatusHelper.Not(cellStatus), depth - 1);
                        tempGolePos.PosStack.Push(tempPos);
                        tempGoleList[tempGolePos.Gole] = tempGolePos.PosStack;
                    }
                    cellStatusArr[x][y] = CellStatus.Empty;
                }
            }
            var gole = tempGoleList.Keys.Max();
            return new GolePos(gole, tempGoleList[gole]);
        }

        private bool ShouldCompute(int x, int y)
        {
            return cellStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
