using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WeatherMaker.ViewModels;

namespace WeatherMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point startPoint;
        private readonly Brush myBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#251A54"));
        private bool isMouseCaptured;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainVM();

            this.MouseDown += MainWindow_MouseDown;
            this.MouseMove += MainWindow_MouseMove;
            this.MouseUp += MainWindow_MouseUp;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                startPoint = e.GetPosition(this);
            }
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(this);
                double offsetX = currentPoint.X - startPoint.X;
                double offsetY = currentPoint.Y - startPoint.Y;

                this.Left += offsetX;
                this.Top += offsetY;
            }
        }

        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = false;
            }
        }

        private void HourlyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                HourlyModeBtnText.Foreground = myBlue;

                DailyModeBtn.Background = myBlue;
                DailyModeBtnText.Foreground = Brushes.White;
            }
        }

        private void DailyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                DailyModeBtnText.Foreground = myBlue;

                HourlyModeBtn.Background = myBlue;
                HourlyModeBtnText.Foreground = Brushes.White;
            }
        }

        private T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default;

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);

                if (child != null)
                {
                    break;
                }
            }

            return child;
        }
    }
}