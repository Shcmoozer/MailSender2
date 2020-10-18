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
            ??= new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute(object p) => true;

        private void OnLoadDataCommandExecuted(object p)
        {
            Servers = new ObservableCollection<Server>(_ServersStore.GetAll());
            Senders = new ObservableCollection<Sender>(_SendersStore.GetAll());
            Recipients = new ObservableCollection<Recipient>(_RecipientsStore.GetAll());
            Messages = new ObservableCollection<Message>(_MessagesStore.GetAll());
        }

        private ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand
            ??= new LambdaCommand(OnSaveDataCommandExecuted);

        private void OnSaveDataCommandExecuted(object p)
        {
            _ServersStore.Update(SelectedServer);
            _SendersStore.Update(SelectedSender);
            _RecipientsStore.Update(SelectedRecipient);
            _MessagesStore.Update(SelectedMessage);

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

            _ServersStore.Add(server);
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

        //private ICommand _DeleteServerCommand;
        //public ICommand DeleteServerCommand => _DeleteServerCommand
        //    ??= new LambdaCommand(OnDeleteServerCommandExecuted,
        //        CanDeleteServerCommandExecute);
        //private bool CanDeleteServerCommandExecute(object p) => p is Server;
        //private void OnDeleteServerCommandExecuted(object p)
        //{
        //    if (!(p is Server server)) return;
        //    _ServerStorage.Items.Remove(server);
        //    Servers.Remove(server);

        //}

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

        #region DeleteServerCommand

        private ICommand _DeleteServerCommand;

        public ICommand DeleteServerCommand => _DeleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);

        private bool CanDeleteServerCommandExecute(object p) => p is Server || SelectedServer != null;

        private void OnDeleteServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();

            //MessageBox.Show($"Удаление сервера {server.Address}!", "Управление серверами");
        }

        #endregion
    }
}
