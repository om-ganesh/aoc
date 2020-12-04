using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020cs
{
    // https://adventofcode.com/2020/day/4
    class Day4PassportProcessing : IProblem
    {
        List<string> data = new List<string>();

        public void Execute()
        {
            data = FileReader.GetPassportFiles("day4-passportbatch.txt");

            var result1 = FindSolution1(data);
            var result2 = FindSolution2(result1);

            Console.WriteLine($"Day4:: The total valid passports are {result1.Count} and second answer is {result2.Count}");
        }

        private List<string> FindSolution1(List<string> data)
        {
            List<string> result = new List<string>();
            
            Console.WriteLine($"Total password received {data.Count()}");
            
            foreach(var passport in data)
            {
                var fieldsCount = passport.Split(' ').Length;
                if (fieldsCount >7 ||
                    (fieldsCount == 7 && passport.IndexOf("cid:") == -1))
                {
                    result.Add(passport);
                }
            }

            return result;
        }

        private List<string> FindSolution2(List<string> data)
        {
            List<string> result = new List<string>();

            Console.WriteLine($"Total password received {data.Count()}");

            foreach (var passport in data)
            {
                //check individual vlaues, and immediately skip if something is found error

                // check birth year, issue year, expiry year
                var byr = Convert.ToInt32(passport.Substring(passport.IndexOf("byr:") + 4).Split(' ')[0]);
                var iyr = Convert.ToInt32(passport.Substring(passport.IndexOf("iyr:") + 4).Split(' ')[0]);
                var eyr = Convert.ToInt32(passport.Substring(passport.IndexOf("eyr:") + 4).Split(' ')[0]);
                if (byr < 1920 || byr > 2002 || iyr < 2010 | iyr > 2020 || eyr < 2020 || eyr > 2030)
                {
                    continue;
                }

                // check height
                string hgt = passport.Substring((passport.IndexOf("hgt:") + 4)).Split(' ')[0];
                var unit = hgt.Substring(hgt.Length - 2); 
                if (!(unit == "cm" || unit == "in"))
                {
                    continue;
                }

                var value = Convert.ToInt32(hgt.Substring(0, hgt.Length - 2));
                if (!((unit == "cm" && value >= 150 && value <= 193)
                    || (unit == "in" && value >= 59 && value <= 76)))
                {
                    continue;
                }

                // check hair color
                var hcl = passport.Substring(passport.IndexOf("hcl:") + 4).Split(' ')[0];
                if (!hcl.StartsWith("#") || hcl.Length<7 || !Regex.IsMatch(hcl.Substring(1), "[a-z0-9]"))
                {
                    continue;
                }

                // check eye color
                var ecl = passport.Substring(passport.IndexOf("ecl:") + 4).Split(' ')[0];
                var validColors = new string[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                if (!validColors.Contains(ecl))
                {
                    continue;
                }

                // check passport
                var pass = passport.Substring(passport.IndexOf("pid:") + 4).Split(' ')[0];
                if(pass.Length != 9 || ! Regex.IsMatch(pass,"[0-9]{9}"))
                {
                    continue;
                }

                // If all is good above, then increaes the count
                var items = passport.Split(' ')
                    .Where(x => !x.StartsWith("cid"))
                    .OrderBy(x => x);
                Console.WriteLine(string.Join("\t",items));
                result.Add(passport);
            }

            return result;
        }
    }
}
