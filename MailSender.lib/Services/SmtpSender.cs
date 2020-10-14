using System;
using System.Collections.Generic;
using System.Text;
using WpfMailSender.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Interfaces;


namespace WpfMailSender.Services
{
    public class SmtpSender : IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;
        public SmtpSender(
            string Address, int Port, bool UseSSL,
            string Login, string Password)
        {
            _Address = Address;
            _Port = Port;
            _UseSsl = UseSSL;
            _Login = Login;
            _Password = Password;
        }

        public void Send(
            string From, string To,
            string Title, string Message)
        {
            using var message = new MailMessage(From, To)
            {
                Subject = Title,
                Body = Message
            };
            using var client = new SmtpClient(_Address, _Port)
            {
                EnableSsl = _UseSsl,
                Credentials = new NetworkCredential(_Login, _Password)
            };
            client.Send(message);
        }

        public void Send(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientsAddresses)
                Send(SenderAddress, recipient_address, Subject, Body);
        }

        public void SendParallel(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientsAddresses)
                ThreadPool.QueueUserWorkItem(o => Send(SenderAddress, recipient_address, Subject, Body));
        }

        public async Task SendAsync(string SenderAddress, string RecipientAddress, string Subject, string Body,
            CancellationToken Cancel = default)
        {
            
        }

        public async Task SendAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            IProgress<(string Recipient, double Percent)> Progress = null, CancellationToken Cancel = default)
        {
           
        }

        public async Task SendParallelAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            CancellationToken Cancel = default)
        {
            
        }
    }
}
