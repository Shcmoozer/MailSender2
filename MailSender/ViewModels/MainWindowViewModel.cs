using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace WpfMailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
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
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Messages = new ObservableCollection<Message>(TestData.Messages);
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
