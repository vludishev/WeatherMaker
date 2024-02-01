using System.Windows;
using System.Windows.Controls;
using WeatherMaker.ViewModels;

namespace WeatherMaker
{
    /// <summary>
    /// Interaction logic for DowTile.xaml
    /// </summary>
    public partial class DowTile : UserControl
    {
        public static readonly DependencyProperty MiddleFontSizeProperty =
            DependencyProperty.Register("MiddleFontSize", typeof(double), typeof(DowTile));

        public static readonly DependencyProperty BottomFontSizeProperty =
            DependencyProperty.Register("BottomFontSize", typeof(double), typeof(DowTile));

        public double MiddleFontSize
        {
            get { return (double)GetValue(MiddleFontSizeProperty); }
            set { SetValue(MiddleFontSizeProperty, value); }
        }

        public double BottomFontSize
        {
            get { return (double)GetValue(BottomFontSizeProperty); }
            set { SetValue(BottomFontSizeProperty, value); }
        }

        public DowTileVM ViewModel
        {
            get { return (DowTileVM)DataContext; }
            set { DataContext = value; }
        }

        public DowTile()
        {
            InitializeComponent();

            DataContext = new DowTileVM();
        }
    }
}
