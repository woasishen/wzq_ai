using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace wzq_ai
{
    public class MaxMin
    {
        private class PosGole
        {
            public PosGole(Pos pos, int gole)
            {
                Pos = pos;
                Gole = gole;
            }

            public Pos Pos { get; }
            public int Gole { get; }
        }

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
        /// <param name="cellStatus">当前走棋方</param>
        /// <param name="depth">计算深度</param>
        /// <returns></returns>
        public GolePos GeneBestPos(CellStatus cellStatus, int depth)
        {
            var posGoleStack = new Stack<PosGole>();
            for (int x = 0; x < cellStatusArr.Length; x++)
            {
                for (int y = 0; y < cellStatusArr[x].Length; y++)
                {
                    if (!ShouldCompute(x, y))
                    {
                        continue;
                    }
                    var tempPos = new Pos(x, y);
                    var posGole = evaluate.ComputePosGole(tempPos);
                    if (posGoleStack.Count != 0 && posGoleStack.Peek().Gole < posGole)
                    {
                        posGoleStack.Clear();
                    }
                    if (posGoleStack.Count == 0 || posGoleStack.Peek().Gole == posGole)
                    {
                        posGoleStack.Push(new PosGole(tempPos, posGole));
                    }
                }
            }
            var tempStack = new Stack<Pos>();
            tempStack.Push(posGoleStack.Peek().Pos);
            return new GolePos(posGoleStack.Peek().Gole, tempStack);
        }

        private bool ShouldCompute(int x, int y)
        {
            return cellStatusArr[x][y] == CellStatus.Empty;
        }
    }
}
