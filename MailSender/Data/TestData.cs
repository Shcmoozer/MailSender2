using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using WpfMailSender.Models;


namespace WpfMailSender.Data
{
    class TestData
    {
        public static TestData LoadFromXML(string FileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.OpenText(FileName);
            return (TestData)serializer.Deserialize(file);
        }
        public void SaveToXML(string FileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.Create(FileName);
            serializer.Serialize(file, this);
        }

        public IList<Server> Servers { get; set; } = new List<Server>
        {
            new Server
            {
                //Id = 1,
                Name = "Mail.ru",
                Address = "smtp.mail.ru",
                Port = 25,
                UseSSL = true,
                Login = "pns95@mail.ru",
                Password = "PassWord",
            },
            new Server
            {
                //Id = 2,
                Name = "gMail",
                Address = "smtp.gmail.com",
                Port = 465,
                UseSSL = true,
                Login = "user@gmail.com",
                Password = "PassWord",
            },
            new Server
            {
                //Id = 1,
                Name = "Яндекс",
                Address = "smtp.yandex.ru",
                Port = 587,
                UseSSL = true,
                Login = "user@yandex.ru",
                Password = "PassWord",
            }//,
        };

        public IList<Sender> Senders { get; set; } = new List<Sender>
        {
            new Sender
            {
                //Id = 1,
                Name = "Пупкин",
                Address = "pns95@mail.ru",
                Description = "Почта от Пупкина"
            },
            new Sender
            {
                //Id = 2,
                Name = "Владимиров",
                Address = "vlad@server.ru",
                Description = "Почта от Владимирова"
            },
            new Sender
            {
                //Id = 3,
                Name = "Распутин",
                Address = "rasputin@server.ru",
                Description = "Почта от Распутина"
            }//,

        };

        public IList<Recipient> Recipients { get; set; } = new List<Recipient>
        {
            new Recipient
            {
                //Id = 1,
                Name = "Пупкин",
                Address = "pns9595@mail.ru",
                Description = "Почта для Пупкина"
            },
            new Recipient
            {
                //Id = 2,
                Name = "Владимиров",
                Address = "vlad@server.ru",
                Description = "Почта для Владимирова"
            },
            new Recipient
            {
                //Id = 3,
                Name = "Распутин",
                Address = "rasputin@server.ru",
                Description = "Почта для Распутина"
            }//,
        };

        public IList<Message> Messages { get; set; } = Enumerable
            .Range(1, 10)
            .Select(i => new Message
            {
                //Id = i,
                Tittle = $"Сообщение {i}",
                Body = $"Текст сообщения {i}"
            })
            .ToList();
    }
}
