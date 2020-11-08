using System;
using System.Collections.Generic;
using System.Text;
using WpfMailSender.Models;

namespace MailSender.Interfaces
{
    public interface IMailSchedulerService
    {
        void Start();

        void Stop();

        void AddTask(DateTime Time, Sender Sender, IEnumerable<Recipient> Recipients, Server Server, Message Message);
    }
}
