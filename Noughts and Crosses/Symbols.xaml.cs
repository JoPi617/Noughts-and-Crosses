using System;
using System.Collections.Generic;
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

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for Symbols.xaml
    /// </summary>
    public partial class Symbols : UserControl
    {
        public string Symbol { get; set; }
        public string Group { get; set; }
        public Symbols()
        {
            InitializeComponent();
        }

        private void btn_Checked(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            Symbol = btn?.Content.ToString()!;
        }
    }
}
