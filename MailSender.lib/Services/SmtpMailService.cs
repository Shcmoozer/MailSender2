using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using MailSender.Interfaces;
using WpfMailSender.Services;

namespace MailSender.Services
{
    public class SmtpMailService : IMailService
    {
        

        public IMailSender GetSender(
            string Address, int Port, bool UseSSL,
            string Login, string Password)
        {
            return new SmtpSender(Address, Port, UseSSL, Login, Password);
        }
        
    }

    
}
