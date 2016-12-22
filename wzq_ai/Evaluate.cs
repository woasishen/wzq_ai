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

    public class Pos
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
        private readonly List<Pos[]> posLineArr = new List<Pos[]>(); //所有可能的5连格

        public Evaluate(int width, int height)
        {
            if (width < 6 || height < 6)
            {
                throw new Exception("棋盘太小");
            }

            //横的5连
            AddDirectPos(height, width, 5, posLineArr, (y, x) => new Pos(x, y));

            //竖的5连
            AddDirectPos(width, height, 5, posLineArr, (x, y) => new Pos(x, y));

            //正对角的5连
            AddDiagonalPos(width, height, 5, posLineArr, (x, y) => new Pos(x, y));

            //反对角的5连
            AddDiagonalPos(width, height, 5, posLineArr, (x, y) => new Pos(width - x - 1, y));
        }

        public int ComputeGole(CellStatus[][] cellArr, CellStatus cellStatus)
        {
            var gole = 0;
            foreach (var result in posLineArr.Select(posLine => posLine.Sum(pos => (int)cellArr[pos.X][pos.Y])))
            {
                switch (cellStatus)
                {
                    case CellStatus.Black:
                        //含有白子
                        if (result > 10)
                        {
                            continue;
                        }
                        gole += 2 << result;
                        break;
                    case CellStatus.White:
                        //含有黑子
                        if (result % 10 > 0)
                        {
                            continue;
                        }
                        gole += 2 << (result / 10);
                        break;
                    case CellStatus.Empty:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cellStatus), cellStatus, null);
                }
            }
            return gole;
        }

        public bool CheckIsWin(CellStatus[][] cellArr)
        {
            return posLineArr.Select(
                pos5Item => pos5Item.Sum(pos => (int) cellArr[pos.X][pos.Y]))
                .Any(tempItemKey => tempItemKey == 5 || tempItemKey == 50);
        }

        private void AddDirectPos(int width, int height, int length, List<Pos[]> posArr, Func<int, int, Pos> genPos)
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
                    posArr.Add(tempPos);
                }
            }
        }

        private void AddDiagonalPos(int width, int height, int length, List<Pos[]> posArr, Func<int, int, Pos> genPos)
        {
            AddDiagonalPosOneLine(0, 0, width, height, length, posArr, genPos);
            for (var i = 1; i < width; i++)
            {
                AddDiagonalPosOneLine(i, 0, width, height, length, posArr, genPos);
            }
            for (var i = 1; i < height; i++)
            {
                AddDiagonalPosOneLine(0, i, width, height, length, posArr, genPos);
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
