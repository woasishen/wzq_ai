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

        public Action<TimeSpan> ComputeFinish;

        public Evaluate Evaluate => evaluate;

        public MaxMin(CellStatus[][] curStatusArr, int width, int height)
        {
            cellStatusArr = curStatusArr;
            evaluate = new Evaluate(cellStatusArr, width, height);
        }

        /// <summary>
        /// 推测最佳位置
        /// </summary>
        /// <param name="curStatus">当前走棋方</param>
        /// <param name="depth">计算深度</param>
        /// <returns></returns>
        public GolePos GeneBestPos(CellStatus curStatus, int depth)
        {
            var start = DateTime.Now;
            var oldSelfGole = evaluate.ComputeGole(curStatus);
            var oldOtherGole = evaluate.ComputeGole(CellStatusHelper.Not(curStatus));
            var tempGoleList = new Dictionary<int, Stack<Pos>>();
            for (int x = 0; x < cellStatusArr.Length; x++)
            {
                for (int y = 0; y < cellStatusArr[x].Length; y++)
                {
                    if (!ShouldCompute(x, y))
                    {
                        continue;
                    }
                    cellStatusArr[x][y] = curStatus;
                    var tempStack = new Stack<Pos>();
                    tempStack.Push(new Pos(x, y));

                    tempGoleList[evaluate.ComputeTotalGole(
                        oldSelfGole, oldOtherGole, curStatus, tempStack.Peek())] = tempStack;
                    cellStatusArr[x][y] = CellStatus.Empty;
                }
            }
            var gole = tempGoleList.Keys.Max();
            ComputeFinish.Invoke(DateTime.Now - start);
            return new GolePos(gole, tempGoleList[gole]);
        }

        //private GolePos RecursionGeneBestPos(CellStatus curStatus, int depth)
        //{
            
        //}

        private bool ShouldCompute(int x, int y)
        {
            return cellStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
