using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using WeatherMaker.Models;
using WeatherMaker.ViewModels;

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
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Image image)
            {

                //mainPage.Source = new BitmapImage(new Uri("C:\\Users\\41n\\source\\repos\\WeatherMaker\\Resources\\Images\\settings.png", UriKind.RelativeOrAbsolute));
            }

            NavigationService.Navigate(new MainPage());
            //MainFrame.Content = new MainPage();
        }

        private void GeopositionCB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Ваш код обработки события
            if (sender is ComboBox comboBox)
            {
                var selectedItem = comboBox.SelectedItem;
                if (selectedItem == null) {
                    return;
                }

                if (selectedItem is GeoInfo geoItem) {
                    AppSettings.SelectedCity = geoItem.Name;
                    AppSettings.GeonameId = geoItem.GeonameId.ToString();
                    AppSettings.Latitude = geoItem.Lat;
                    AppSettings.Longitude = geoItem.Lng;
                }
                
                // Дополнительные действия с выбранным элементом
            }
        }

        private void TemperatureCB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Ваш код обработки события
            if (sender is ComboBox comboBox)
            {
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
                var selectedItem = comboBox.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                AppSettings.Language = (string)selectedItem;
                // Дополнительные действия с выбранным элементом
            }
        }

        private void AutorunCBx_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
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
