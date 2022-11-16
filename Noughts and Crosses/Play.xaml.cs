using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int height;
        private int width;
        private int win;
        private int[,] board;
        private string winner;
        private string p1Sym;
        private string p2Sym;
        private Brush p1Color;
        private Brush p2Color;
        private string p1Name;
        private string p2Name;
        private bool turn;

        public Page1 Home;

        public MainWindow(int height, int width, int win, string newP1, string newP2, Brush p1Color, Brush p2Color, string p1Name, string p2Name)
        {
            InitializeComponent();

            BoardSet(height,width);
            turn = true;
            board = new int[height, width];
            this.height = height;
            this.width = width;
            p1Sym = newP1;
            p2Sym = newP2;
            this.p1Color = p1Color;
            this.p2Color = p2Color;
            this.p1Name = p1Name;
            this.p2Name = p2Name;
            this.win = win;
            winner = "";
            txtTurn.Text = p1Sym;
            txtTurn.Foreground = p1Color;
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
                    button.Background = new SolidColorBrush(Colors.Transparent);
                    button.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    brdMain.BoardGrid.Children.Add(button);
                }
            }
        }
        private void Turn(object sender)
        {
            Button btn = (sender as Button)!;
            var row = Grid.GetRow(btn);
            var column = Grid.GetColumn(btn);
            if (board[row, column] == 0)
            {
                if (turn)
                {
                    board[row, column] = 10;
                    txtTurn.Text = p2Sym;
                    txtTurn.Foreground = p2Color;
                    btn.Content = p1Sym;
                    btn.Foreground = p1Color;
                }
                else
                {
                    board[row, column] = 1;
                    txtTurn.Text = p1Sym;
                    txtTurn.Foreground = p1Color;
                    btn.Content = p2Sym;
                    btn.Foreground = p2Color;
                }

                var result = ScoreCheck();
                if (result[0] != 0)
                {
                    Win(result);
                }
                else if (fillCheck())
                {
                    Draw();
                }

                turn= !turn;
            }
        }

        private bool fillCheck()
        {
            var full = true;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (board[j, i] == 0) full = false;
                }
            }
            return full;
        }

        private void Draw()
        {
            MessageBox.Show("It's a draw!", "Draw!", MessageBoxButton.OK);
            Hide();
            Home.Visibility = Visibility.Visible;
        }
        private void Win(int[] result)
        {
            Line(result);
            winner = result[0].ToString();

            if (winner == "1")
            {
                MessageBox.Show($"{p1Name} is the winner!", "Winner!", MessageBoxButton.OK);
                Home.P1Score++;
            }
            else
            {
                MessageBox.Show($"{p2Name} is the winner!", "Winner!", MessageBoxButton.OK);
                Home.P2Score++;
            }
            Hide();
            
            Home.Visibility = Visibility.Visible;
        }

        private void Line(int[] input)
        {
            var color = input[0] == 1 ? p1Color : p2Color;
            var boardWidth = brdMain.ActualWidth /  Convert.ToDouble(brdMain.BoardGrid.ColumnDefinitions.Count);
            var boardHeight = brdMain.ActualHeight / Convert.ToDouble(brdMain.BoardGrid.RowDefinitions.Count);

            var startX = (input[1]+0.5) * boardWidth;
            var startY = (input[2]+0.5) * boardHeight;
            var endX = (input[3] + 0.5) * boardWidth;
            var endY = (input[4] + 0.5) * boardHeight;

            var line = new Line();
            line.Stroke = color;
            line.X1 = startX;
            line.X2 = endX;
            line.Y1 = startY;
            line.Y2 = endY;
            line.StrokeThickness = 10;
            Grid.Children.Add(line);
        }

        private int[] ScoreCheck()
        {
            for (int _height = 0; _height < height-win+1; _height++)
            {
                for (int _width = 0; _width < width - win+1; _width++)
                {
                    int[] result = SubCheck(_height, _width, win);
                    if (result[0] != 0)
                    {
                        return result;
                    }
                }
            }
            return new[] {0};
        }
        private int[] SubCheck(int x, int y, int size)
        {
            int sum = 0;
            for (int row = 0; row < size; row++) // check rows
            {
                for (int column = 0; column < size; column++)
                {
                    sum += board[x+row, y+column];
                }

                if (sum == size)
                {
                    return new []{2, x,y + row, x + size-1, y + row };
               
                }
                if (sum==10*size)
                {
                    return new[] { 1, x, y + row, x + size - 1, y + row };
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

                if (sum == size)
                {
                    return new[] { 2, x + column, y, x + column, y + size-1 };

                }
                if (sum == 10*size)
                {
                    return new[] { 1, x + column, y, x + column, y+ size-1 };
                }
                sum = 0;
            }

            sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum+= board[x+i, y+i];
            }

            if (sum == size)
            {
                return new[] { 2, x, y, (x + size-1), (y + size-1) };
            }

            if (sum == 10*size)
            {
                return new[] { 1, x, y, (x + size-1), (y + size-1) };
            }

            sum = 0;
            int rev = size-1;
            for (int i = 0; i < size; i++)
            {
               sum+= board[x+i,y+rev];
               rev--;
            }

            return new []{0};
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
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var child in brdMain.BoardGrid.Children)
            {
                var btn = child as Button;
                try
                {
                    if (btn.ActualHeight > btn.ActualWidth)
                    {
                        btn.FontSize = btn.ActualWidth * 0.7;
                    }
                    else
                    {
                        btn.FontSize = btn.ActualHeight *0.7;
                    }
                }
                catch{}
            }
        }
    }
}
