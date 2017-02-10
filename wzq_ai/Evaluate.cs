using System;
using System.Collections.Generic;
using System.Linq;

namespace wzq_ai
{
    public class Evaluate
    {
        public static readonly Dictionary<int, int> GOLE_DICT = new Dictionary<int, int>
        {
            {0,0 },
            {1,1 },
            {2,50 },
            {3,2500 },
            {4,125000 },
            {5,6250000 }
        };

        private readonly Border border;
        private readonly List<Pos[]> posLineArr = new List<Pos[]>(); //所有可能的5连格
        private readonly Dictionary<Pos, List<Pos[]>> posContainersDict = 
            new Dictionary<Pos, List<Pos[]>>();//包含特定位置的所有5连格

        public Evaluate(Border border)
        {
            this.border = border;

            //横的5连
            AddDirectPos(
                GlobalConst.BORDER_SIZE,
                GlobalConst.BORDER_SIZE, 
                5, 
                (y, x) => new Pos(x, y));

            //竖的5连
            AddDirectPos(
                GlobalConst.BORDER_SIZE, 
                GlobalConst.BORDER_SIZE, 
                5,
                (x, y) => new Pos(x, y));

            //正对角的5连
            AddDiagonalPos(
                GlobalConst.BORDER_SIZE,
                GlobalConst.BORDER_SIZE, 
                5, 
                (x, y) => new Pos(x, y));

            //反对角的5连
            AddDiagonalPos(
                GlobalConst.BORDER_SIZE,
                GlobalConst.BORDER_SIZE,
                5, 
                (x, y) => new Pos(GlobalConst.BORDER_SIZE - x - 1, y));

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

        public void GenePosGole(Pos pos, out int black, out int white)
        {
            black = white = 0;
            if (border.GetCellStatus(pos) != CellStatus.Empty)
            {
                return;
            }
            foreach (var line in posContainersDict[pos])
            {
                UpdatePosLineGole(line, ref black, ref white);
            }
        }

        private void UpdatePosLineGole(Pos[] posLine, ref int black,ref int white)
        {
            var result = posLine.Sum(p => (int) border.GetCellStatus(p));
            //不含白子
            if (result < 10)
            {
                black += GOLE_DICT[result + 1];
            }
            //不含黑子
            if (result%10 == 0)
            {
                white += GOLE_DICT[result/10 + 1];
            }
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
