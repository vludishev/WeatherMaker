using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using XPlat.Device.Geolocation;

namespace WeatherMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point startPoint;
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Content = new MainPage();

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
            if (isDragging && e.LeftButton == MouseButtonState.Pressed)
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
    }
}