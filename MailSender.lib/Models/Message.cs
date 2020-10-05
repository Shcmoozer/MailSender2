using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Models.Base;

namespace WpfMailSender.Models
{
    public class Message : Entity
    {
        public string Tittle { get; set; }
        public string Body { get; set; }
    }
}
