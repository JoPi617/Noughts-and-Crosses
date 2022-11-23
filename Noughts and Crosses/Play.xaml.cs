using System;
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
    private readonly int[,] board;
    private readonly int height;

    public Page1 Home = null!;
    private readonly Brush p1Color;
    private readonly string p1Name;
    private readonly string p1Sym;
    private readonly Brush p2Color;
    private readonly string p2Name;
    private readonly string p2Sym;
    private bool turn;
    private readonly int width;
    private readonly int win;
    private string winner;

    public MainWindow(int height, int width, int win, string newP1, string newP2, Brush p1Color, Brush p2Color,
        string p1Name, string p2Name)
    {
        InitializeComponent();

        BoardSet(height, width);
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

    private void BoardSet(int height, int _width)
    {
        for (var i = 0; i < height; i++) brdMain.BoardGrid.RowDefinitions.Add(new RowDefinition());

        for (var i = 0; i < _width; i++) brdMain.BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());

        for (var row = 0; row < height; row++)
            for (var column = 0; column < _width; column++)
            {
                var button = new Button();
                button.Name = "btn_" + row + "_" + column;
                button.Click += Btn_Click;
                button.Background = new SolidColorBrush(Colors.Transparent);
                button.BorderBrush = new SolidColorBrush(Colors.Transparent);


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
                Win(result);
            else if (FillCheck()) Draw();

            turn = !turn;
        }
    }

    private bool FillCheck()
    {
        var full = true;
        for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
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

        Close();
        Home.Visibility = Visibility.Visible;
    }

    private void Line(int[] input)
    {
        var boardWidth = brdMain.ActualWidth / Convert.ToDouble(brdMain.BoardGrid.ColumnDefinitions.Count);
        var boardHeight = brdMain.ActualHeight / Convert.ToDouble(brdMain.BoardGrid.RowDefinitions.Count);

        var line = new Line
        {
            Stroke = input[0] == 1 ? p1Color : p2Color,
            X1 = (input[1] + 0.5) * boardWidth,
            X2 = (input[3] + 0.5) * boardWidth,
            Y1 = (input[2] + 0.5) * boardHeight,
            Y2 = (input[4] + 0.5) * boardHeight,
            StrokeThickness = 10
        };

        Grid.Children.Add(line);
    }

    private int[] ScoreCheck()
    {
        for (var row = 0; row < height - win + 1; row++)
            for (var column = 0; column < width - win + 1; column++)
            {
                var result = SubCheck(column, row, win);
                if (result[0] != 0) return result;
            }

        return new[] { 0 };
    }

    private int[] SubCheck(int x, int y, int size)
    {
        var sum = 0;
        for (var row = 0; row < size; row++) // check rows
        {
            for (var column = 0; column < size; column++) sum += board[x + row, y + column];

            if (sum == size) return new[] { 2, x, y + row, x + size - 1, y + row };
            if (sum == size * 10) return new[] { 1, x, y + row, x + size - 1, y + row };
            sum = 0;
        }

        sum = 0;
        for (var column = 0; column < size; column++) // check columns
        {
            for (var row = 0; row < size; row++) sum += board[x + row, y + column];

            if (sum == size) return new[] { 2, x + column, y, x + column, y + size - 1 };
            if (sum == 10 * size) return new[] { 1, x + column, y, x + column, y + size - 1 };
            sum = 0;
        }

        sum = 0;
        for (var i = 0; i < size; i++) sum += board[x + i, y + i];

        if (sum == size) return new[] { 2, x, y, x + size - 1, y + size - 1 };

        if (sum == 10 * size) return new[] { 1, x, y, x + size - 1, y + size - 1 };

        sum = 0;
        var rev = size - 1;
        for (var i = 0; i < size; i++)
        {
            sum += board[x + i, y + rev];
            rev--;
        }

        if (sum == size) return new[] { 2, x, y + size - 1, x + size - 1, y };

        if (sum == 10 * size) return new[] { 1, x, y + size - 1, x + size - 1, y };

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