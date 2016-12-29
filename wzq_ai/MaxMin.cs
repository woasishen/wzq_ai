using System;
using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        private readonly Evaluate evaluate;
        private readonly CellStatus[][] cellStatusArr;

        public Evaluate Evaluate
        {
            get { return evaluate; }
        }
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
                    if (x == 9 && y == 2)
                    {
                        int a = 1;
                    }
                    if (x == 8 && y == 2)
                    {
                        int a = 2;
                    }
                    if (x == 3 && y == 7)
                    {
                        int a = 3;
                    }

                    var tempStack = new Stack<Pos>();
                    tempStack.Push(new Pos(x, y));
                    tempGoleList[evaluate.ComputeTotalGole(curStatus)] = tempStack;

                    cellStatusArr[x][y] = CellStatus.Empty;
                }
            }
            var gole = curStatus == CellStatus.Black
                ? tempGoleList.Keys.Max()
                : tempGoleList.Keys.Min();

            return new GolePos(gole, tempGoleList[gole]);
        }

        private bool ShouldCompute(int x, int y)
        {
            return cellStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
