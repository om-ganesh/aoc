using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    class DayXProblem : IProblem
    {
        List<string> data;
        public DayXProblem()
        {
            data = FileReader.GetTodayData();
        }

        public void Execute()
        {
            foreach(var x in data)
            {
                Console.WriteLine(x);
            }
            
        }
    }
}
