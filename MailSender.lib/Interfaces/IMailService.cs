using System;
using System.Collections.Generic;
using System.Text;

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
        }


}
