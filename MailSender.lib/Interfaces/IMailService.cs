using System;
using System.Collections.Generic;
using System.Text;
using WpfMailSender.Models;

namespace MailSender.Interfaces
{
    
        public interface IMailService
        {
            IMailSender GetSender(
                string Address, int Port, bool UseSSL,
                string Login, string Password);
        }
        public interface IMailSender
        {
            void Send(
                string From, string To,
                string Title, string Message);
            void Send(string From, IEnumerable<string> To, 
                string Title, string Message);

            void SendParallel(string From, IEnumerable<string> To, 
                string Title, string Message);
    }

        public interface IStorage<T>
        {
            ICollection<T> Items { get; }
            // Метод понадобится для того чтобы считать данные из файла/БД
            void Load();
            // Метод понадобится для того чтобы записать данные в файл/БД
            void SaveChanges();
        }
        public interface IServerStorage : IStorage<Server> { }
        public interface ISendersStorage : IStorage<Sender> { }
        public interface IRecipientsStorage : IStorage<Recipient> { }
        public interface IMessagesStorage : IStorage<Message> { }


}
