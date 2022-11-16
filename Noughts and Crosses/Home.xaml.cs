using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private int p1Score;

        public int P1Score
        {
            get => p1Score;
            set
            {
                txtP1Score.Text = $"Score: {value}";
                p1Score = value;
            }
        }

        private int p2Score;
        public int P2Score
        {
            get => p2Score;
            set
            {
                txtP2Score.Text = $"Score: {value}";
                p2Score = value;
            }
        }

        public Page1()
        {
            InitializeComponent();
            clrP1.sldB.Value = 255;
            clrP2.sldR.Value = 255;
            symbP1.btn1.Foreground = clrP1.Colour;
            txtP1Score.Foreground = clrP1.Colour;
            symbP2.btn1.Foreground = clrP2.Colour;
            txtP2Score.Foreground = clrP2.Colour;
            symbP1.btn1.GroupName = "p1";
            symbP1.btn1.GroupName = "p2";
        }

        private void clrP1_MouseMove(object sender, MouseEventArgs e)
        {
            txtP1Score.Foreground = txtP1Display.Foreground = symbP1.btn1.Foreground = txtP1Name.Foreground = 
                    clrP1.Colour;
        }

        private void clrP2_MouseMove(object sender, MouseEventArgs e)
        {
            txtP2Score.Foreground = txtP2Display.Foreground = symbP2.btn1.Foreground = txtP2Name.Foreground =
                clrP2.Colour;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            var frm1 = new MainWindow(ToInt32(txtHeight.Text),ToInt32(txtWidth.Text), ToInt32(txtWin.Text),
                txtP1Display.Text, txtP2Display.Text, txtP1Display.Foreground, txtP2Display.Foreground,
                txtP1Name.Text, txtP2Name.Text);
            frm1.Home = this;
            frm1.Show();
        }

        private void symbP1_MouseMove(object sender, MouseEventArgs e)
        {
            txtP1Display.Text = symbP1.Symbol;
        }

        private void symbP2_MouseMove(object sender, MouseEventArgs e)
        {
            txtP2Display.Text = symbP2.Symbol;
        }
    }
}
