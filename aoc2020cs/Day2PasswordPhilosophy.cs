using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    // https://adventofcode.com/2020/day/2
    class Day2PasswordPhilosophy : IProblem
    {
        List<Tuple<int, int, char, string>> data = new List<Tuple<int, int, char, string>>();

        public Day2PasswordPhilosophy()
        {
            data.Add(new Tuple<int, int, char, string>(1, 3, 'a', "abcde"));
            data.Add(new Tuple<int, int, char, string>(1, 3, 'b', "cdefg"));
            data.Add(new Tuple<int, int, char, string>(2, 9, 'c', "ccccccccc"));
        }
        public void Execute()
        {
            data = ReadAllPasswordData("day2.txt");
            
            var result1 = GetValidPasswordCount(data);
            var result2 = GetNewValidPasswordCount(data);
            
            Console.WriteLine($"Day2:: The first answer is {result1} and the second answer is {result2}");
        }

        /// <summary>
        /// 1-3 a: abcde
        /// 1-3 b: cdefg
        /// 2-9 c: ccccccccc
        /// How many passwords are valid according to their policies?
        /// Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character, and so on.
        /// Exactly one of these positions must contain the given letter. Other occurrences of the letter are irrelevant for the purposes of policy enforcement.
        /// </summary>
        private int GetNewValidPasswordCount(List<Tuple<int, int, char, string>> data)
        {
            int result = 0;

            foreach(var d in data)
            {
                int firstIndex = d.Item1;
                int secondIndex = d.Item2;
                char character = d.Item3;
                string password = d.Item4;

                //Some pre-defined rules
                if(firstIndex > password.Length)
                {
                    continue;
                }
                var containsFirst= (password.ElementAt(firstIndex-1) == character);
                var containsSecond= (password.ElementAt(secondIndex-1) == character);
                if( (containsFirst && !containsSecond) || (!containsFirst && containsSecond))
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// 1-3 a: abcde
        /// 1-3 b: cdefg
        /// 2-9 c: ccccccccc
        /// How many passwords are valid according to their policies?
        /// In the above example, 2 passwords are valid. The middle password, cdefg, is not; it contains no instances of b, but needs at least 1. 
        /// The first and third passwords are valid: they contain one a or nine c, both within the limits of their respective policies.
        /// </summary>
        private int GetValidPasswordCount(List<Tuple<int, int, char, string>> data)
        {
            int result = 0;

            foreach(var d in data)
            {
                int minCount = d.Item1;
                int maxCount = d.Item2;
                char character = d.Item3;
                string password = d.Item4;

                //Some pre-defined rules
                if(minCount == 0)
                {
                    result++;
                    continue;
                }
                if (password.Length < minCount || maxCount== 0)
                { 
                    continue;
                }

                int counter=0;
                foreach(char ch in password)
                {
                    if(ch == character)
                    {
                        counter++;
                    }
                }
                if(counter>=minCount && counter<=maxCount)
                {
                    result++;
                }
            }

            return result;
        }


        private List<Tuple<int, int, char, string>> ReadAllPasswordData(string fileName)
        {
            List<Tuple<int, int, char, string>> data = new List<Tuple<int, int, char, string>>();
            foreach (var line in FileReader.GetAllLines(fileName))
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
