﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Convert;

namespace Noughts_and_Crosses;

/// <summary>
///     Interaction logic for Colours.xaml
/// </summary>
public partial class Colours : UserControl
{
    public Colours()
    {
        InitializeComponent();
        Colour = new SolidColorBrush(Colors.Red);
    }

    public Brush Colour { get; set; }

    private void Value_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Colour = new SolidColorBrush(Color.FromRgb(ToByte(sldR.Value), ToByte(sldG.Value), ToByte(sldB.Value)));
    }
}