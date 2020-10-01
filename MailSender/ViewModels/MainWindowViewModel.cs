using System;
using System.Collections.Generic;
using System.Text;
using WpfMailSender.ViewModels.Base;
using WpfMailSender.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;

namespace WpfMailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Главное окно программы";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private ICommand _ShowDialogCommand;
        public ICommand ShowDialogCommand => _ShowDialogCommand
            ??= new LambdaCommand(OnShowDialogCommandExecuted);
        private void OnShowDialogCommandExecuted(object p)
        {
            var message = p as string ?? "Hello World!";
            MessageBox.Show(message, "Сообщение от первой команды");
        }
    }
}
