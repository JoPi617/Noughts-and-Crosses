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
using static System.Convert;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private int size { get; set; }

        public UserControl1()
        {
            InitializeComponent();
        }

        public ImageBrush ToBrush(string input)
        {
            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/" + input + ".png")));
        }

        public ImageSource ToSource(string input)
        {
            return new BitmapImage(new Uri("pack://application:,,,/Resources/" + input + ".png"));
        }

        private bool isNought;
        private int[,] board = new int[3, 3];

        private void Turn(int collumn, int row, object obj)
        {
            if (board[row, collumn] == 0)
            {
                Button button = (obj as Button)!;
                if (isNought)
                {
                    board[row, collumn] = 10;
                    //imgTurn.Source = ToSource("Cross");
                    button.Background = ToBrush("Nought");
                }
                else
                {
                    board[row, collumn] = 1;
                   // imgTurn.Source = ToSource("Nought");
                    button.Background = ToBrush("Cross");
                }

                //lblWin.Content = Scorecheck();

                isNought = !isNought;
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var array = button!.Name.Split('_');
            Turn(ToInt32(array[2]), ToInt32(array[1]),sender);
        }
    }
}
