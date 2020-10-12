using System;
using System.Threading;

namespace SeparateStreamsTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число");

            var number = long.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            ThreadClass1 threadClass1 = new ThreadClass1(number);
            Thread thread = new Thread(threadClass1.ThreadClassMethod)
            {
                Priority = ThreadPriority.Highest,
                Name = "Вторичный поток"
            };
            thread.Start();

            ThreadClass2 threadClass2 = new ThreadClass2(number);
            Thread thread2 = new Thread(threadClass2.ThreadClassMethod2)
            {
                Priority = ThreadPriority.Highest,
                Name = "Вторичный поток2"
            };
            thread2.Start();
            Console.Read();
        }

    }

    public class ThreadClass1
    {
        private readonly long _number;
        public ThreadClass1(long number) { _number = number; }

        public static void Factorial(long number)
        {

            Thread.Sleep(1000);
            var result = number;
            while (number > 1)
            {
                result *= (number - 1);
                number--;
            }

            Console.WriteLine(result);
        }

        public void ThreadClassMethod()
        {
            Factorial(_number);
        }

    }

    public class ThreadClass2
    {
        private readonly long _number;
        public ThreadClass2(long number)
        {
            _number = number;

        }

        public static void SumInt(long number)
        {

            long result = 0;
            while (0 < number)
            {
                result += (number - 1);
                number--;
                Thread.Sleep(10);
            }

            Console.WriteLine(result);
        }


        public void ThreadClassMethod2()
        {
            SumInt(_number);
        }

    }
}
