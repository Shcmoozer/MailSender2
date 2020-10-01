using System;
using System.Collections.Generic;
using System.Text;
using WpfMailSender.ViewModels.Base;
using WpfMailSender.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;
using MailSender;
using Microsoft.Extensions.DependencyInjection;

namespace WpfMailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Рассыльщик почты";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
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
