using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace WPFTests
{
    class EmailSendServiceClass
    {
        private MailAddress from;
        private MailAddress to;
        private MailMessage message;
        private SmtpClient client;

        public EmailSendServiceClass(NetworkCredential credential)
        {
            from = new MailAddress(DataStorage.MailFrom, DataStorage.NameFrom);
            to = new MailAddress(DataStorage.MailTo);
            message = new MailMessage(from, to)
            {
                Subject = DataStorage.MsgSubject + DateTime.Now,
                Body = DataStorage.MsgBody + DateTime.Now
            };
            client = new SmtpClient(DataStorage.SmtpHost, DataStorage.SmtpPort)
            {
                EnableSsl = true,
                Credentials = credential
            };
        }

        public void Send()
        {
            client.Send(message);
        }
    }
}
