using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace wzq_ai
{
    public class Border
    {
        private readonly CellStatus[][] cellStatusArr;
        private readonly Stack<Pos> stepStack = new Stack<Pos>();

        private readonly Evaluate evaluate;
        private readonly int[][] blackPosGoles;
        private readonly int[][] whitePosGoles;

        public Border()
        {
            cellStatusArr = new CellStatus[GlobalConst.BORDER_SIZE][];
            blackPosGoles = new int[GlobalConst.BORDER_SIZE][];
            whitePosGoles = new int[GlobalConst.BORDER_SIZE][];

            for (var i = 0; i < GlobalConst.BORDER_SIZE; i++)
            {
                cellStatusArr[i] = new CellStatus[GlobalConst.BORDER_SIZE];
                blackPosGoles[i] = new int[GlobalConst.BORDER_SIZE];
                whitePosGoles[i] = new int[GlobalConst.BORDER_SIZE];

                for (var j = 0; j < cellStatusArr[i].Length; j++)
                {
                    blackPosGoles[i][j] = 0;
                    whitePosGoles[i][j] = 0;
                    cellStatusArr[i][j] = CellStatus.Empty;
                }
            }
            evaluate = new Evaluate(this);
        }

        #region 读取位置棋子状态
        public CellStatus GetCellStatus(Pos pos)
        {
            return cellStatusArr[pos.X][pos.Y];
        }

        public CellStatus GetCellStatus(int x, int y)
        {
            return cellStatusArr[x][y];
        }
        #endregion

        public int GetBlackPosGole(Pos pos)
        {
            return blackPosGoles[pos.X][pos.Y];
        }

        public int GetBlackPosGole(int x, int y)
        {
            return blackPosGoles[x][y];
        }

        public int GetWhitePosGole(Pos pos)
        {
            return whitePosGoles[pos.X][pos.Y];
        }

        public int GetWhitePosGole(int x, int y)
        {
            return whitePosGoles[x][y];
        }

        /// <summary>
        /// 下棋
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="cellStatus"></param>
        /// <returns></returns>
        public bool PutChess(Pos pos, CellStatus cellStatus)
        {
            if (cellStatusArr[pos.X][pos.Y] != CellStatus.Empty)
            {
                return false;
            }
            cellStatusArr[pos.X][pos.Y] = cellStatus;

            stepStack.Push(pos);

            UpdateAffectPosScore(pos);

            return true;
        }

        /// <summary>
        /// 悔棋
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool UnPutChess()
        {
            var pos = stepStack.Pop();
            if (cellStatusArr[pos.X][pos.Y] == CellStatus.Empty)
            {
                return false;
            }
            cellStatusArr[pos.X][pos.Y] = CellStatus.Empty;

            UpdateAffectPosScore(pos);

            return true;
        }

        /// <summary>
        /// 有棋子的最小X
        /// </summary>
        /// <returns></returns>
        public int MinX()
        {
            return stepStack.Min(pos => pos.X);
        }

        /// <summary>
        /// 有棋子的最大X
        /// </summary>
        /// <returns></returns>
        public int MaxX()
        {
            return stepStack.Max(pos => pos.X);
        }

        /// <summary>
        /// 有棋子的最小Y
        /// </summary>
        /// <returns></returns>
        public int MinY()
        {
            return stepStack.Min(pos => pos.Y);
        }

        /// <summary>
        /// 有棋子的最大Y
        /// </summary>
        /// <returns></returns>
        public int MaxY()
        {
            return stepStack.Max(pos => pos.Y);
        }

        private void UpdateAffectPosScore(Pos pos)
        {
            for (var i = pos.X - 4; i < pos.X + 4; i++)
            {
                for (var j = pos.Y - 4; j < pos.Y + 4; j++)
                {
                    //和pos不能连成五子的不需要updateScore
                    if (i != pos.X 
                        && j != pos.Y 
                        && Math.Abs(i-pos.X) != Math.Abs(j - pos.Y))
                    {
                        continue;
                    }
                    int blackGole, whiteGole;
                    evaluate.GenePosGole(new Pos(i, j), out blackGole, out whiteGole);
                    blackPosGoles[i][j] = blackGole;
                    whitePosGoles[i][j] = whiteGole;
                }
            }
        }
    }

    public class SortByX : IComparer<Pos>
    {
        public int Compare(Pos x, Pos y)
        {
            return x.X != y.X ? x.X - y.X : x.Y - y.Y;
        }
    }

    public class SortByY : IComparer<Pos>
    {
        public int Compare(Pos x, Pos y)
        {
            return x.Y != y.Y ? x.Y - y.Y : x.X - y.X;
        }
    }
}
