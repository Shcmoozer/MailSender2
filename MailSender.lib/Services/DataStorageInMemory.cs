using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Interfaces;
using System.Diagnostics;
using System.Linq;
using WpfMailSender.Models;

namespace MailSender.Services
{
    public class DataStorageInMemory :
        IServerStorage, ISendersStorage,
        IRecipientsStorage, IMessagesStorage
    {
        public ICollection<Server> Servers { get; set; } = new List<Server>();
        public ICollection<Sender> Senders { get; set; } = new List<Sender>();
        public ICollection<Recipient> Recipients { get; set; }
            = new List<Recipient>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        ICollection<Server> IStorage<Server>.Items => Servers;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Message> IStorage<Message>.Items => Messages;

        public void Load()
        {
            Debug.WriteLine("Вызвана процедура загрузки данных");
            if (Servers is null || Servers.Count == 0)
                Servers = new List<Server>
                {
                    new Server
                    {
                        Id = 1,
                        Name = "Яндекс",
                        Address = "smpt.yandex.ru",
                        Port = 465,
                        UseSSL = true,
                        Login = "user@yandex.ru",
                        Password = "PassWord",
                    },
                    new Server
                    {
                        Id = 2,
                        Name = "gMail",
                        Address = "smpt.gmail.com",
                        Port = 465,
                        UseSSL = true,
                        Login = "user@yandex.ru",
                        Password = "PassWord",
                    },
                };
            if (Senders is null || Senders.Count == 0)
                Senders = new List<Sender>
                {
                    new Sender
                    {
                        Id = 1,
                        Name = "Иванов",
                        Address = "ivanov@server.ru",
                        Description = "Почта от Иванова"
                    },
                    new Sender
                    {
                        Id = 2,
                        Name = "Петров",
                        Address = "petrov@server.ru",
                        Description = "Почта от Петрова"
                    },
                    new Sender
                    {
                        Id = 3,
                        Name = "Сидоров",
                        Address = "sidorov@server.ru",
                        Description = "Почта от Сидорова"
                    },
                };
            if (Recipients is null || Recipients.Count == 0)
                Recipients = new List<Recipient>
                {
                    new Recipient
                    {
                        Id = 1,
                        Name = "Иванов",
                        Address = "ivanov@server.ru",
                        Description = "Почта для Иванова"
                    },
                    new Recipient
                    {
                        Id = 2,
                        Name = "Петров",
                        Address = "petrov@server.ru",
                        Description = "Почта для Петрова"
                    },
                    new Recipient
                    {
                        Id = 3,
                        Name = "Сидоров",
                        Address = "sidorov@server.ru",
                        Description = "Почта для Сидорова"
                    },
                };
            if (Messages is null || Messages.Count == 0)
                Messages = Enumerable
                    .Range(1, 10)
                    .Select(i => new Message
                    {
                        Id = i,
                        Tittle = $"Сообщение {i}",
                        Body = $"Текст сообщения {i}"
                    })
                    .ToList();
        }
        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}

