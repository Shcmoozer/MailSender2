using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Interfaces;
using WpfMailSender.Models;

namespace MailSender.Services
{
    class InMemoryDataStorage :
        IServerStorage, ISendersStorage,
        IRecipientsStorage, IMessagesStorage
    {
        // Свойства для непосредственного использования объекта
        // по имени класса InMemoryDataStorage
        public ICollection<Server> Servers { get; set; }
        public ICollection<Sender> Senders { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public ICollection<Message> Messages { get; set; }
        // Свойства для удовлетворения требований интерфейсов
        // IServerStorage, ISendersStorage, IRecipientsStorage, IMessagesStorage
        ICollection<Server> IStorage<Server>.Items => Servers;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Message> IStorage<Message>.Items => Messages;

        public void Load()
        {
            // Здесь будем инициализировать свойства данными
        }

        public void SaveChanges()
        {
            // Здесь ничего не будем делать, так как данные лежат в памяти
        }
    }
}
