using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MailSender.Interfaces;
using MailSender.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfMailSender.ViewModels;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private static IHost __Hosting;
        public static IHost Hosting
        {
            get
            {
                if (__Hosting != null) return __Hosting;
                var host_builder = Host
                    .CreateDefaultBuilder(Environment.GetCommandLineArgs());
                host_builder.ConfigureServices(ConfigureServices);
                return __Hosting = host_builder.Build();
            }
        }

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(
            HostBuilderContext host,
            IServiceCollection services)
        {
            // Здесь нам надо добавить все сервисы нашего приложения
            // в коллекцию services
            // В переменной host хранится информация, на пример,
            // о пути запуска нашего приложения
            services.AddSingleton<MainWindowViewModel>();
            //services.AddTransient<IMailService, SmtpMailService>();
#if DEBUG
            services.AddTransient<IMailService, SmtpMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            // Выбираем либо этот блок
            var memory_store = new DataStorageInMemory();
            services.AddSingleton<IServerStorage>(memory_store);
            services.AddSingleton<ISendersStorage>(memory_store);
            services.AddSingleton<IRecipientsStorage>(memory_store);
            services.AddSingleton<IMessagesStorage>(memory_store);
            //либо этот. Один надо закомментировать, другой - раскомментировать
            //const string data_file_name = "MailSenderStorage.xml";
            //var file_storage = new DataStorageInXmlFile(data_file_name);
            //services.AddSingleton<IServerStorage>(file_storage);
            //services.AddSingleton<ISendersStorage>(file_storage);
            //services.AddSingleton<IRecipientsStorage>(file_storage);
            //services.AddSingleton<IMessagesStorage>(file_storage);

            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();
        }

    }

    //interface IDialogService
    //{
    //    void ShowInfo(string msg);
    //}

    //class WindowDialog : IDialogService
    //{
    //    public void ShowInfo(string msg) => MessageBox.Show(msg);
    //}



}
