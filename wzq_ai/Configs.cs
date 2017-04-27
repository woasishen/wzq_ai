using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wzq_ai
{
    public static class Configs
    {
        public const int DEPTH = 3;
        public const int BORDER_SIZE = 15;

        public const bool SHOW_STEP = true;

        public static Action<string> LogMsg;
    }
}
