using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
using static System.Convert;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Window
    {
        public Page1()
        {
            InitializeComponent();
            clrP1.sldB.Value = 255;
            clrP2.sldR.Value = 255;
        }

        private void clrP1_MouseMove(object sender, MouseEventArgs e)
        {
            txtP1Display.Foreground = new SolidColorBrush(Color.FromRgb(ToByte(clrP1.sldR.Value),
                ToByte(clrP1.sldG.Value), ToByte(clrP1.sldB.Value)));
        }

        private void clrP2_MouseMove(object sender, MouseEventArgs e)
        {
            txtP2Display.Foreground = new SolidColorBrush(Color.FromRgb(ToByte(clrP2.sldR.Value),
                ToByte(clrP2.sldG.Value), ToByte(clrP2.sldB.Value)));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            var frm1 = new MainWindow(3,3, "╳", "◯");
            frm1.Show();
        }

    }


    /*
    public class SizeToFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return value / 1.0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dbl = value as double?;
            return dbl * 2;
        }
    }
    */
}
