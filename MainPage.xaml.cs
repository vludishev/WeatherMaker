using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeatherMaker.ViewModels;

namespace WeatherMaker
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly Brush myBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#251A54"));

        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainVM();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            OptionsImg.Source = new BitmapImage(new Uri(Path.Combine(basePath, @"resources\images\Settings.png")));
            GeoImg.Source = new BitmapImage(new Uri(Path.Combine(basePath, @"resources\images\Location.png")));
            MinimizeWindowImg.Source = new BitmapImage(new Uri(Path.Combine(basePath, @"resources\images\MinimizeWindow.png")));
            CloseWindowImg.Source = new BitmapImage(new Uri(Path.Combine(basePath, @"resources\images\CloseWIndow.png")));
        }

        private void HourlyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                HourlyModeTB.Foreground = myBlue;

                DailyModeBtn.Background = myBlue;
                DailyModeTB.Foreground = Brushes.White;
            }
        }

        private void DailyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                DailyModeTB.Foreground = myBlue;

                HourlyModeBtn.Background = myBlue;
                HourlyModeTB.Foreground = Brushes.White;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
            //WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            //Close();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            var optionsPage = new SettingsPage();
            NavigationService.Navigate(optionsPage);
        }
    }
}
 