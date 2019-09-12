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
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }



        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitConfiguration();
            ConfigureServices();

            MainWindow = new MainWindow();
            MainWindow.Show();
        }


        private void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // EF
            serviceCollection.AddSingleton<Func<TweetReaderContext>>(
                provider => () => new TweetReaderContext(Configuration.GetConnectionString("db"))
            );

            // services
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
