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
using System.Windows.Shapes;
using static System.Convert;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class Config : Window
    {
        public Page1 home;
        public Config()
        {
            InitializeComponent();
        }

        private void btnCustom1_Click(object sender, RoutedEventArgs e)
        {
            var custom = new Custom();
            custom.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            home.width = ToInt32(sldWidth.Value);
            home.height = ToInt32(sldHeight.Value);
            home.win = ToInt32(sldWin.Value);
            home.time = ToInt32(sldTime.Value);
            home.mode = drpModes.Text;
            home.Music = drpMusic.SelectedIndex;
            home.Back = drpBack.SelectedIndex;
            home.Visibility = Visibility.Visible;
        }
    }
}
