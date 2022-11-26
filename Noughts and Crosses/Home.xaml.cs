﻿using System;
using System.Windows;
using System.Windows.Input;
using static System.Convert;

namespace Noughts_and_Crosses;

/// <summary>
///     Interaction logic for Page1.xaml
/// </summary>
public partial class Page1 : Window
{
    private int p1Score;
    private int p2Score;

    public int width;
    public int height;
    public int win;
    public string mode;
    public int time;

    public Page1()
    {
        InitializeComponent();
        clrP1.sldB.Value = 255;
        clrP2.sldR.Value = 255;
        txtP1Score.Foreground = clrP1.Colour;
        txtP2Score.Foreground = clrP2.Colour;
        symbP1.Foreground = clrP1.Colour;
        symbP2.Foreground = clrP2.Colour;
        symbP1.SelectedIndex = 0;
        symbP2.SelectedIndex = 1;
    }

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
        try
        {
            if (ToInt32(txtHeight.Text) < ToInt32(txtWin.Text) && ToInt32(txtWidth.Text) < ToInt32(txtWin.Text))
            {
                MessageBox.Show("Win size must be less than size", "Error", MessageBoxButton.OK);
                return;
            }
        }
        catch
        {
            MessageBox.Show("Dimensions must be integers", "Error", MessageBoxButton.OK);
            return;
        }
        if(txtP1Display.Text==txtP2Display.Text)
        {
            MessageBox.Show("Player symbols cannot be the same", "Error", MessageBoxButton.OK);
            return;
        }
        Visibility = Visibility.Collapsed;
            var frm1 = new MainWindow(ToInt32(txtHeight.Text), ToInt32(txtWidth.Text), ToInt32(txtWin.Text),
                txtP1Display.Text, txtP2Display.Text, txtP1Display.Foreground, txtP2Display.Foreground,
                txtP1Name.Text, txtP2Name.Text,false);
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
        var config = new Config();
        config.home = this;
        config.Show();
    }
}