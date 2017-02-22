using System;
using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class Border
    {
        private readonly CellStatus[][] _cellStatusArr;
        private readonly Stack<Pos> _stepStack = new Stack<Pos>();

        private readonly Evaluate _evaluate;
        private readonly MaxMin _maxMin;
        private readonly int[][] _blackPosGoles;
        private readonly int[][] _whitePosGoles;

        public Action<int, TimeSpan> ComputeFinished;
        public Pos LastStepPos => _stepStack.Peek();
        public int StepIndex => _stepStack.Count;

        public Border()
        {
            _cellStatusArr = new CellStatus[Configs.BORDER_SIZE][];
            _blackPosGoles = new int[Configs.BORDER_SIZE][];
            _whitePosGoles = new int[Configs.BORDER_SIZE][];

            for (var i = 0; i < Configs.BORDER_SIZE; i++)
            {
                _cellStatusArr[i] = new CellStatus[Configs.BORDER_SIZE];
                _blackPosGoles[i] = new int[Configs.BORDER_SIZE];
                _whitePosGoles[i] = new int[Configs.BORDER_SIZE];
            }
            _evaluate = new Evaluate(this);
            _maxMin = new MaxMin(this)
            {
                ComputeFinished = (i, span) => { ComputeFinished(i, span); }
            };
            ReInitCellStatusAndPosGole();
        }

        private void ReInitCellStatusAndPosGole()
        {
            for (var i = 0; i < Configs.BORDER_SIZE; i++)
            {
                for (var j = 0; j < Configs.BORDER_SIZE; j++)
                {
                    _cellStatusArr[i][j] = CellStatus.Empty;
                }
            }
            for (var i = 0; i < Configs.BORDER_SIZE; i++)
            {
                for (var j = 0; j < Configs.BORDER_SIZE; j++)
                {
                    int blackGole, whiteGole;
                    _evaluate.GenePosGole(new Pos(i, j), out blackGole, out whiteGole);
                    _blackPosGoles[i][j] = blackGole;
                    _whitePosGoles[i][j] = whiteGole;
                }
            }
        }

        #region 读取位置棋子状态
        public CellStatus GetCellStatus(Pos pos)
        {
            return _cellStatusArr[pos.X][pos.Y];
        }

        public CellStatus GetCellStatus(int x, int y)
        {
            return _cellStatusArr[x][y];
        }
        #endregion

        #region 计算得分
        public int GetBlackPosGole(Pos pos)
        {
            return _blackPosGoles[pos.X][pos.Y];
        }

        public int GetBlackPosGole(int x, int y)
        {
            return _blackPosGoles[x][y];
        }

        public int GetWhitePosGole(Pos pos)
        {
            return _whitePosGoles[pos.X][pos.Y];
        }

        public int GetWhitePosGole(int x, int y)
        {
            return _whitePosGoles[x][y];
        }

        public int GetRoleGole(CellStatus curStatus)
        {
            var blackMax = 0;
            var whiteMax = 0;
            for (var i = 0; i < Configs.BORDER_SIZE; i++)
            {
                for (var j = 0; j < Configs.BORDER_SIZE; j++)
                {
                    blackMax = Math.Max(blackMax, _blackPosGoles[i][j]);
                    whiteMax = Math.Max(whiteMax, _whitePosGoles[i][j]);
                }
            }

            switch (curStatus)
            {
                case CellStatus.Black:
                    return blackMax * 25 - whiteMax;
                case CellStatus.White:
                    return whiteMax * 25 - blackMax;
            }
            return 0;
        }
        #endregion

        public void AutoPutChess(CellStatus curstatus)
        {
            PutChess(_maxMin.FindBestPos(curstatus), curstatus);
        }

        /// <summary>
        /// 下棋
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="cellStatus"></param>
        /// <returns></returns>
        public bool PutChess(Pos pos, CellStatus cellStatus)
        {
            if (_cellStatusArr[pos.X][pos.Y] != CellStatus.Empty)
            {
                return false;
            }
            _cellStatusArr[pos.X][pos.Y] = cellStatus;

            _stepStack.Push(pos);

            UpdateAffectPosScore(pos);
            return true;
        }

        /// <summary>
        /// 悔棋
        /// </summary>
        /// <returns></returns>
        public bool UnPutChess()
        {
            if (!_stepStack.Any())
            {
                return false;
            }
            var pos = _stepStack.Pop();
            _cellStatusArr[pos.X][pos.Y] = CellStatus.Empty;
            UpdateAffectPosScore(pos);
            return true;
        }

        public void ClearChess()
        {
            _stepStack.Clear();
            ReInitCellStatusAndPosGole();
        }

        public bool CheckGameOver(Pos pos)
        {
            return _evaluate.CheckGameOver(pos);
        }

        /// <summary>
        /// 有棋子的最小X
        /// </summary>
        /// <returns></returns>
        public int MinX()
        {
            return _stepStack.Min(pos => pos.X);
        }

        /// <summary>
        /// 有棋子的最大X
        /// </summary>
        /// <returns></returns>
        public int MaxX()
        {
            return _stepStack.Max(pos => pos.X);
        }

        /// <summary>
        /// 有棋子的最小Y
        /// </summary>
        /// <returns></returns>
        public int MinY()
        {
            return _stepStack.Min(pos => pos.Y);
        }

        /// <summary>
        /// 有棋子的最大Y
        /// </summary>
        /// <returns></returns>
        public int MaxY()
        {
            return _stepStack.Max(pos => pos.Y);
        }

        private void UpdateAffectPosScore(Pos pos)
        {
            var xMin = Math.Max(pos.X - 4, 0);
            var xMax = Math.Min(pos.X + 4, Configs.BORDER_SIZE);
            var yMin = Math.Max(pos.Y - 4, 0);
            var yMax = Math.Min(pos.Y + 4, Configs.BORDER_SIZE);

            for (var i = xMin; i < xMax; i++)
            {
                for (var j = yMin; j < yMax; j++)
                {
                    //和pos不能连成五子的不需要updateScore
                    if (i != pos.X
                        && j != pos.Y
                        && Math.Abs(i - pos.X) != Math.Abs(j - pos.Y))
                    {
                        continue;
                    }
                    int blackGole, whiteGole;
                    _evaluate.GenePosGole(new Pos(i, j), out blackGole, out whiteGole);
                    _blackPosGoles[i][j] = blackGole;
                    _whitePosGoles[i][j] = whiteGole;
                }
            }
        }
    }
}
