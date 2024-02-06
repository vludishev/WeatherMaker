using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WeatherMaker.Models;
using WeatherMaker.Models.Responses;
using WeatherMaker.ViewModels;
using WPFLocalizeExtension.Engine;

namespace WeatherMaker
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {

        public SettingsPage()
        {
            InitializeComponent();

            DataContext = new SettingsVM();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            ReturnImg.Source = new BitmapImage(new Uri(Path.Combine(basePath, @"resources\images\Return.png")));
            LanguageCB.FontSize = 15;
            TemperatureUnitCB.FontSize = 15;
            GeopositionCB.FontSize = 15;

            LanguageCB.FontFamily = new FontFamily("Cascadia Code SemiLight");
            TemperatureUnitCB.FontFamily = new FontFamily("Cascadia Code SemiLight");
            GeopositionCB.FontFamily = new FontFamily("Cascadia Code SemiLight");
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void GeopositionCB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Ваш код обработки события
            if (sender is ComboBox comboBox)
            {
                if (!comboBox.IsLoaded)
                    return;

                var selectedItem = comboBox.SelectedItem;
                if (selectedItem == null) {
                    return;
                }

                if (selectedItem is GeoInfo geoItem) {
                    AppSettings.GeonameId = geoItem.GeonameId.ToString();
                    AppSettings.Latitude = geoItem.Latitude;
                    AppSettings.Longitude = geoItem.Longitude;
                }
                
                // Дополнительные действия с выбранным элементом
            }
        }

        private void TemperatureCB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Ваш код обработки события
            if (sender is ComboBox comboBox)
            {
                if (!comboBox.IsLoaded)
                    return;

                var selectedItem = comboBox.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }
                if (selectedItem is TemperatureUnitInfo temperatureItem)
                {
                    AppSettings.TemperatureUnit = temperatureItem.Value.ToString();
                }
                // Дополнительные действия с выбранным элементом
            }
        }

        private void LanguageCB_SelectionChanged(object sender, RoutedEventArgs e)
        {

            // Ваш код обработки события
            if (sender is ComboBox comboBox)
            {
                if (!comboBox.IsLoaded)
                    return;

                var selectedItem = comboBox.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                if (selectedItem is LanguageModel languageItem)
                {
                    AppSettings.Language = languageItem.Value;

                    CultureInfo cultureInfo = new(languageItem.Value);

                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    LocalizeDictionary.Instance.Culture = cultureInfo;

                    //TODO: ПЕРЕРИСОВАТЬ контент контролов или обновить модель 
                    NavigationService.Navigate(new MainPage());
                }
            }
        }

        private void AutorunCBx_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (!checkBox.IsLoaded)
                    return;

                AppSettings.Autorun = checkBox.IsChecked.GetValueOrDefault().ToString();

                if (checkBox.IsChecked == true)
                {
                    // Получаем путь к исполняемому файлу вашего .exe приложения
                    string exePath = Process.GetCurrentProcess().MainModule.FileName;

                    // Создаем запись в реестре для автозагрузки
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key.SetValue("WeatherMaker", exePath);
                }
            }
        }
    }
}
