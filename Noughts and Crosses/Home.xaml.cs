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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            var frm1 = new MainWindow(Convert.ToInt32(txtAmmount.Text));
            frm1.Show();
        }
    }
}
