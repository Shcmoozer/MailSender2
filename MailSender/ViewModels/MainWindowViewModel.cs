using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using WpfMailSender.ViewModels.Base;
using WpfMailSender.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;
using MailSender;
using Microsoft.Extensions.DependencyInjection;
using WpfMailSender.Data;
using WpfMailSender.Models;
using System.Linq;
using MailSender.Interfaces;
using WpfMailSender.Services;


namespace WpfMailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IMailService _MailService;
        private readonly IServerStorage _ServerStorage;
        private readonly ISendersStorage _SendersStorage;
        private readonly IRecipientsStorage _RecipientsStorage;
        private readonly IMessagesStorage _MessagesStorage;
        public MainWindowViewModel(
            IMailService MailService,
            IServerStorage ServerStorage, ISendersStorage SendersStorage,
            IRecipientsStorage RecipientsStorage, IMessagesStorage MessagesStorage)
        {
            _MailService = MailService;
            _ServerStorage = ServerStorage;
            _SendersStorage = SendersStorage;
            _RecipientsStorage = RecipientsStorage;
            _MessagesStorage = MessagesStorage;
        }



        public StatisticViewModel Statistic { get; } = new StatisticViewModel();

        private string _Title = "Рассыльщик почты";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private ObservableCollection<Server> _Servers;

        public ObservableCollection<Server> Servers
        {
            get => _Servers;
            set => Set(ref _Servers, value);
        }

        private ObservableCollection<Sender> _Senders;
        public ObservableCollection<Sender> Senders
        {
            get => _Senders;
            set => Set(ref _Senders, value);
        }
        private ObservableCollection<Recipient> _Recipients;
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }
        private ObservableCollection<Message> _Messages;
        public ObservableCollection<Message> Messages
        {
            get => _Messages;
            set => Set(ref _Messages, value);
        }

        private Server _SelectedServer;

        public Server SelectedServer
        {
            get => _SelectedServer;
            set => Set(ref _SelectedServer, value);
        }

        private Sender _SelectedSender;

        public Sender SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }

        private Recipient _SelectedRecipient;

        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        private Message _SelectedMessage;

        public Message SelectedMessage
        {
            get => _SelectedMessage;
            set => Set(ref _SelectedMessage, value);
        }


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
        //22
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

        //далее тестовый код
        //private ICommand _ShowDialogCommand;
        //public ICommand ShowDialogCommand => _ShowDialogCommand
        //    ??= new LambdaCommand(OnShowDialogCommandExecuted);
        //private void OnShowDialogCommandExecuted(object p)
        //{
        //    var message = p as string ?? "Hello World!";
        //    App.ServicesTest
        //        .GetService<IDialogService>()
        //        .ShowInfo(message);
        //    //MessageBox.Show(message, "Сообщение от первой команды");
        //}
    }
}
