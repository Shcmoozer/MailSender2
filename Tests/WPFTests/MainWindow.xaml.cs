using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WPFTests
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            DataStorage.MailFrom = LoginEdit.Text;
            DataStorage.MailTo = RecipientEdit.Text;
            DataStorage.SmtpHost = SmtpHostEdit.Text;
            DataStorage.SmtpPort = Int32.Parse(PortEdit.Text);
            DataStorage.MsgSubject = SubjectEdit.Text;
            DataStorage.MsgBody = BodyEdit.Text;
            DataStorage.Login = LoginEdit.Text;
            NetworkCredential credential = new NetworkCredential
            {

                UserName = DataStorage.Login,
                SecurePassword = PasswordEdit.SecurePassword

            };

            EmailSendServiceClass mailSender = new EmailSendServiceClass(credential);
            //var from = new MailAddress(DataStorage.MailFrom, DataStorage.NameFrom);
            //var to = new MailAddress(DataStorage.MailTo);

            //dsfadf
            //dfsf
            //dvdf
            //var message = new MailMessage(from, to)
            //{
            //    Subject = DataStorage.MsgSubject + DateTime.Now, 
            //    Body = DataStorage.MsgBody + DateTime.Now
            //};


            //var client = new SmtpClient(DataStorage.SmtpHost, DataStorage.SmtpPort)
            //{
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential
            //    {
            //        UserName = DataStorage.Login, 
            //        SecurePassword = PasswordEdit.SecurePassword
            //    }
            //};



            //client.Send(message);
            
            mailSender.Send();
        }
    }
}
