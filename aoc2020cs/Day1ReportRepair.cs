using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    // https://adventofcode.com/2020/day/1
    class Day1ReportRepair: IProblem
    {
        List<int> data = new List<int>();

        public Day1ReportRepair()
        {
            data.AddRange(new int[]{1721, 979, 366, 299, 675, 1456 });
        }
        public void Execute()
        {
            data = FileReader.ReadAllLinesOfNumbers("2020.txt");
            var result1 = GetMultiplicationOfTwo(data);
            var result2 = GetMultiplicationOfThree(data);
            Console.WriteLine($"Result1 is {result1} and Result2 is {result2}");
        }

        /// <summary>
        /// find the two entries that sum to 2020 and then multiply those two numbers together.
        /// 1721
        /// 979
        /// 366
        /// 299
        /// 675
        /// 1456
        /// In this list, the two entries that sum to 2020 are 1721 and 299. Multiplying them together produces 1721 * 299 = 514579, so the correct answer is 514579.
        /// </summary>
        private int GetMultiplicationOfTwo(List<int> numbers)
        {
            bool isExists = false;
            int number1 = 0;

            HashSet<int> hash = new HashSet<int>(numbers);
            foreach(int x in numbers)
            {
                if(x<=2020 && hash.Contains(2020-x))
                {
                    isExists = true;
                    number1 = x;
                    break;
                }
            }
            if(isExists)
            {
                return number1 * (2020-number1);
            }
            return -1;
        }

        /// <summary>
        /// what is the product of the three entries that sum to 2020?
        /// 1721
        /// 979
        /// 366
        /// 299
        /// 675
        /// 1456
        /// Using the above example again, the three entries that sum to 2020 are 979, 366, and 675. Multiplying them together produces the answer, 241861950
        /// </summary>
        private int GetMultiplicationOfThree(List<int> numbers)
        {
            bool isExists = false;
            int number1 = 0;
            int number2 = 0;

            //remove all entires that is more than 2020
            numbers = numbers.FindAll(x => x < 2020);
            HashSet<int> hash = new HashSet<int>(numbers);
            for(int i=0; i<numbers.Count; i++)
            {
                for(int j=0; j<numbers.Count; j++)
                {
                    if( (numbers[i] + numbers[j] <= 2020) && hash.Contains(2020-numbers[i]-numbers[j]))
                    {
                        isExists = true;
                        number1 = numbers[i];
                        number2 = numbers[j];
                        break;
                    }
                }
            }
            if(isExists)
            {
                return number1 * number2 * (2020-number1-number2);
            }
            return -1;
        }
    }
}
