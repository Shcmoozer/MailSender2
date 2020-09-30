using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = new MailAddress("pns95@mail.ru", "Павел");
            var to = new MailAddress("pns9595@mail.ru", "Павел");
            

            var message = new MailMessage(from, to);
            //var msg = new MailAddress("user@server.ru", "qwe@ASD.ru");

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма + " + DateTime.Now;

            var client = new SmtpClient("smtp.mail.ru", 25);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "",
                Password = ""
            };

            client.Send(message);

            Console.WriteLine("Hello World!");
        }
    }
}
