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
    public string mode = "classic";
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
            = symbP2.Foreground =
            clrP2.Colour;
    }

    private void btnStart_Click(object sender, RoutedEventArgs e)
    {
        if(txtP1Display.Text==txtP2Display.Text)
        {
            MessageBox.Show("Player symbols cannot be the same", "Error", MessageBoxButton.OK);
            return;
        }
        Visibility = Visibility.Collapsed;
            var frm1 = new MainWindow(height, width, win,
                txtP1Display.Text, txtP2Display.Text, txtP1Display.Foreground, txtP2Display.Foreground,
                txtP1Name.Text, txtP2Name.Text,false, time, mode, Background, musics[music]);
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
        var config = new Config();
        config.home = this;
        config.Show();
    }
}