using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileHandlingTests
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"..\..\..\Data";
            DirectoryInfo dirInfo = new DirectoryInfo(directory);
            FileInfo[] files = dirInfo.GetFiles();

            var listResult = ProcessDataTestAsync(files);

            StreamWriter file3 = new StreamWriter(@"..\..\..\Result\result.dat");

            foreach (var s in listResult.Result)
            {
                //Console.WriteLine(s);
                file3.WriteLine(s);
            }
            file3.Close();

            Console.ReadKey();
        }

        private static async Task<float> ParallelMethod(string items)
        {
            float result = 0;
            using var file = new StreamReader(items);
            var line = await file.ReadLineAsync().ConfigureAwait(false);

            if (line != null)
            {

                var dat = line.Replace('.', ',').Split(' ');

                if (int.Parse(dat[0]) == 1)
                {
                    result = float.Parse(dat[1]) * float.Parse(dat[2]);
                }
                else if (int.Parse(dat[0]) == 2)
                {
                    result = float.Parse(dat[1]) / float.Parse(dat[2]);
                }
                else
                {
                    return 0;
                }
            }

            return result;
        }

        public static async Task<List<float>> ProcessDataTestAsync(FileInfo[] files)
        {
            var listResult = new List<float>();
            var tasks = files.Select(line => ParallelMethod(line.ToString()));

            Console.WriteLine(">>> Подготовка к запуску обработки файлов...");

            var runingTasks = tasks.ToArray();

            Console.WriteLine(">>> Задачи созданы");

            await Task.WhenAll(runingTasks).ContinueWith(
                (i) => { listResult.AddRange(runingTasks.Select(task => task.Result)); });

            Console.WriteLine(">>> Обработка всех сообщений файлов");
            return listResult;
        }

    }
}
