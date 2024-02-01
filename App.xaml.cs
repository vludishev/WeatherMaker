using Microsoft.Extensions.Configuration;
using System.Windows;

namespace WeatherMaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем ConfigurationBuilder и загружаем конфигурацию из файла
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Добавляем конфигурацию в ресурсы приложения
            Current.Resources.Add("Configuration", configuration);

            // Здесь можно запустить ваше WPF окно или другие действия...
        }
    }
}
