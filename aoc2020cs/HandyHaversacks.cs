using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    class HandyHaversacks : IProblem
    {
        const string MYBAG = "shinygold";
        List<string> data;
        public HandyHaversacks()
        {
            data = FileReader.GetAllLines("day7-test.txt");
        }

        public void Execute()
        {
            List<(string, List<string>)> lists = new List<(string, List<string>)>();
            foreach(var x in data)
            {
                if(x.Contains("no other bags"))
                {
                    //skip these bags as they don't have child
                    continue;
                }
                var cleanX = x.Replace(" bags", "").Replace(" bag", "").Replace(".", "").Replace(", ", ",");
                var bags = cleanX.Split(new[] { "contain" }, StringSplitOptions.None);
                var parentBag = bags[0].Replace(" ","");
                var childBags = bags[1].Trim().Split(',').ToList().ConvertAll(it => {
                    var np = it.Split(' ').ToList();
                    np.RemoveAt(0);
                    return np.Aggregate( (p,q) => p+q);
                    });
                lists.Add( (parentBag, childBags) );
                Console.WriteLine(x);
            }

            var result = GetOutermostBagsForShinyGold(lists);
            Console.WriteLine($"The list of parent bags: {string.Join(",", result)}");
        }

        private List<string> GetOutermostBagsForShinyGold(List<(string, List<string>)> lists)
        {
            List<string> parents = new List<string>();
            foreach(var pair in lists)
            {
                if(pair.Item2.Contains(MYBAG))
                {
                    parents.Add(pair.Item1);
                }
            }

            foreach (var pair in lists)
            {
                var result = pair.Item2.Intersect(parents);
                if(result.Count()>0)
                {
                    parents.Add(pair.Item1);
                }
            }

            return parents;
        }

        //private List<string> GetAllParentsRecursive(List<(string, List<string>)> lists, List<string> parents)
        //{
        //    foreach (var pair in lists)
        //    {
        //        var result = pair.Item2.Intersect(parents);
        //        if (result.Count() > 0)
        //        {
        //            parents.Add(pair.Item1);
        //        }
        //    }
        //}
    }
}
