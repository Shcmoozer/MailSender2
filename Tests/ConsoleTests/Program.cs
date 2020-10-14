using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var from = new MailAddress("pns95@mail.ru", "Павел");
            //var to = new MailAddress("pns9595@mail.ru", "Павел");


            //var message = new MailMessage(from, to);
            ////var msg = new MailAddress("user@server.ru", "qwe@ASD.ru");

            //message.Subject = "Заголовок письма от " + DateTime.Now;
            //message.Body = "Текст тестового письма + " + DateTime.Now;

            //var client = new SmtpClient("smtp.mail.ru", 25);
            //client.EnableSsl = true;

            //client.Credentials = new NetworkCredential
            //{
            //    UserName = "",
            //    Password = ""
            //};

            //client.Send(message);

            //Console.WriteLine("Hello World!");

            //ThreadTests.Start();
            //CriticalSectionTests.Start();
            //ThreadPoolTests.Start();

            //Console.WriteLine("Главный поток работу закончил!");

            //--------------------
            //Перемножение матриц

            //RndMatrix(100, 100, out var matrix1);
            //RndMatrix(100, 100, res: out var matrix2);
            int[,] matrix1 = new int[100,100];
            int[,] matrix2 = new int[100, 100];

            Parallel.Invoke(
                () =>
                {
                    RndMatrix(100, 100, out matrix1);
                    RndMatrix(100, 100, res: out matrix2);
                }
                );

            var multiMatrix = new MatrixMultiplication(matrix1, matrix2);
            var result = multiMatrix.Multiplication();

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    
                    Console.Write("{0}\t", result[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        static int[,] RndMatrix(int x, int y, out int[,] res)
        {
            var myArr = new int[x, y];
            var ran = new Random();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    myArr[i, j] = ran.Next(0, 10);
                    //Console.Write("{0}\t", myArr[i, j]);
                }
                //Console.WriteLine();
            }

            res = myArr;
            return myArr;
        }

    }
}
