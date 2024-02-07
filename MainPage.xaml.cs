using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
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

        SettingsPageVM settingsPageVM = new SettingsPageVM();

        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainPageVM();

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
            Window.GetWindow(this).WindowState = WindowState.Minimized;        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            var optionsPage = new SettingsPage(settingsPageVM);
            NavigationService.Navigate(optionsPage);
        }

        private static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                    return (T)child;

                T childItem = FindVisualChild<T>(child);
                if (childItem != null) return childItem;
            }
            return null;
        }

        private void DowTilesLB_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(DowTilesLB);

            if (e.Delta > 0)
                scrollViewer.LineLeft(); // Используем LineLeft() для прокрутки влево
            else if (e.Delta < 0)
                scrollViewer.LineRight(); // Используем LineRight() для прокрутки вправо

            e.Handled = true;
        }

        private void ScrollBar_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var scrollBar = sender as ScrollBar;
            if (scrollBar == null) return;

            double newValue = 0;

            if (scrollBar.Orientation == Orientation.Horizontal)
            {
                if (e.GetPosition(scrollBar).X < scrollBar.ActualWidth / 2)
                {
                    // Прокрутка влево
                    newValue = Math.Max(scrollBar.Value - scrollBar.LargeChange, scrollBar.Minimum);
                }
                else
                {
                    // Прокрутка вправо
                    newValue = Math.Min(scrollBar.Value + scrollBar.LargeChange, scrollBar.Maximum);
                }
                scrollBar.Value = newValue;
            }
            else
            {
                if (e.GetPosition(scrollBar).Y < scrollBar.ActualHeight / 2)
                {
                    // Прокрутка вверх
                    newValue = Math.Max(scrollBar.Value - scrollBar.LargeChange, scrollBar.Minimum);
                }
                else
                {
                    // Прокрутка вниз
                    newValue = Math.Min(scrollBar.Value + scrollBar.LargeChange, scrollBar.Maximum);
                }
                scrollBar.Value = newValue;
            }

            e.Handled = true;
        }

        private void RightScrollBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DowTilesLB.SelectedIndex + 4 < DowTilesLB.Items.Count)
            {
                // Устанавливаем индекс выбранного элемента на значение, которое находится на 4 позиции вперёд
                DowTilesLB.SelectedIndex += 4;
                // Прокручиваем ListBox к выбранному элементу
                DowTilesLB.ScrollIntoView(DowTilesLB.SelectedItem);
            }
            else
            {
                DowTilesLB.SelectedIndex = DowTilesLB.Items.Count - 1;
                // Прокручиваем ListBox к выбранному элементу
                DowTilesLB.ScrollIntoView(DowTilesLB.SelectedItem);     
            }
        }

        private void LeftScrollBtn_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, есть ли хотя бы 4 элемента назад от текущего выбранного элемента
            if (DowTilesLB.SelectedIndex - 4 >= 0)
            {
                // Устанавливаем индекс выбранного элемента на значение, которое находится на 4 позиции назад
                DowTilesLB.SelectedIndex -= 4;
                // Прокручиваем ListBox к выбранному элементу
                DowTilesLB.ScrollIntoView(DowTilesLB.SelectedItem);
            } else
            {
                DowTilesLB.SelectedIndex = 0;
                // Прокручиваем ListBox к выбранному элементу
                DowTilesLB.ScrollIntoView(DowTilesLB.SelectedItem);
            }
        }
    }
}
 