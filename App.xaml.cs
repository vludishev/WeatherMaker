using System.Globalization;
using System.Windows;
using WeatherMaker.Models;
using WPFLocalizeExtension.Engine;

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

            CultureInfo cultureInfo = new(AppSettings.Language ?? Language.English);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            LocalizeDictionary.Instance.Culture = cultureInfo;
        }
    }
}
