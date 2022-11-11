using static System.Convert;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for Colours.xaml
    /// </summary>
    public partial class Colours : UserControl
    {
        public System.Windows.Media.Brush Colour { get; set; }
        public Colours()
        {
            InitializeComponent();
            Colour = new SolidColorBrush(Colors.Red);
        }

        private void Value_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Colour = new SolidColorBrush(Color.FromRgb(ToByte(sldR.Value), ToByte(sldG.Value), ToByte(sldB.Value)));
        }
    }
}
