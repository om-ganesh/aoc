using aoc2020cs.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020cs
{
    class Day5BinaryBoarding : IProblem
    {
        List<string> data = new List<string>();

        public void Execute()
        {
            data = FileReader.GetAllLines("day5.txt");
            var seatList = GetAllSeatList(data);
            //Show all the seats
            //var seatIds = seatList.ConvertAll(seat => 8 * seat.row + seat.column).ToArray();
            //Array.Sort(seatIds);
            //Console.WriteLine(string.Join(",", seatIds));

            // Problem1
            Console.WriteLine($"Day5:: Total boarding passes:: {seatList.Count}");

            var result = FindMaxSeatId(seatList);
            Console.WriteLine($"Day5:: The highest seat ID is {result}");

            //Problem2
            result = FindMySeatId(seatList);
            Console.WriteLine($"Day5:: My seat ID is {result}");
        }

        private List<(int row, int column)> GetAllSeatList(List<string> data)
        {
            List<(int row, int column)> rowsNColumns = new List<(int row, int column)>();
            foreach (var item in data)
            {
                int row = FindRow(0, 127, item.Substring(0, 7));
                int column = FindColumn(0, 7, item.Substring(7));
                rowsNColumns.Add((row, column));
            }
            return rowsNColumns;
        }

        private int FindMaxSeatId(List<(int row, int column)> seatList)
        {
            int maxSeatId = -1;

            foreach (var seat in seatList)
            {
                var seatId = 8 * seat.row + seat.column;
                if(maxSeatId<seatId)
                {
                    maxSeatId = seatId;
                }
            }

            return maxSeatId;
        }

        // Need to find the missing ids
        private int FindMySeatId(List<(int row, int column)> seatList)
        {
            var seatIds = seatList.ConvertAll(seat => 8 * seat.row + seat.column).ToArray();
            
            //Method1: Using Hash
            HashSet<int> hash = new HashSet<int>(seatIds);
            List<int> probableSeats = new List<int>();
            for (int i = 0; i < seatIds.Length; i++)
            {
                if (!hash.Contains(seatIds[i] + 1))
                {
                    probableSeats.Add(seatIds[i]+1);
                }
            }

            //Method2: Using sorted array 
            //Array.Sort(seatIds);
            //for (int i = 1; i < seatIds.Length-2; i++)
            //{
            //    if (seatIds[i+1] - seatIds[i] > 1)
            //    {
            //        probableSeats.Add(seatIds[i]+1);
            //    }
            //}

            //Console.WriteLine(string.Join(",", probableSeats));
            var myId = Math.Min(probableSeats[0], probableSeats[1]);    //there will be other last seat that doesn't have one number up
            return myId;
        }


        private int FindRow(int front, int back, string str)
        {
            var mid = front + (back - front) / 2;
            if(str.Length==1)
            {
                return (str[0] == 'F') ? front : back;
            }
            else
            {
                if (str[0] == 'F')
                {
                    return FindRow(front, mid, str.Substring(1));
                }
                else
                {
                    return FindRow(1+mid, back, str.Substring(1));
                }
            }
        }

        private int FindColumn(int left, int right, string str)
        {
            var mid = left + (right - left) / 2;
            if (str.Length == 1)
            {
                return (str[0] == 'L') ? left : right;
            }
            else
            {
                if (str[0] == 'L')
                {
                    return FindColumn(left, mid, str.Substring(1));
                }
                else
                {
                    return FindColumn(1 + mid, right, str.Substring(1));
                }
            }
        }

    }
}
