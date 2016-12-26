using System;
using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        private readonly Evaluate evaluate;
        private readonly CellStatus[][] cellStatusArr;
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
            var curGole = evaluate.ComputeGole(curStatus);
            if (curGole == -1)
            {
                throw new Exception("已经取得胜利");
            }
            if (curGole >= Evaluate.GOLE_DICT[4])
            {
                for (int i = 0; i < cellStatusArr.Length; i++)
                {
                    for (int j = 0; j < cellStatusArr[i].Length; j++)
                    {
                        var pos = new Pos(i, j);
                        var gole = evaluate.ComputePosGoleForSelf(curStatus, pos);
                        if (gole > Evaluate.GOLE_DICT[5])
                        {
                            var tempStack = new Stack<Pos>();
                            tempStack.Push(pos);
                            return new GolePos(-1, tempStack);
                        }
                    }
                }
            }
            var otherGole = evaluate.ComputeGole(CellStatusHelper.Not(curStatus));

            //var tempGoleList = new Dictionary<int, Stack<Pos>>();
            //for (var x = 0; x < cellStatusArr.Length; x++)
            //{
            //    for (var y = 0; y < cellStatusArr[x].Length; y++)
            //    {
            //        if (!ShouldCompute(x, y))
            //        {
            //            continue;
            //        }
            //        cellStatusArr[x][y] = curStatus;
            //        if (depth > 0)
            //        {
            //            var tempGoldPos = RecursionGeneBestPos(
            //                CellStatusHelper.Not(curStatus),
            //                depth - 1);
            //            tempGoldPos.PosStack.Push(new Pos(x, y));
            //            tempGoleList[tempGoldPos.Gole] = tempGoldPos.PosStack;
            //        }
            //        else
            //        {
            //            var tempStack = new Stack<Pos>();
            //            tempStack.Push(new Pos(x, y));
            //            tempGoleList[evaluate.ComputeGole(cellStatusArr)] = tempStack;
            //        }
            //        cellStatusArr[x][y] = CellStatus.Empty;
            //    }
            //}
            //var gole = curStatus == CellStatus.Black
            //    ? tempGoleList.Keys.Max()
            //    : tempGoleList.Keys.Min();

            //return new GolePos(gole, tempGoleList[gole]);
        }

        private bool ShouldCompute(int x, int y)
        {
            return cellStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
