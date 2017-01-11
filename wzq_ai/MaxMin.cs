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
        private readonly int width;
        private readonly int height;
        private int recusionTimes;

        public Action<TimeSpan, int> ComputeFinish;

        public Evaluate Evaluate => evaluate;

        public MaxMin(CellStatus[][] curStatusArr, int width, int height)
        {
            cellStatusArr = curStatusArr;
            evaluate = new Evaluate(cellStatusArr, width, height);
            this.width = width;
            this.height = height;
        }

        public GolePos GeneBestPos(CellStatus cellStatus)
        {
            var startTime = DateTime.Now;
            recusionTimes = 0;

            int xMax, xMin, yMax, yMin;
            xMin = yMin = int.MaxValue;
            xMax = yMax = int.MinValue;
            bool emptyCellArr = true;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cellStatusArr[i][j] != CellStatus.Empty)
                    {
                        emptyCellArr = false;
                        xMax = Math.Min(Math.Max(xMax, i + 1), width);
                        xMin = Math.Max(Math.Min(xMin, i - 1), 0);
                        yMax = Math.Min(Math.Max(yMax, j + 1), height);
                        yMin = Math.Max(Math.Min(yMin, j - 1), 0);
                    }
                }
            }
            if (emptyCellArr)
            {
                var tempStack = new Stack<Pos>();
                tempStack.Push(new Pos(width/2, height/2));
                return new GolePos(0, tempStack);
            }

            var result = GeneBestPos(cellStatus, 2, xMax, xMin, yMax, yMin);
            ComputeFinish.Invoke(startTime - DateTime.Now, recusionTimes);
            return result;
        }

        /// <summary>
        /// 推测最佳位置
        /// </summary>
        /// <param name="cellStatus">当前走棋方</param>
        /// <param name="depth">计算深度</param>
        /// <param name="xMax"></param>
        /// <param name="xMin"></param>
        /// <param name="yMax"></param>
        /// <param name="yMin"></param>
        /// <returns></returns>
        private GolePos GeneBestPos(CellStatus cellStatus, int depth, 
            int xMax, int xMin, int yMax, int yMin)
        {
            recusionTimes++;
            var oldSelfGole = evaluate.ComputeGole(cellStatus);
            var oldOtherGole = evaluate.ComputeGole(CellStatusHelper.Not(cellStatus));
            var tempGoleList = new Dictionary<int, Stack<Pos>>();
            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
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
                        var tempGolePos = GeneBestPos(
                            CellStatusHelper.Not(cellStatus),
                            depth - 1,
                            Math.Min(Math.Max(xMax, x + 1), width),
                            Math.Max(Math.Min(xMin, x - 1), 0),
                            Math.Min(Math.Max(yMax, y + 1), height),
                            Math.Max(Math.Min(yMin, y - 1), 0));
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
