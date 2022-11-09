using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Data data = new();
        public int height { get; set; }
        public int width { get; set; }
        public int win { get; set; }
        public int[,] board { get; set; }
        public string winner { get; set; } = "sd";
        public string p1 { get; set; }
        public string p2 { get; set; }
        public bool turn { get; set; }
        public MainWindow(int height, int width, string newP1, string newP2)
        {
            InitializeComponent();

            BoardSet(height,width);
            turn = true;
            board = new int[height, width];
            p1 = newP1;
            p2 = newP2;
            data.Winner = "asdas";
        }

        private void BoardSet(int _height, int _width)
        {
            for (int i = 0; i < _height; i++)
            {
                brdMain.BoardGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _width; i++)
            {
                brdMain.BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    Button button = new Button();
                    button.Name = "btn_" + row + "_" + column;
                    button.Click += Btn_Click;
                    button.Background = new SolidColorBrush(new Color());
                    button.BorderBrush = new SolidColorBrush(new Color());

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    brdMain.BoardGrid.Children.Add(button);

                    var bnd = new Binding("winner")
                    {
                        Source = data,
                        Mode = BindingMode.TwoWay
                    };
                }
            }
        }
        private void Turn(object obj)
        {
            Button btn = (obj as Button)!;
            var row = Grid.GetRow(btn);
            var column = Grid.GetColumn(btn);
            if (board[row, column] == 0)
            {
                if (turn)
                {
                    board[row, column] = 10;
                    txtTurn.Text = p2;
                    btn.Content = p1;
                }
                else
                {
                    board[row, column] = 1;
                    txtTurn.Text = p1;
                    btn.Content = p2;
                }

                data.Winner = ScoreCheck();
                UpdateWinner();
                turn= !turn;
            }
        }

        private void UpdateWinner()
        {
            txtTurn.GetBindingExpression(Data.WinnerProperty).UpdateSource();
            txtTurn.GetBindingExpression(Data.WinnerProperty).UpdateTarget();
        }

        private string ScoreCheck()
        {
            for (int _height = 0; _height < height-win+1; _height++)
            {
                for (int _width = 0; _width < width - win+1; _width++)
                {
                    string result = SubCheck(_height, _width, win);
                    if (result != "")
                    {
                        return result;
                    }
                }
            }
            return "";
        }
        private string SubCheck(int x, int y, int size)
        {
            int sum = 0;
            for (int row = 0; row < size; row++) // check rows
            {
                for (int column = 0; column < size; column++)
                {
                    sum += board[x+row, y+column];
                }

                if (sum == 3)
                {
                    return "nought";
               
                }
                if (sum==30)
                {
                    return "cross";
                }
                sum = 0;
            }

            sum = 0;
            for (int column = 0; column < size; column++) // check columns
            {
                for (int row = 0; row < 3; row++)
                {
                    sum += board[x+row, y+column];
                }

                if (sum == 3)
                {
                    return "nought";
                    
                }
                if (sum == 30)
                {
                    return "cross";
                    
                }
                sum = 0;
            }

            sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum+= board[x+i, y+i];
            }

            if (sum == 3)
            {
                return "nought";
            }

            if (sum == 30)
            {
                return "cross";
            }

            sum = 0;
            int rev = size-1;
            for (int i = 0; i < size; i++)
            {
               sum+= board[x+i,y+rev];
               rev--;
            }

            return "";
        }

        public ImageBrush ToBrush(string input)
        {
            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/" + input + ".png")));
        }

        public ImageSource ToSource(string input)
        {
            return new BitmapImage(new Uri("pack://application:,,,/Resources/" + input + ".png"));
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Turn(sender);
            data.Winner = "df";
            UpdateWinner();
        }
    }
}
