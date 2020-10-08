using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MailSender;
using WpfMailSender.Infrastructure.Commands;
using WpfMailSender.Models;

namespace WpfMailSender.ViewModels
{
    partial class MainWindowViewModel
    {
        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommand(OnLoadDataCommandExecuted);
        private void OnLoadDataCommandExecuted(object p)
        {
            _ServerStorage.Load();
            _RecipientsStorage.Load();
            _ServerStorage.Load();
            _MessagesStorage.Load();

            Servers = new ObservableCollection<Server>(_ServerStorage.Items);
            Senders = new ObservableCollection<Sender>(_SendersStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_RecipientsStorage.Items);
            Messages = new ObservableCollection<Message>(_MessagesStorage.Items);
            //var data = File.Exists("")
            //    ? TestData.LoadFromXML("")
            //    : new TestData();
            //Servers = new ObservableCollection<Server>(data.Servers);
            //Senders = new ObservableCollection<Sender>(data.Senders);
            //Recipients = new ObservableCollection<Recipient>(data.Recipients);
            //Messages = new ObservableCollection<Message>(data.Messages);
        }

        private ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand
            ??= new LambdaCommand(OnSaveDataCommandExecuted);

        private void OnSaveDataCommandExecuted(object p)
        {
            _ServerStorage.SaveChanges();
            _SendersStorage.SaveChanges();
            _RecipientsStorage.SaveChanges();
            _MessagesStorage.SaveChanges();

            //var data = new TestData
            //{
            //    Servers = Servers,
            //    Senders = Senders,
            //    Recipients = Recipients,
            //    Messages = Messages
            //};
            //data.SaveToXML("");
        }

        private ICommand _CreateServerCommand;
        public ICommand CreateServerCommand => _CreateServerCommand
            ??= new LambdaCommand(OnCreateServerCommandExecuted);

        private void OnCreateServerCommandExecuted(object p)
        {
            if (!ServerEditDialog.Create(
                out var name,
                out var address,
                out var port,
                out var ssl,
                out var description,
                out var login,
                out var password))
                return;
            var server = new Server
            {
                Id = Servers.DefaultIfEmpty().Max(s => s?.Id ?? 0) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };

            _ServerStorage.Items.Add(server);
            Servers.Add(server);
        }

        private ICommand _EditServerCommand;
        public ICommand EditServerCommand => _EditServerCommand
            ??= new LambdaCommand(OnEditServerCommandExecuted,
                CanEditServerCommandExecute);
        private bool CanEditServerCommandExecute(object p) => p is Server;
        private void OnEditServerCommandExecuted(object p)
        {
            if (!(p is Server server)) return;
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;
            if (!ServerEditDialog.ShowDialog("Редактирование сервера",
                ref name,
                ref address, ref port, ref ssl,
                ref description,
                ref login, ref password))
                return;
            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Description = description;
            server.Login = login;
            server.Password = password;
        }

        private ICommand _DeleteServerCommand;
        public ICommand DeleteServerCommand => _DeleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted,
                CanDeleteServerCommandExecute);
        private bool CanDeleteServerCommandExecute(object p) => p is Server;
        private void OnDeleteServerCommandExecuted(object p)
        {
            if (!(p is Server server)) return;
            _ServerStorage.Items.Remove(server);
            Servers.Remove(server);

        }

        private ICommand _SendMailMessageCommand;
        public ICommand SendMailMessageCommand => _SendMailMessageCommand
            ??= new LambdaCommand(
                OnSendMailMessageCommandExecuted,
                CanSendMailMessageCommandExecute);
        private bool CanSendMailMessageCommandExecute(object p)
        {
            return SelectedServer != null
                   && SelectedSender != null
                   && SelectedRecipient != null
                   && SelectedMessage != null;
        }
        private void OnSendMailMessageCommandExecuted(object p)
        {
            var server = SelectedServer;
            var client = _MailService.GetSender(
                server.Address, server.Port, server.UseSSL,
                server.Login, server.Password);
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;
            client.Send(
                sender.Address, recipient.Address,
                message.Tittle, message.Body);
        }
    }
}
