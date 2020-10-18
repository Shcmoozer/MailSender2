using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailSender.Interfaces;
using WpfMailSender.Models;

namespace WpfMailSender.Data.Stores.InMemory
{
    class RecipientsStoreInMemory : IStore<Recipient>
    {
        private readonly TestData _testData = new TestData();
        public IEnumerable<Recipient> GetAll() => _testData.Recipients;

        public Recipient GetById(int Id) => GetAll().FirstOrDefault(r => r.Id == Id);

        public Recipient Add(Recipient Item)
        {
            if (_testData.Recipients.Contains(Item)) return Item;

            Item.Id = _testData.Recipients.DefaultIfEmpty().Max(r => r.Id) + 1;
            _testData.Recipients.Add(Item);
            return Item;
        }

        public void Update(Recipient Item) { }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            _testData.Recipients.Remove(item);
        }
    }
}
