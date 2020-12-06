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
            return GetAllLines(fileName);
        }

        public static List<string> GetAllLines(string fileName)
        {
            var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, fileName);
            return File.ReadAllLines(fullPath).ToList();
        }

        public static List<int> GetAllLinesAsNumbers(string fileName)
        {
            var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, fileName);
            return File.ReadAllLines(fullPath).Select(line => Convert.ToInt32(line)).ToList();
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
                    myProcess.StartInfo.Arguments = "-force -output " + fileName;
                    myProcess.Start();
                    //Use kill method to terminate the process, if it is not self-terminating
                }

                //Move file to data folder
                Thread.Sleep(2000);
                var fullPath = Path.Combine(ROOT_PATH, DATA_PATH, fileName);
                File.Move(fileName, fullPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
