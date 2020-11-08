using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WpfMailSender.Data
{
    class MailSenderDbInitializer
    {
        private readonly MailSenderDB _db;
        private readonly TestData _testData = new TestData();

        //public MailSenderDbInitializer(MailSenderDB db, TestData testData)
        //{
        //    _db = db;
        //    _testData = new TestData();
        //}
        public MailSenderDbInitializer(MailSenderDB db)
        {
            _db = db;
        }
        public void Initialize()
        {
            _db.Database.Migrate();

            InitializeRecipients();
            InitializeSenders();
            InitializeServers();
            InitializeMessages();
        }

        private void InitializeRecipients()
        {
            if (_db.Recipients.Any()) return;

            _db.Recipients.AddRange(_testData.Recipients);
            _db.SaveChanges();
        }

        private void InitializeSenders()
        {
            if (_db.Senders.Any()) return;

            _db.Senders.AddRange(_testData.Senders);
            _db.SaveChanges();
        }

        private void InitializeMessages()
        {
            if (_db.Messages.Any()) return;

            _db.Messages.AddRange(_testData.Messages);
            _db.SaveChanges();
        }

        private void InitializeServers()
        {
            if (_db.Servers.Any()) return;

            _db.Servers.AddRange(_testData.Servers);
            _db.SaveChanges();
        }
    }
}
