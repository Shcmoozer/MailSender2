using System;
using System.Collections.Generic;
using System.Text;
using MailSender;
using WpfMailSender;
using Microsoft.Extensions.DependencyInjection;


namespace WpfMailSender.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services
            .GetRequiredService<MainWindowViewModel>();
    }
}
