using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static readonly Dictionary<int, int> GOLE_DICT = new Dictionary<int, int>
            {
                {1,1 },
                {2,100 },
                {3,10000 },
                {4,1000000 },
                {5,100000000 }
            };
        static void Main(string[] args)
        {

            Console.WriteLine(-GOLE_DICT[5]);
        }
    }
}
