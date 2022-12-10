using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Convert;

namespace Noughts_and_Crosses;

/// <summary>
///     Interaction logic for Page1.xaml
/// </summary>
public partial class Page1 : Window
{
    #region Fields

    private MediaPlayer player = new();

    private int p1Score;
    private int p2Score;

    public int width = 3;
    public int height = 3;
    public int win = 3;
    public int time = 5;

    private int music;
    public int Music
    {
        get => music;
        set
        {
            if (value is < 3 and > -1)
            {
                music = value;
                player.Open(musics[value]);
                player.Play();
            }
            else
            {
                player.Stop();
            }
        }
    }
    private List<Uri> musics = new()
    {
        new(@"C:\\Users\\jhp33\\source\\repos\\School\\N&C\\Noughts and Crosses\\Noughts and Crosses\\Resources\\orchesta theme.wav"),
        new(@"C:\\Users\\jhp33\\source\\repos\\School\\N&C\\Noughts and Crosses\\Noughts and Crosses\\Resources\\8bit theme.wav"),
        new(@"C:\Users\jhp33\source\repos\School\N&C\Noughts and Crosses\Noughts and Crosses\Resources\organ theme.wav"),
    };

    private int back;

    public int Back
    {
        get => back;
        set
        {
            if (value is < 3 and > -1)
            {
                back = value;
                Background = backs[value];
            }
            else
            {
                Background = new SolidColorBrush(Colors.DarkSlateGray);
            }
        }
    }

    private List<Brush> backs = new()
    {
        new SolidColorBrush(Colors.DarkSlateGray),
        new ImageBrush(new BitmapImage(new Uri(@"C:\Users\jhp33\source\repos\School\N&C\Noughts and Crosses\Noughts and Crosses\Resources\Mandelbrot.png"))),
        new ImageBrush(new BitmapImage(new Uri(@"C:\Users\jhp33\source\repos\School\N&C\Noughts and Crosses\Noughts and Crosses\Resources\Snowflake.png"))),
        new ImageBrush(new BitmapImage(new Uri(@"C:\Users\jhp33\source\repos\School\N&C\Noughts and Crosses\Noughts and Crosses\Resources\Square.png"))),
    };

    private int mode;

    public int Mode
    {
        get => mode;
        set
        {
            if (value is < 4 and > -1)
            {
                mode = value;
            }
            else
            {
                mode = 0;
            }
        }
    }

    private List<string> modes = new()
    {
        "Classic",
        "Random",
        "Mystery",
        "Two Turn",
    };

    public int P1Score
    {
        get => p1Score;
        set
        {
            txtP1Score.Text = $"Score: {value}";
            p1Score = value;
        }
    }

    public int P2Score
    {
        get => p2Score;
        set
        {
            txtP2Score.Text = $"Score: {value}";
            p2Score = value;
        }
    }

    #endregion


    public Page1()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Set();
    }

    private void Set()
    {
        clrP1.sldB.Value = 255;
        clrP2.sldR.Value = 255;
        txtP1Score.Foreground = clrP1.Colour;
        txtP2Score.Foreground = clrP2.Colour;
        symbP1.Foreground = clrP1.Colour;
        symbP2.Foreground = clrP2.Colour;
        symbP1.SelectedIndex = 0;
        symbP2.SelectedIndex = 1;
        player.Open(musics[Music]);
        player.Play();
        width = 3;
        height = 3;
        win = 3;
        time = 5;
        p1Score = 0;
        txtP1Score.Text = txtP2Score.Text = "Score: 0";
        p2Score = 0;
        symbP1.SelectedIndex = 0;
        symbP2.SelectedIndex = 1;
        txtP1Name.Text = "Player 1";
        txtP2Name.Text = "Player 2";
        clrP1.sldR.Value = clrP1.sldG.Value = clrP2.sldG.Value = clrP2.sldB.Value = 0;
        clrP1.sldB.Value = clrP2.sldR.Value = 255;
        clrP1_MouseMove(null!, null!);
        clrP2_MouseMove(null!, null!);
        tckComp.IsChecked = false;
    }

    private void clrP1_MouseMove(object sender, MouseEventArgs e)
    {
        txtP1Score.Foreground = txtP1Display.Foreground  = txtP1Name.Foreground
            = symbP1.Foreground = 
            clrP1.Colour;
    }

    private void clrP2_MouseMove(object sender, MouseEventArgs e)
    {
        txtP2Score.Foreground = txtP2Display.Foreground  = txtP2Name.Foreground 
            = symbP2.Foreground = tckComp.Foreground =
            clrP2.Colour;
    }

    private void btnStart_Click(object sender, RoutedEventArgs e)
    {
        if(txtP1Display.Text==txtP2Display.Text)
        {
            MessageBox.Show("Player symbols cannot be the same", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Visibility = Visibility.Collapsed;
        bool isComp = tckComp.IsChecked != null && (bool)tckComp.IsChecked;
        if (isComp && (height>3 || width>3))
        {
            MessageBox.Show("Warning: computer player may be slow on larger grids", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        var frm1 = new MainWindow(height, width, win,
                txtP1Display.Text, txtP2Display.Text, txtP1Display.Foreground, txtP2Display.Foreground,
                txtP1Name.Text, txtP2Name.Text, isComp, time,
                modes[mode], Background, musics[music]);
            frm1.Home = this;
            frm1.Show();
        }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        try
        {
            btnStart.FontSize = Window.ActualWidth * 0.05;
            symbP1.FontSize = symbP2.FontSize = Window.ActualWidth * 0.02;
        }
        catch { }
    }

    private void btnSettings_Click(object sender, RoutedEventArgs e)
    {
        Hide();
        var config = new Config { Home = this };
        config.Show();
    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        Set();
    }
}