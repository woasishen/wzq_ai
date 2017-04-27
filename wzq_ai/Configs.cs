using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wzq_ai
{
    public static class Configs
    {
        public const int BORDER_SIZE = 15;
        public static int Depth = 4;
        public static bool ShowStep = false;
        public static bool UseMaxMin = true;

        public static Action<string> LogMsg;
    }
}
