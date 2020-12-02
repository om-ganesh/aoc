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

        public static List<Tuple<int, int, char, string>> ReadAllPasswordData(string filename)
        {
            List<Tuple<int, int, char, string>> data = new List<Tuple<int, int, char, string>>();

            var fullPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "data", "day1", filename);
            foreach(var line in File.ReadAllLines(fullPath))
            {
                var inputs = line.Split(' ');
                
                var count = inputs[0].Split('-');
                int minCount = Convert.ToInt32(count[0]);
                int maxCount = Convert.ToInt32(count[1]);
                
                char character = inputs[1][0];
                
                string password = inputs[2];
                
                data.Add(new Tuple<int, int, char, string>(minCount, maxCount, character, password));
            }
            return data;
        }
    }
}
