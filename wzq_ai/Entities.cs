using System;
using System.Collections.Generic;

namespace wzq_ai
{
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

    public class GlobalConst
    {
        public const int BORDER_SIZE = 15;
    }
}
