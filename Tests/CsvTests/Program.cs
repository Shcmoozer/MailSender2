using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace CsvTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<string> DataCSV = new List<string>();
            string CSVFile = @"..\..\..\Data\CsvTest.csv";
            string[] lines = File.ReadAllLines(CSVFile, Encoding.Default);

            var threadMtd = new ThreadClass(lines);
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(threadMtd.ThreadClassMethod) { Name = i.ToString() };
                thread.Start();
            }

            //Console.ReadLine();
            //Console.WriteLine("Файл обработан");
            //threadMtd.SaveTxt();
            Console.ReadLine();

            Thread threadOne = new Thread(threadMtd.WriteOneThread);
            Thread threadSecond = new Thread(threadMtd.WriteSecondThread);

            threadOne.Start();
            Thread.Sleep(3000);
            threadSecond.Start();

            threadOne.Join();
            threadSecond.Join();

            Console.WriteLine("Запись в файл осуществлена");
            Console.ReadKey();
        }

    }

    public class ThreadClass
    {
        private static readonly object lockObject = new object();
        private readonly string[] _lines;
        private List<string> _sortLines = new List<string>();
        public ThreadClass(string[] lines)
        {
            _lines = lines;
        }

        public void ThreadClassMethod(object state)
        {

            lock (lockObject)
            {
                int numTread = 0;
                if (Thread.CurrentThread.Name != null)
                    numTread = int.Parse((Thread.CurrentThread.Name).Substring(0, 1));

                for (int j = numTread; j < _lines.Length; j += 5)
                {
                    string sortStr = "";
                    var elementTable = _lines[j].Split(';');
                    for (int i = elementTable.Length - 1; i >= 0; i--)
                    {
                        sortStr = sortStr + elementTable[i] + "-";
                    }
                    _sortLines.Add(sortStr);
                }

            }
            Console.WriteLine("Ok");
        }
        public void SaveTxt()
        {
            foreach (var s in _sortLines)
            {
                Console.WriteLine(s);
            }
        }

        public void WriteOneThread()
        {
            StreamWriter file3 = new StreamWriter(@"..\..\..\Data\Result.txt", false);
            Console.WriteLine(new string('-', 20));
            try
            {
                for (int i = 0; i < _sortLines.Count; i += 2)
                {
                    file3.WriteLine(_sortLines[i]);
                }

                //foreach (var value in _sortLines)
                //{
                //    file3.WriteLine(value);
                //}
                file3.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void WriteSecondThread()
        {
            StreamWriter file3 = new StreamWriter(@"..\..\..\Data\Result.txt", true);
            Console.WriteLine(new string('-', 20));
            try
            {
                for (int i = 1; i < _sortLines.Count; i += 2)
                {
                    file3.WriteLine(_sortLines[i]);
                }

                //foreach (var value in _sortLines)
                //{

                //    file3.WriteLine(value);
                //}
                file3.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }

}

