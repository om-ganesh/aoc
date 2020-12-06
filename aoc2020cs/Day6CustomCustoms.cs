using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    class Day6CustomCustoms : IProblem
    {
        List<char[]> data;
        public Day6CustomCustoms()
        {
            data = new List<char[]>();
        }

        public void Execute()
        {
            // Problem 1
            data = GetCustomAnswers1("day6.txt");
            int count = 0;
            foreach (var x in data)
            {
                count += x.Length;
            }
            Console.WriteLine($"The total unique answers is {count}");


            //Problem 2
            data.Clear();
            count = 0;

            data = GetCustomAnswers2("day6.txt");
            foreach (var x in data)
            {
                count += x.Length;
            }
            Console.WriteLine($"The total common YES in a group is {count}");
        }

        private List<char[]> GetCustomAnswers1(string fileName)
        {
            List<char[]> data = new List<char[]>();

            StringBuilder input = new StringBuilder();
            foreach (var line in FileReader.GetAllLines(fileName))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (input.Length == 0)
                    {
                        input.Append(line);
                    }
                    else
                    {
                        input.Append(line);
                    }
                }
                else
                {
                    var result = input.ToString().ToCharArray().Distinct().ToArray();
                    data.Add(result);
                    input.Clear();
                }
            }
            //Add the last string
            data.Add(input.ToString().ToCharArray().Distinct().ToArray());

            return data;
        }


        private List<char[]> GetCustomAnswers2(string fileName)
        {
            List<char[]> data = new List<char[]>();

            List<char> input = new List<char>();
            bool newLine = true;
            foreach (var line in FileReader.GetAllLines(fileName))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (newLine)
                    {
                        input = line.ToCharArray().ToList();
                    }
                    else
                    {
                        input = input.Intersect(line.ToCharArray().ToList()).ToList();
                    }
                    newLine = false;
                }
                else
                {
                    newLine = true;
                    if (input.Count > 0)
                    {
                        data.Add(input.ToArray());
                        input.Clear();
                    }
                }
            }
            //Add the last string
            data.Add(input.ToArray());

            return data;
        }
    }
}
