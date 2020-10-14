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
    partial class MainWindowViewModel : ViewModel
    {

        
        private readonly IStore<Recipient> _RecipientsStore;
        private readonly IMailService _MailService;
        private readonly IServerStorage _ServerStorage;
        private readonly ISendersStorage _SendersStorage;
        private readonly IRecipientsStorage _RecipientsStorage;
        private readonly IMessagesStorage _MessagesStorage;
        //public MainWindowViewModel(
        //    IMailService MailService,
        //    IServerStorage ServerStorage, ISendersStorage SendersStorage,
        //    IRecipientsStorage RecipientsStorage, IMessagesStorage MessagesStorage)
        //{
        //    _MailService = MailService;
        //    _ServerStorage = ServerStorage;
        //    _SendersStorage = SendersStorage;
        //    _RecipientsStorage = RecipientsStorage;
        //    _MessagesStorage = MessagesStorage;
        //}

        public MainWindowViewModel(IMailService MailService, IStore<Recipient> RecipientsStore, IServerStorage ServerStorage, 
            ISendersStorage SendersStorage, IMessagesStorage MessagesStorage)
        {

            // Unit of Work
            _MailService = MailService;
            //_RecipientsStore = RecipientsStore;
            _ServerStorage = ServerStorage;
            _SendersStorage = SendersStorage;
            _MessagesStorage = MessagesStorage;
            //Servers = new ObservableCollection<Server>(Servers);
            //Senders = new ObservableCollection<Sender>(Senders);
            Recipients = new ObservableCollection<Recipient>(RecipientsStore.GetAll());
            //Messages = new ObservableCollection<Message>(Messages);

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

    }
}
