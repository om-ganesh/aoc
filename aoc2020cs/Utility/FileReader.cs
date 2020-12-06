using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aoc2020cs.Utility
{
    // https://adventofcode.com/2020/day/x/input
    class FileReader
    {
        readonly static string ROOT_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        const string DATA_PATH = "data";

        // Download data from URL (Ref: https://github.com/GreenLightning/advent-of-code-downloader)
        public static List<string> GetTodayData()
        {
            // Step1: Create Today filename 
            string fileName = string.Concat("day", DateTime.Now.Day, ".txt");

            // Step2: Download data from Advent of Code website
            RunAocDownloaderToGetData(fileName);
            
            // Step3: Read Lines from file and return
            return ReadStringInputs(fileName);
        }

        
        public static List<int> ReadAllLinesOfNumbers(string filename)
        {
            var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, filename);
            return File.ReadLines(fullPath).Select(line => Convert.ToInt32(line)).ToList();
        }

        public static List<Tuple<int, int, char, string>> ReadAllPasswordData(string filename)
        {
            List<Tuple<int, int, char, string>> data = new List<Tuple<int, int, char, string>>();

            var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, filename);
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

        public static List<string> ReadStringInputs(string filename)
        {
            var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, filename);
            var lines = File.ReadAllLines(fullPath);

            List<string> data = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                data.Add(lines[i]);
            }
            return data;
        }

        public static List<string> GetPassportFiles(string fileName)
        {
            var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, fileName);
            List<string> data = new List<string>();

            var lines = File.ReadAllLines(fullPath);
            StringBuilder input = new StringBuilder();

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (input.Length == 0)
                    {
                        input.Append(line);
                    }
                    else
                    {
                        input.Append(" " + line);
                    }
                }
                else
                {
                    data.Add(input.ToString());
                    input.Clear();
                }
            }
            //Add the last string
            data.Add(input.ToString());

            return data;
        }


        private static void RunAocDownloaderToGetData(string fileName)
        {
            try
            {
                //Simple Download
                //Process.Start(@"C:\Users\subas\go\bin\aocdl.exe");

                //Using output argument to provide downloadFilename for aocdl 
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = @"C:\Users\subas\go\bin\aocdl.exe";
                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.Arguments = "-output " + fileName;
                    myProcess.Start();
                    //Use kill method to terminate the process, if it is not self-terminating
                }

                //Move file to data folder
                Thread.Sleep(2000);
                var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                File.Move(fileName, fullPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
