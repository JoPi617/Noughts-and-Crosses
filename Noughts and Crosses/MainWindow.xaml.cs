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
using System.Media;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Media.Animation;
using static Noughts_and_Crosses.Resources;
using static System.Net.Mime.MediaTypeNames;


namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isNought;
        private int[,] board = new int[3, 3];

        private void Turn(int collumn, int row)
        {

        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTL_Click(object sender, RoutedEventArgs e)
        {
            Turn(0,0);

            if (isNought)
            {
                btnTL.Background = new ImageBrush(new BitmapImage(new Uri("/Resources/Nought.png", UriKind.Relative)));
            }
            else
            {
                btnTL.Background = new ImageBrush(new BitmapImage(new Uri()));
            }

            //btnTL.IsEnabled = false;

        }

        private void btnBR_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
