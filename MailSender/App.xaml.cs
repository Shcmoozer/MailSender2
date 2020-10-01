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
        }


        //из тестового приложения
        //private static IServiceProvider _Services;
        //public static IServiceProvider ServicesTest => _Services
        //    ??= GetServices().BuildServiceProvider();
        //private static IServiceCollection GetServices()
        //{
        //    var services = new ServiceCollection();
        //    InitializeServices(services);
        //    return services;
        //}
        //private static void InitializeServices(IServiceCollection services)
        //{
        //    services.AddTransient<IDialogService, WindowDialog>();
        //    //services.AddScoped<IDialogService, WindowDialog>();
        //    //services.AddSingleton<IDialogService, WindowDialog>();
        //}
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
