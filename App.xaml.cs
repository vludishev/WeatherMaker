using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System.Globalization;
using System.Reflection;
using System.Windows;
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

            CultureInfo cultureInfo = new CultureInfo("en-US");

            if (AppSettings.Language == "Русский")
            {
                cultureInfo = new CultureInfo("ru"); // для русского языка
            }
            else if (AppSettings.Language == "English")
            {
                cultureInfo = new CultureInfo("en-US");
            }

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            LocalizeDictionary.Instance.Culture = cultureInfo;
        }
    }
}
