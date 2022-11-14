using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Shapes;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int height;
        public int width;
        public int win;
        public int[,] board;
        public string winner;
        public string p1Sym;
        public string p2Sym;
        public Brush p1Color;
        public Brush p2Color;
        public string p1Name;
        public string p2Name;
        public bool turn;

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

                var temp = ScoreCheck();
                if (temp[0] != 0)
                {
                    Draw(temp);
                    winner = temp[0].ToString();
                    txtWin.Text = winner;
                }

                turn= !turn;
            }
        }

        private void Draw(int[] input)
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
            line.StrokeThickness = 20;
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
                    return new []{2, x+row,y, x+row + size-1, y};
               
                }
                if (sum==10*size)
                {
                    return new[] { 1, x+row, y, x + row + size - 1, y};
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
                    return new[] { 2, x, y+column, x, y + column + size-1 };

                }
                if (sum == 10*size)
                {
                    return new[] { 1, x, y + column, x, y + column + size-1 };
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
    }
}
