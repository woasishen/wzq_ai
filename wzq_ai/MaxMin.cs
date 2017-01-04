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
            var result = GeneBestPos(cellStatus, 1);
            ComputeFinish.Invoke(DateTime.Now - startTime, recusionTimes);
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
            var posGoleStack = new Stack<GolePos>();
            for (int x = 0; x < cellStatusArr.Length; x++)
            {
                for (int y = 0; y < cellStatusArr[x].Length; y++)
                {
                    if (!ShouldCompute(x, y))
                    {
                        continue;
                    }
                    var tempPos = new Pos(x, y);
                    GolePos tempGolePos;
                    if (depth == 0)
                    {
                        tempGolePos = new GolePos(evaluate.ComputePosGole(tempPos), new Stack<Pos>());
                    }
                    else
                    {
                        cellStatusArr[tempPos.X][tempPos.Y] = cellStatus;
                        tempGolePos = GeneBestPos(CellStatusHelper.Not(cellStatus), depth - 1);
                        cellStatusArr[tempPos.X][tempPos.Y] = CellStatus.Empty;
                    }
                    
                    if (posGoleStack.Count != 0 && posGoleStack.Peek().Gole < tempGolePos.Gole)
                    {
                        posGoleStack.Clear();
                    }
                    if (posGoleStack.Count == 0 || posGoleStack.Peek().Gole == tempGolePos.Gole)
                    {
                        tempGolePos.PosStack.Push(tempPos);
                        posGoleStack.Push(tempGolePos);
                    }
                }
            }

            return posGoleStack.Peek();
        }

        private bool ShouldCompute(int x, int y)
        {
            return cellStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
