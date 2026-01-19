
using AuxularyApp.Services;
using AuxularyApp.ViewModels;
using AuxularyApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using System;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
namespace AuxularyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ServiceProvider _serviceProvider;
        public App()
        {
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            //var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            // Отключаем DPI virtualization
            SetDPIAwareness();
        }
        private void ConfigureService(IServiceCollection services)
        {
            //services.AddHostedService<EquipmentBackgroundService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<MainWindowViewModel>();
            services.AddSingleton<IServiceProvider>(provider => provider);
            services.AddSingleton<MainWindow>();
        }

        private void SetDPIAwareness()
        {
            // Для Windows 10/11
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

}
