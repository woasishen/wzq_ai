using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;

namespace wzq_ai
{
    public class Border
    {
        private const int STATUS_MUL = 4;
        private readonly CellStatus[][] _cellStatusArr;
        private readonly Stack<Pos> _stepStack = new Stack<Pos>();

        private readonly Evaluate _evaluate;
        private readonly MaxMin _maxMin;
        private readonly int[][] _blackPosGoles;
        private readonly int[][] _whitePosGoles;

        public Action<int, TimeSpan> ComputeFinished;
        public Action ChessChanged;
        public Pos LastStepPos => _stepStack.Peek();
        public int StepIndex => _stepStack.Count;
        public bool GameOver;

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

        public int GetPosBlackGole(int x, int y)
        {
            return _blackPosGoles[x][y];
        }

        public int GetPosWhiteGole(int x, int y)
        {
            return _whitePosGoles[x][y];
        }

        public int GetPosBlackGole(Pos pos)
        {
            return _blackPosGoles[pos.X][pos.Y];
        }

        public int GetPosWhiteGole(Pos pos)
        {
            return _whitePosGoles[pos.X][pos.Y];
        }

        public int GetPosGole(int x, int y, CellStatus curStatus)
        {
            switch (curStatus)
            {
                case CellStatus.Black:
                    return _blackPosGoles[x][y] * STATUS_MUL + _whitePosGoles[x][y];
                case CellStatus.White:
                    return _blackPosGoles[x][y] + _whitePosGoles[x][y] * STATUS_MUL;
                default:
                    throw new ArgumentOutOfRangeException(nameof(curStatus), curStatus, null);
            }
        }

        public int GetRoleGole(CellStatus curStatus)
        {
            var totalBlack = 0;
            var totalWhite = 0;

            for (var i = 0; i < Configs.BORDER_SIZE; i++)
            {
                for (var j = 0; j < Configs.BORDER_SIZE; j++)
                {
                    totalBlack += _blackPosGoles[i][j];
                    totalWhite += _whitePosGoles[i][j];
                }
            }
            switch (curStatus)
            {
                case CellStatus.Black:
                    return totalBlack * STATUS_MUL - totalWhite;
                case CellStatus.White:
                    return totalWhite * STATUS_MUL - totalBlack;
                default:
                    throw new ArgumentOutOfRangeException(nameof(curStatus), curStatus, null);
            }
        }
        #endregion

        public Pos FindBestPos(CellStatus curstatus)
        {
            return _maxMin.FindBestPos(curstatus);
        }

        /// <summary>
        /// 下棋
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="cellStatus"></param>
        /// <param name="needRefresh"></param>
        /// <returns></returns>
        public bool PutChess(Pos pos, CellStatus cellStatus, bool needRefresh = true)
        {
            if (_cellStatusArr[pos.X][pos.Y] != CellStatus.Empty)
            {
                throw new Exception("putchess not emput");
            }
            _cellStatusArr[pos.X][pos.Y] = cellStatus;

            _stepStack.Push(pos);

            UpdateAffectPosScore(pos);
            GameOver = _evaluate.CheckGameOver(pos);
            if (needRefresh)
            {
                ChessChanged.Invoke();
            }
            return GameOver;
        }

        /// <summary>
        /// 悔棋
        /// </summary>
        /// <returns></returns>
        public bool UnPutChess(bool needRefresh = true)
        {
            if (!_stepStack.Any())
            {
                return false;
            }
            GameOver = false;
            var pos = _stepStack.Pop();
            _cellStatusArr[pos.X][pos.Y] = CellStatus.Empty;
            UpdateAffectPosScore(pos);
            if (needRefresh)
            {
                ChessChanged.Invoke();
            }
            return true;
        }

        /// <summary>
        /// 重新开始
        /// </summary>
        public void ClearChess()
        {
            GameOver = false;
            _stepStack.Clear();
            ReInitCellStatusAndPosGole();
            ChessChanged.Invoke();
        }

        private void UpdateAffectPosScore(Pos pos)
        {
            var xMin = Math.Max(pos.X - 4, 0);
            var xMax = Math.Min(pos.X + 5, Configs.BORDER_SIZE);
            var yMin = Math.Max(pos.Y - 4, 0);
            var yMax = Math.Min(pos.Y + 5, Configs.BORDER_SIZE);

            for (var i = xMin; i < xMax; i++)
            {
                for (var j = yMin; j < yMax; j++)
                {
                    if (GetCellStatus(i, j) != CellStatus.Empty)
                    {
                        _blackPosGoles[i][j] = _whitePosGoles[i][j] = 0;
                        continue;
                    }
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
