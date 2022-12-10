using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Noughts_and_Crosses;
//draw goes at inverted x,y

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly int[,] _board;
    private readonly int _firstTime;
    private readonly int _height;
    private readonly bool _isComp;
    private readonly Brush _p1Color;
    private readonly string _p1Name;
    private readonly string _p1Sym;
    private readonly Brush _p2Color;
    private readonly string _p2Name;
    private readonly string _p2Sym;
    private readonly int _width;
    private readonly int _win;
    private int _currentTime;
    private readonly DispatcherTimer _dispatcherTimer = new();
    private bool _isP1Turn;
    private int _turns;
    private string _winner;
    public Page1 Home = null!;
    public string Mode;


    public MainWindow(int height, int width, int win, string newP1, string newP2, Brush p1Color, Brush p2Color,
        string p1Name, string p2Name, bool isComp, int time, string mode, Brush back, Uri music)
    {
        InitializeComponent();

        BoardSet(height, width);
        _isP1Turn = true;
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
        _currentTime = time * 100;
        _firstTime = time * 100;
        Mode = mode;

        Background = back;
        MediaPlayer player = new();
        player.Open(music);
        player.Play();


        _dispatcherTimer.Tick += dispatcherTimer_Tick;
        _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        _dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object? sender, EventArgs e)
    {
        if (_isComp && !_isP1Turn) CompTurn();
        if (ScoreCheck(_board)[0] != 0) return;
        _currentTime--;
        if (_currentTime == -1)
        {
            if (_isP1Turn)
            {
                MessageBox.Show($"{_p1Name} ran out of time, {_p2Name} is the winner!",
                    "Winner!", MessageBoxButton.OK);
                Home.P2Score++;
            }
            else
            {
                MessageBox.Show($"{_p2Name} ran out of time, {_p1Name} is the winner!",
                    "Winner!", MessageBoxButton.OK);
                Home.P1Score++;
            }

            Close();
            Home.Visibility = Visibility.Visible;
        }

        txtTime.Text = _currentTime % 100 < 10
            ? $"{_currentTime / 100}.{"0" + _currentTime % 100}"
            : $"{_currentTime / 100}.{_currentTime % 100}";
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

    private void Turn(Button btn)
    {
        var row = Grid.GetRow(btn);
        var column = Grid.GetColumn(btn);

        if (Mode != "Mystery")
        {
            _board[row, column] = _isP1Turn ? -1 : 1;
            btn.Content = _isP1Turn ? _p1Sym : _p2Sym;
            btn.Foreground = _isP1Turn ? _p1Color : _p2Color;
        }
        else
        {
            _board[row, column] = _isP1Turn ? -1 : 1;
            btn.Content = "?";
            btn.Foreground = Brushes.BurlyWood;
        }

        var result = ScoreCheck(_board);
        if (result[0] != 0) Win(result);
        else if (FillCheck(_board)) Draw();

        _currentTime = _firstTime;

        switch (Mode)
        {
            case "Mystery":
            case "Classic":
                _isP1Turn = !_isP1Turn;
                break;
            case "Random":
                var rand = new Random();
                if (rand.Next(0, 2) == 1) _isP1Turn = !_isP1Turn;
                break;
            case "Two Turn":
                if (_turns == 1)
                {
                    _turns = 0;
                    _isP1Turn = !_isP1Turn;
                }

                break;
        }

        txtTurn.Text = _isP1Turn ? _p1Sym : _p2Sym;
        txtTurn.Foreground = _isP1Turn ? _p1Color : _p2Color;
    }

    private bool FillCheck(int[,] board)
    {
        for (var i = 0; i < _width; i++)
        for (var j = 0; j < _height; j++)
            if (board[j, i] == 0)
                return false;
        return true;
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
        var cellWidth = brdMain.ActualWidth / Convert.ToDouble(brdMain.BoardGrid.ColumnDefinitions.Count);
        var cellHeight = brdMain.ActualHeight / Convert.ToDouble(brdMain.BoardGrid.RowDefinitions.Count);

        var line = new Line
        {
            Stroke = input[0] == -1 ? _p1Color : _p2Color,
            X1 = (input[1] + 0.5) * cellWidth,
            X2 = (input[3] + 0.5) * cellWidth,
            Y1 = (input[2] + 0.5) * cellHeight,
            Y2 = (input[4] + 0.5) * cellHeight,
            StrokeThickness = 10
        };
        Grid.SetColumnSpan(line, _width);
        Grid.SetRowSpan(line, _height);
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
            for (var column = 0; column < size; column++) sum += board[y + row, x + column];

            if (sum == size) return new[] { 1, x, y + row, x + size - 1, y + row };
            if (sum == size * -1) return new[] { -1, x, y + row, x + size - 1, y + row };
            sum = 0;
        }

        sum = 0;
        for (var column = 0; column < size; column++) // check columns
        {
            for (var row = 0; row < size; row++) sum += board[y + row, x + column];

            if (sum == size) return new[] { 1, x + column, y, x + column, y + size - 1 };
            if (sum == -1 * size) return new[] { -1, x + column, y, x + column, y + size - 1 };
            sum = 0;
        }

        sum = 0;
        for (var i = 0; i < size; i++) sum += board[y + i, x + i];

        if (sum == size) return new[] { 1, x, y, x + size - 1, y + size - 1 };

        if (sum == -1 * size) return new[] { -1, x, y, x + size - 1, y + size - 1 };

        sum = 0;
        var rev = size - 1;
        for (var i = 0; i < size; i++)
        {
            sum += board[y + i, x + rev];
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
        var btn = sender as Button;
        if (btn != null && _board[Grid.GetRow(btn), Grid.GetColumn(btn)] == 0) Turn(btn);
    }

    private void CompTurn()
    {
        var best = FindBest(_board);
        foreach (var obj in brdMain.BoardGrid.Children)
            if (obj is Button btn && Grid.GetColumn(btn) == best[1] && Grid.GetRow(btn) == best[0])
                Turn(btn);
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
            catch
            {
            }
        }
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        _dispatcherTimer.Stop();
        Home.Visibility = Visibility.Visible;
    }

    #region Minimax

    private int Minimax(int[,] board, int depth, bool isMax, int alpha, int beta)
    {
        var result = ScoreCheck(board);
        if (result[0] != 0) return result[0] * 11 + result[0] > 0 ? depth : -depth;
        if (FillCheck(board) || depth > 5) return 0;

        //   && (_width > 3 || _height > 3))

        if (isMax)
        {
            var bestVal = int.MinValue;
            for (var row = 0; row < _height; row++)
            {
                for (var column = 0; column < _width; column++)
                    if (board[row, column] == 0)
                    {
                        board[row, column] = 1;
                        var value = Minimax(board, depth + 1, !isMax, alpha, beta);
                        board[row, column] = 0;
                        bestVal = Math.Max(bestVal, value);
                        alpha = Math.Max(alpha, bestVal);
                        if (beta <= alpha) break;
                    }

                if (beta <= alpha) break;
            }

            return bestVal;
        }
        else
        {
            var bestVal = int.MaxValue;
            for (var row = 0; row < _height; row++)
            {
                for (var column = 0; column < _width; column++)
                    if (board[row, column] == 0)
                    {
                        board[row, column] = -1;
                        var value = Minimax(board, depth + 1, true, alpha, beta);
                        board[row, column] = 0;
                        bestVal = Math.Min(bestVal, value);
                        beta = Math.Min(beta, bestVal);
                        if (beta <= alpha)
                            break;
                    }

                if (beta <= alpha) break;
            }

            return bestVal;
        }
    }

    private int[] FindBest(int[,] board)
    {
        var bestVal = int.MinValue;
        int[] best = { -1, -1 };

        for (var row = 0; row < _height; row++)
        for (var column = 0; column < _width; column++)
            if (board[row, column] == 0)
            {
                board[row, column] = 1;
                var moveVal = Minimax(board, 0, false, int.MinValue, int.MaxValue);
                board[row, column] = 0;
                if (moveVal > bestVal)
                {
                    best = new[] { row, column };
                    bestVal = moveVal;
                }
            }

        return best;
    }

    #endregion
}