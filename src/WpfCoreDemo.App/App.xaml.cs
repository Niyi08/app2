using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfCoreDemo.Data;
using WpfCoreDemo.Data.Services;

namespace WpfCoreDemo.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Window wnd;

        public IServiceProvider ServiceProvider { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }



        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitConfiguration();
            ConfigureServices();

            // BUG: https://github.com/dotnet/wpf/issues/668
            // first WPF window in preview05 shows with 100% DPI
            wnd = new Window
            {
                Height = 0,
                ShowInTaskbar = false,
                Width = 0,
                WindowStyle = WindowStyle.None
            };
            wnd.Show();
            wnd.Hide();

            MainWindow = new MainWindow();
            MainWindow.Show();

            wnd.Close();
        }


        private void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // EF
            serviceCollection.AddSingleton<Func<TweetReaderContext>>(
                provider => () => new TweetReaderContext(Configuration.GetConnectionString("db"))
            );

            // services
            serviceCollection.AddSingleton<ExportService>();
            serviceCollection.AddSingleton<TweetService>();
            Tweetinvi.Auth.SetUserCredentials(
                Configuration.GetSection("Twitter")["ConsumerKey"],
                Configuration.GetSection("Twitter")["ConsumerSecret"],
                Configuration.GetSection("Twitter")["AccessToken"],
                Configuration.GetSection("Twitter")["AccessSecret"]
            );

            // viewmodels
            serviceCollection.AddSingleton<MainViewModel>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void InitConfiguration()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            builder.AddUserSecrets("7db4c047-7049-4f92-8254-2a275a0c8f28");
            Configuration = builder.Build();
        }
    }
}
