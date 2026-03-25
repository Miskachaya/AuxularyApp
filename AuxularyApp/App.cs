using AuxularyApp.Services;
using AuxularyApp.ViewModels;
using AuxularyApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuxularyApp
{
    public class App : Application
    {
        public ServiceProvider _serviceProvider;
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEquipmentService,EquipmentService>();
            // Регистрируем ViewModels
            services.AddTransient<MainWindowViewModel>();
            services.AddSingleton<IServiceProvider>(provider => provider);

            // Регистрируем Views
            services.AddTransient<MainWindow>();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {

            //base.OnStartup(e);
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow?.Show();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            //base.OnExit(e);
        }
    }
}
