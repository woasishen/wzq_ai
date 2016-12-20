using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace wzq_ai
{
    public enum CellStatus
    {
        Empty = 1,
        Black = 2,
        White = 3,
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
            }
            throw new Exception("cellstatus do not valid to not");
        }
    }

    public class Pos
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", X, Y);
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

    /// <summary>
    /// 评分规则：
    /// 1) 00000->50000  //5格
    /// 
    /// 2) +0000+->4320  //6格
    /// 3) +000++->720
    /// 4) ++000+->720
    /// 5) +00+0+->720
    /// 6) +0+00+->720
    /// 
    /// 7) 0000+->720    //5格
    /// 8) +0000->720
    /// 9) 00+00->720
    /// 10)0+000->720
    /// 11)000+0->720
    /// 
    /// 12)++00++->720    //6格
    /// 13)++0+0+->120
    /// 14)+0+0++->120
    /// 15)+++0++->20
    /// 16)++0+++->20
    /// </summary>
    public class Evaluate
    {
        private static readonly Dictionary<int, int> Goles =
            new Dictionary<int, int>
            {
                {22222, 50000},
                {122221, 4320},
                {122211, 720},
                {112221, 720},
                {122121, 720},
                {121221, 720},
                {22221, 720},
                {12222, 720},
                {22122, 720},
                {21222, 720},
                {22212, 720},
                {112211, 720},
                {112121, 120},
                {121211, 120},
                {111211, 20},
                {112111, 20},
                {33333, -50000},
                {133331, -4320},
                {133311, -720},
                {113331, -720},
                {133131, -720},
                {131331, -720},
                {33331, -720},
                {13333, -720},
                {33133, -720},
                {31333, -720},
                {33313, -720},
                {113311, -720},
                {113131, -120},
                {131311, -120},
                {111311, -20},
                {113111, -20},
            };

        private readonly List<Pos[]> pos5 = new List<Pos[]>();//所有可能的5连格
        private readonly List<Pos[]> pos6 = new List<Pos[]>();//所有可能的6连格
        public Evaluate(int width, int height)
        {
            if (width < 6 || height < 6)
            {
                throw new Exception("棋盘太小");
            }

            //横的5连
            AddDirectPos(height, width, 5, pos5, (y, x) => new Pos(x, y));
            //横的6连
            AddDirectPos(height, width, 6, pos6, (y, x) => new Pos(x, y));

            //竖的5连
            AddDirectPos(width, height, 5, pos5, (x, y) => new Pos(x, y));
            //竖的6连
            AddDirectPos(width, height, 6, pos6, (x, y) => new Pos(x, y));

            //正对角的5连
            AddDiagonalPos(width, height, 5, pos5, (x, y) => new Pos(x, y));
            //正对角的6连
            AddDiagonalPos(width, height, 6, pos6, (x, y) => new Pos(x, y));

            //反对角的5连
            AddDiagonalPos(width, height, 5, pos5, (x, y) => new Pos(width - x - 1, y));
            //反对角的6连
            AddDiagonalPos(width, height, 6, pos6, (x, y) => new Pos(width - x - 1, y));
        }

        public int ComputeGole(CellStatus[][] cellArr)
        {
            MaxMin.evaluateTimes++;
            Log.Info("evaluateTimes:" + MaxMin.evaluateTimes);
            var result = 0;
            foreach (var pos5Item in pos5)
            {
                var tempItemKey = pos5Item.Aggregate(0, (current, t) => current * 10 + (int)cellArr[t.X][t.Y]);
                if (Goles.ContainsKey(tempItemKey))
                {
                    result += Goles[tempItemKey];
                }
            }
            foreach (var pos6Item in pos6)
            {
                var tempItemKey = pos6Item.Aggregate(0, (current, t) => current * 10 + (int)cellArr[t.X][t.Y]);
                if (Goles.ContainsKey(tempItemKey))
                {
                    result += Goles[tempItemKey];
                }
            }
            return result;
        }

        private void AddDirectPos(
            int width, int height, int length,
            List<Pos[]> posArr,
            Func<int, int, Pos> genPos)
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

        private void AddDiagonalPos(
            int width, int height, int length,
            List<Pos[]> posArr,
            Func<int, int, Pos> genPos)
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

        private void AddDiagonalPosOneLine(
            int startX, int startY,
            int width, int height, int length,
            List<Pos[]> posArr,
            Func<int, int, Pos> genPos)
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
