using System.ComponentModel;
using System.Windows;
using static System.Convert;

namespace Noughts_and_Crosses;

/// <summary>
///     Interaction logic for Config.xaml
/// </summary>
public partial class Config : Window
{
    public Page1 Home = null!;

    public Config()
    {
        InitializeComponent();
    }

    private void btnCustom1_Click(object sender, RoutedEventArgs e)
    {
        var custom = new Custom();
        custom.Show();
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        if (sldWin.Value > sldWidth.Value && sldWin.Value > sldHeight.Value)
        {
            MessageBox.Show("Win size cannot be greater than width and height", "Error", MessageBoxButton.OK);
            return;
        }

        Close();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        Home.width = ToInt32(sldWidth.Value);
        Home.height = ToInt32(sldHeight.Value);
        Home.win = ToInt32(sldWin.Value);
        Home.time = ToInt32(sldTime.Value);
        Home.Mode = drpModes.SelectedIndex;
        Home.Music = drpMusic.SelectedIndex;
        Home.Back = drpBack.SelectedIndex;
        Home.Visibility = Visibility.Visible;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        sldWidth.Value = Home.width;
        sldHeight.Value = Home.height;
        sldWin.Value = Home.win;
        sldTime.Value = Home.time;
        drpModes.SelectedIndex = Home.Mode;
        drpMusic.SelectedIndex = Home.Music;
        drpBack.SelectedIndex = Home.Back;
    }
}