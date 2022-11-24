using System;
using System.ComponentModel;
using System.Data.Common;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Noughts_and_Crosses; //draw goes at inverted x,y



/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Page1 Home = null!;

    private readonly int[,] _board;
    private readonly int _height;
    private readonly Brush _p1Color;
    private readonly string _p1Name;
    private readonly string _p1Sym;
    private readonly Brush _p2Color;
    private readonly string _p2Name;
    private readonly string _p2Sym;
    private bool _turn;
    private readonly int _width;
    private readonly int _win;
    private string _winner;
    private readonly bool _isComp;


    public MainWindow(int height, int width, int win, string newP1, string newP2, Brush p1Color, Brush p2Color,
        string p1Name, string p2Name, bool isComp)
    {
        InitializeComponent();

        BoardSet(height, width);
        _turn = true;
        _board = new int[height, width];
        _height = height;
        _width = width;
        _p1Sym = newP1;
        _p2Sym = newP2;
        _p1Color = p1Color;
        _p2Color = p2Color;
        _p1Name = p1Name;
        _p2Name = p2Name;
        _win = win;
        _winner = "";
        txtTurn.Text = _p1Sym;
        txtTurn.Foreground = p1Color;
        _isComp = isComp;
    }

    private void BoardSet(int height, int width)
    {
        for (var i = 0; i < height; i++) brdMain.BoardGrid.RowDefinitions.Add(new RowDefinition());

        for (var i = 0; i < width; i++) brdMain.BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());

        for (var row = 0; row < height; row++)
            for (var column = 0; column < width; column++)
            {
                var button = new Button
                {
                    Name = "btn_" + row + "_" + column,
                    Background = new SolidColorBrush(Colors.Transparent),
                    BorderBrush = new SolidColorBrush(Colors.Transparent)
                };
                button.Click += Btn_Click;

                Grid.SetRow(button, row);
                Grid.SetColumn(button, column);
                brdMain.BoardGrid.Children.Add(button);
            }
    }

    private void Turn(object sender)
    {
        var btn = (sender as Button)!;
        var row = Grid.GetRow(btn);
        var column = Grid.GetColumn(btn);
        if (_board[row, column] == 0)
        {
            if (_turn)
            {
                _board[row, column] = -1;
                txtTurn.Text = _p2Sym;
                txtTurn.Foreground = _p2Color;
                btn.Content = _p1Sym;
                btn.Foreground = _p1Color;
            }
            else
            {
                _board[row, column] = 1;
                txtTurn.Text = _p1Sym;
                txtTurn.Foreground = _p1Color;
                btn.Content = _p2Sym;
                btn.Foreground = _p2Color;
            }

            var result = ScoreCheck(_board);
            if (result[0] != 0) Win(result);
            else if (FillCheck(_board)) Draw();

            _turn = !_turn;

            if (_isComp)
            {
                CompTurn();
                var result2 = ScoreCheck(_board);
                if (result2[0] != 0) Win(result2);
                else if (FillCheck(_board)) Draw();
                _turn = !_turn;
            }
        }
    }

    private void CompTurn()
    {
        var best = FindBest(_board);
        foreach (var obj in brdMain.BoardGrid.Children)
        {
            var btn = obj as Button;
            if (Grid.GetColumn(btn) == best[1] && Grid.GetRow(btn) == best[0])
            {
                _board[best[0], best[1]] = 1;
                txtTurn.Text = _p1Sym;
                txtTurn.Foreground = _p1Color;
                btn!.Content = _p2Sym;
                btn.Foreground = _p2Color;
            }
        }
    }

    #region Minimax

    private int Minimax(int[,] board, int depth, bool isMax)
    {
        Console.WriteLine(depth);

        var result = ScoreCheck(board);
        if (result[0] != 0) return result[0];
        if (FillCheck(board)) return 0;


        if (isMax)
        {
            var bestVal = int.MinValue;
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    if (board[row, column] == 0)
                    {
                        board[row, column] = 1;
                        var value = Minimax(board, depth + 1, !isMax);
                        bestVal = Math.Max(bestVal,value);
                        board[row, column] = 0;
                    }

                }
            }

            return bestVal;
        }
        else
        {
            var bestVal = int.MaxValue;
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    if (board[row, column] == 0)
                    {
                        board[row, column] = -1;
                        var value = Minimax(board, depth + 1, true);
                        bestVal = Math.Min(bestVal, value);
                        board[row, column] = 0;
                    }
                }
            }

            return bestVal;
        }
    }

    private int[] FindBest(int[,] board)
    {
        int bestVal = int.MinValue;
        int[] best = { -1, -1 };

        for (int row = 0; row < _height; row++)
        {
            for (int column = 0; column < _width; column++)
            {
                if (board[row, column] == 0)
                {
                    board[row, column] = 1;
                    int moveVal = Minimax(board, 0, false);
                    board[row, column] = 0;
                    if (moveVal > bestVal)
                    {
                        best = new[] { row, column };
                        bestVal = moveVal;
                    }
                }
            }
        }

        return best;

    }

    #endregion

    private bool FillCheck(int[,] board)
    {
        var full = true;
        for (var i = 0; i < _width; i++)
            for (var j = 0; j < _height; j++)
                if (board[j, i] == 0)
                    full = false;
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
        _winner = result[0].ToString();

        if (_winner == "-1")
        {
            MessageBox.Show($"{_p1Name} is the winner!", "Winner!", MessageBoxButton.OK);
            Home.P1Score++;
        }
        else
        {
            MessageBox.Show($"{_p2Name} is the winner!", "Winner!", MessageBoxButton.OK);
            Home.P2Score++;
        }

        Close();
        Home.Visibility = Visibility.Visible;
    }

    private void Line(int[] input)
    {
        var boardWidth = brdMain.ActualWidth / Convert.ToDouble(brdMain.BoardGrid.ColumnDefinitions.Count);
        var boardHeight = brdMain.ActualHeight / Convert.ToDouble(brdMain.BoardGrid.RowDefinitions.Count);

        var line = new Line
        {
            Stroke = input[0] == -1 ? _p1Color : _p2Color,
            X1 = (input[1] + 0.5) * boardWidth,
            X2 = (input[3] + 0.5) * boardWidth,
            Y1 = (input[2] + 0.5) * boardHeight,
            Y2 = (input[4] + 0.5) * boardHeight,
            StrokeThickness = 10,
        };

        Grid.Children.Add(line);
    }

    private int[] ScoreCheck(int[,] board)
    {
        for (var row = 0; row < _height - _win + 1; row++)
            for (var column = 0; column < _width - _win + 1; column++)
            {
                var result = SubCheck(board, column, row, _win);
                if (result[0] != 0) return result;
            }

        return new[] { 0 };
    }

    private int[] SubCheck(int[,] board, int x, int y, int size)
    {
        var sum = 0;
        for (var row = 0; row < size; row++) // check rows
        {
            for (var column = 0; column < size; column++) sum += board[x + row, y + column];

            if (sum == size) return new[] { 1, x, y + row, x + size - 1, y + row };
            if (sum == size * -1) return new[] { -1, x, y + row, x + size - 1, y + row };
            sum = 0;
        }

        sum = 0;
        for (var column = 0; column < size; column++) // check columns
        {
            for (var row = 0; row < size; row++) sum += board[x + row, y + column];

            if (sum == size) return new[] { 1, x + column, y, x + column, y + size - 1 };
            if (sum == -1 * size) return new[] { -1, x + column, y, x + column, y + size - 1 };
            sum = 0;
        }

        sum = 0;
        for (var i = 0; i < size; i++) sum += board[x + i, y + i];

        if (sum == size) return new[] {1, x, y, x + size - 1, y + size - 1 };

        if (sum == -1 * size) return new[] {-1, x, y, x + size - 1, y + size - 1 };

        sum = 0;
        var rev = size - 1;
        for (var i = 0; i < size; i++)
        {
            sum += board[x + i, y + rev];
            rev--;
        }

        if (sum == size) return new[] { 1, x, y + size - 1, x + size - 1, y };

        if (sum == -1 * size) return new[] { -1, x, y + size - 1, x + size - 1, y };

        return new[] { 0 };
    }

    public ImageBrush ToBrush(string input)
    {
        return new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/Resources/{input}.png")));
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
                if (btn!.ActualHeight > btn.ActualWidth)
                    btn.FontSize = btn.ActualWidth * 0.7;
                else
                    btn.FontSize = btn.ActualHeight * 0.7;
            }
            catch { }

        }
    }
}