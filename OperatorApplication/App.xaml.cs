using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OperatorApplication.Services;
using OperatorApplication.ViewModels;
using OperatorApplication.Views;
using Serilog;
using System.Configuration;
using System.Data;
using System.Printing;
using System.Windows;

namespace OperatorApplication
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ServiceProvider _serviceProvider;
        private void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IKafkaService, KafkaService>();
            services.AddSingleton<IRabbitMQService, RabbitMQService>();
            services.AddSingleton<INatsService, NATSService>();
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
            //mainWindow?.Show();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            //base.OnExit(e);
        }
    }
}
