using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MailSender.Interfaces;
using MailSender.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WpfMailSender.ViewModels;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
                .ConfigureHostConfiguration(cfg => cfg
                    .AddJsonFile("appconfig.json", true, true)
                    .AddXmlFile("appsettings.xml", true, true)
                )
                .ConfigureAppConfiguration(cfg => cfg
                    .AddJsonFile("appconfig.json", true, true)
                    .AddXmlFile("appsettings.xml", true, true)
                )
                .ConfigureLogging(log => log
                    .AddConsole()
                    .AddDebug()
                )
                .ConfigureServices(ConfigureServices)
                .Build();

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
