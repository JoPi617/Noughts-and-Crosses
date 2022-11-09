using static System.Convert;
using System.Windows;
using System.Windows.Controls;

using System.Drawing;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for Colours.xaml
    /// </summary>
    public partial class Colours : UserControl
    {
        private Color colour { get; set; }
        public Colours()
        {
            InitializeComponent();
        }

        private void Value_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            colour = Color.FromArgb(ToInt32(sldR.Value), ToInt32(sldG.Value), ToInt32(sldB.Value));
        }
    }
}
