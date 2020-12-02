using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AOC 2020");

            IProblem problem = new Day1SumTo2020();
            problem.Execute();

            Console.ReadLine();

        }
    }
}
