using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    // https://adventofcode.com/2020/day/3
    class Day3TobogganTrajectory : IProblem
    {
        List<string> data = new List<string>();

        public void Execute()
        {
            data = FileReader.ReadStringInputs("day3-problem.txt");

            var result1 = FindSolution1(data);
            var result2 = FindSolution2(data);

            Console.WriteLine($"Day3:: The first answer is {result1} and second answer is {result2}");
        }

        private int FindSolution1(List<string> data)
        {
            var arr = data.ToArray();
            int result = 0;
            int index = 3;
            Console.WriteLine($"lines are {data.Count()} and width is {data[0].Length}");
            // Rule 3 chars to right and 1 char down
            for (int i=1; i<arr.Length;i++)
            {
                if(arr[i][index] == '#')
                {
                    result++;
                }
                index = (index + 3) % arr[0].Length;
            }

            return result;
        }

        // ans1:1395553904
        // ans2:712691360
        private double FindSolution2(List<string> data)
        {
            var arr = data.ToArray();
            double[] results = new double[5];
            int[] indexes = new int[]{ 1, 3, 5, 7, 1};
            // Rule 3 chars to right and 1 char down
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i][indexes[0]] == '#')
                {
                    results[0] = results[0] + 1;
                }
                if (arr[i][indexes[1]] == '#')
                {
                    results[1] = results[1] + 1;
                }
                if (arr[i][indexes[2]] == '#')
                {
                    results[2] = results[2] + 1;
                }
                if (arr[i][indexes[3]] == '#')
                {
                    results[3] = results[3] + 1;
                }
                indexes[0] = (indexes[0] + 1) % arr[0].Length;
                indexes[1] = (indexes[1] + 3) % arr[0].Length;
                indexes[2] = (indexes[2] + 5) % arr[0].Length;
                indexes[3] = (indexes[3] + 7) % arr[0].Length;
                if(i%2 == 0)
                {
                    if (arr[i][indexes[4]] == '#')
                    {
                        results[4] = results[4] + 1;
                    }
                    indexes[4] = (indexes[4] + 1) % arr[0].Length;
                }
            }

            Console.WriteLine(string.Join(",", results));
            return results.Aggregate( (x,y) => x*y);
        }
    }
}
