using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using MailSender.Interfaces;
using WpfMailSender.Models;


namespace MailSender.Services
{
    class DataStorageInXmlFile :
        IServerStorage, ISendersStorage,
        IRecipientsStorage, IMessagesStorage
    {
        // Определим внутреннюю структуру данных для удобства
        // сериализации/десериализации
        public class DataStructure
        {
            public List<Server> Servers { get; set; } = new List<Server>();
            public List<Sender> Senders { get; set; } = new List<Sender>();
            public List<Recipient> Recipients { get; set; }
                = new List<Recipient>();
            public List<Message> Messages { get; set; } =
                new List<Message>();
        }

        // Хранилище будет помнить с каким файлом оно работает
        private readonly string _FileName;
        // В конструкторе будем задавать путь к файлу данных
        public DataStorageInXmlFile(string FileName) => _FileName = FileName;
        // Определим свойство с данными хранилища
        private DataStructure Data { get; set; } = new DataStructure();
        // Реализация свойств для интерфейсов (явная реализация интерфейсов)
        ICollection<Server> IStorage<Server>.Items => Data.Servers;
        ICollection<Sender> IStorage<Sender>.Items => Data.Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Data.Recipients;
        ICollection<Message> IStorage<Message>.Items => Data.Messages;
        // При загрузке данных будем выполнять десериализацию контейнера данных
        public void Load()
        {
            if (!File.Exists(_FileName))
            {
                Data = new DataStructure();
                return;
            }
            using var file = File.OpenText(_FileName);
            if (file.BaseStream.Length == 0)
            {
                Data = new DataStructure();
                return;
            }
            var serializer = new XmlSerializer(typeof(DataStructure));
            Data = (DataStructure)serializer.Deserialize(file);
        }
        // При сохранении будем сериализовать данные в файл
        public void SaveChanges()
        {
            using var file = File.CreateText(_FileName);
            var serializer = new XmlSerializer(typeof(DataStructure));
            serializer.Serialize(file, Data);
        }
    }
}
