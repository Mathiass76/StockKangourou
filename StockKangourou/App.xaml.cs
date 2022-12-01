using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StockKangourou
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("../../../Logs/LogKangourou.log")
                .CreateLogger();
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddDbContext<MyContext>();
            services.AddSingleton<IDataService, DataServiceEF>();
            services.AddLogging(builder => builder.AddSerilog(dispose: true));
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var MainWindow = _serviceProvider.GetService<MainWindow>();
            MainWindow.Show();
        }
    }
}
