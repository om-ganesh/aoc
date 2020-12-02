using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs.Utility
{
    class FileReader
    {
        public static List<int> ReadAllLinesOfNumbers(string filename)
        {
            var fullPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "data", "day1", filename);
            return File.ReadLines(fullPath).Select(line => Convert.ToInt32(line)).ToList();
        }
    }
}
