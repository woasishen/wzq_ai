using System;
using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public enum CellStatus
    {
        Empty = 0,
        Black = 1,
        White = 10,
    }

    public static class CellStatusHelper
    {
        public static CellStatus Not(CellStatus s)
        {
            switch (s)
            {
                case CellStatus.Black:
                    return CellStatus.White;
                case CellStatus.White:
                    return CellStatus.Black;
                case CellStatus.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(s), s, null);
            }
            throw new Exception("cellstatus do not valid to not");
        }
    }

    public struct Pos
    {
        public int X { get; }
        public int Y { get; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

    public class GolePos
    {
        public int Gole { get; private set; }
        public Stack<Pos> PosStack { get; private set; }

        public GolePos(int gole, Stack<Pos> posStack)
        {
            Gole = gole;
            PosStack = posStack;
        }
    }

    public class Evaluate
    {
        public static readonly Dictionary<int, int> GOLE_DICT = new Dictionary<int, int>
        {
            {0,0 },
            {1,1 },
            {2,100 },
            {3,10000 },
            {4,1000000 },
            {5,100000000 }
        };

        private readonly CellStatus[][] cellStatusArr;

        private readonly List<Pos[]> posLineArr = new List<Pos[]>(); //所有可能的5连格
        private readonly Dictionary<Pos, List<Pos[]>> posContainersDict = new Dictionary<Pos, List<Pos[]>>();//包含特定位置的所有5连格

        public Evaluate(CellStatus[][] curStatusArr, int width, int height)
        {
            if (width < 6 || height < 6)
            {
                throw new ArgumentException("棋盘太小");
            }

            cellStatusArr = curStatusArr;

            //横的5连
            AddDirectPos(height, width, 5, (y, x) => new Pos(x, y));

            //竖的5连
            AddDirectPos(width, height, 5, (x, y) => new Pos(x, y));

            //正对角的5连
            AddDiagonalPos(width, height, 5, (x, y) => new Pos(x, y));

            //反对角的5连
            AddDiagonalPos(width, height, 5, (x, y) => new Pos(width - x - 1, y));

            foreach (var posLine in posLineArr)
            {
                foreach (var pos in posLine)
                {
                    if (!posContainersDict.ContainsKey(pos))
                    {
                        posContainersDict[pos] = new List<Pos[]>();
                    }
                    posContainersDict[pos].Add(posLine);
                }
            }
        }

        public int ComputeTotalGole(CellStatus cellStatus)
        {
            var selfGole = ComputeGole(cellStatus);
            if (selfGole >= GOLE_DICT[4])
            {
                return GOLE_DICT[5];
            }
            var otherGole = ComputeGole(CellStatusHelper.Not(cellStatus));
            if (otherGole >= GOLE_DICT[4] * 2)
            {
                return -GOLE_DICT[5];
            }
            //因为将要轮到对方走，对方分数做增益
            otherGole *= 2;

            if (cellStatus == CellStatus.Black)
            {
                return selfGole - otherGole;
            }
            return otherGole - selfGole;
        }

        private int ComputeGole(CellStatus cellStatus)
        {
            var gole = 0;
            foreach (var posLine in posLineArr)
            {
                var tempGole = GeneGole(cellStatus, posLine);
                if (tempGole == GOLE_DICT[5])
                {
                    return GOLE_DICT[5];
                }
                if (tempGole == GOLE_DICT[4])
                {
                    gole += tempGole;
                }
                if (tempGole == GOLE_DICT[3])
                {
                    gole += tempGole;
                }
                if (tempGole == GOLE_DICT[2])
                {
                    gole += tempGole;
                }
                if (tempGole == GOLE_DICT[1])
                {
                    gole += tempGole;
                }
            }
            return gole;
        }

        public int ComputePosGoleForSelf(CellStatus cellStatus, Pos pos)
        {
            if (cellStatus == CellStatus.Empty || cellStatusArr[pos.X][pos.Y] != CellStatus.Empty)
            {
                throw new ArgumentException("ComputePosGoleForSelf");
            }
            var gole = 0;
            cellStatusArr[pos.X][pos.Y] = cellStatus;
            foreach (var posLine in posContainersDict[pos])
            {
                var tempGole = GeneGole(cellStatus, posLine);
                if (tempGole == GOLE_DICT[5])
                {
                    return GOLE_DICT[5];
                }
                gole += tempGole;
            }
            cellStatusArr[pos.X][pos.Y] = CellStatus.Empty;
            return gole;
        }

        public int ComputePosGoleForOther(CellStatus cellStatus, Pos pos)
        {
            if (cellStatus == CellStatus.Empty || cellStatusArr[pos.X][pos.Y] != CellStatus.Empty)
            {
                throw new Exception("ComputePosGoleForSelf");
            }
            var gole = 0;
            foreach (var posLine in posContainersDict[pos])
            {
                var tempGole = GeneGole(CellStatusHelper.Not(cellStatus), posLine);
                if (tempGole == GOLE_DICT[5])
                {
                    throw new Exception("内部错误");
                }
                gole += tempGole;
            }
            return gole;
        }

        private int GeneGole(CellStatus cellStatus, Pos[] posLine)
        {
            var result = posLine.Sum(p => (int) cellStatusArr[p.X][p.Y]);
            switch (cellStatus)
            {
                case CellStatus.Black:
                    //含有白子
                    return result >= 10 ? 0 : GOLE_DICT[result];
                case CellStatus.White:
                    //含有黑子
                    return result % 10 >= 1 ? 0 : GOLE_DICT[result / 10];
            }
            throw new ArgumentOutOfRangeException(nameof(cellStatus), cellStatus, null);
        }

        private void AddDirectPos(int width, int height, int length, Func<int, int, Pos> genPos)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height - length + 1; j++)
                {
                    var tempPos = new Pos[length];
                    for (var k = 0; k < length; k++)
                    {
                        tempPos[k] = genPos(i, j + k);
                    }
                    posLineArr.Add(tempPos);
                }
            }
        }

        private void AddDiagonalPos(int width, int height, int length, Func<int, int, Pos> genPos)
        {
            AddDiagonalPosOneLine(0, 0, width, height, length, posLineArr, genPos);
            for (var i = 1; i < width; i++)
            {
                AddDiagonalPosOneLine(i, 0, width, height, length, posLineArr, genPos);
            }
            for (var i = 1; i < height; i++)
            {
                AddDiagonalPosOneLine(0, i, width, height, length, posLineArr, genPos);
            }
        }

        private void AddDiagonalPosOneLine(int startX, int startY, int width, int height, int length, List<Pos[]> posArr, Func<int, int, Pos> genPos)
        {
            while (startX + length - 1 < width && startY + length - 1 < height)
            {
                var tempPos = new Pos[length];
                for (var i = 0; i < length; i++)
                {
                    tempPos[i] = genPos(startX + i, startY + i);
                }
                posArr.Add(tempPos);
                startX++;
                startY++;
            }
        }
    }
}
