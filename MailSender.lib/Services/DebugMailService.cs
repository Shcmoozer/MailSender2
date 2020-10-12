using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MailSender.Interfaces;

namespace MailSender.Services
{
    public class DebugMailService : IMailService
    {
        public IMailSender GetSender(
            string Address, int Port, bool UseSSL,
            string Login, string Password) =>
            new DebugMailSender(Address, Port, UseSSL, Login, Password);

        private class DebugMailSender : IMailSender
        {
            private readonly string _Address;
            private readonly int _Port;
            private readonly bool _UseSsl;
            private readonly string _Login;
            private readonly string _Password;
            public DebugMailSender(
                string Address, int Port, bool UseSsl,
                string Login, string Password)
            {
                _Address = Address;
                _Port = Port;
                _UseSsl = UseSsl;
                _Login = Login;
                _Password = Password;
            }
            public void Send(string From, string To, string Title, string Message)
            {
                Debug.WriteLine(
                    "Почтовый сервер {0}:{1}(ssl:{2})[login:{3} - pass:{4}]",
                    _Address, _Port, _UseSsl, _Login, _Password);
                Debug.WriteLine("Отправка письма от:{0} к:{1}\r\n\t{2}\r\n{3}",
                    From, To, Title, Message);
            }

            public void Send(string From, IEnumerable<string> To, string Title, string Message)
            {
                foreach (var recipient_address in To)
                    Send(From, recipient_address, Title, Message);
            }

            public void SendParallel(string From, IEnumerable<string> To, string Title, string Message)
            {
                Send(From, To, Title, Message);
            }
        }

    }

}

