using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Media.Animation;
using static System.Net.Mime.MediaTypeNames;
using Brush = System.Drawing.Brush;
using static System.Convert;
using Color = System.Drawing.Color;


namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isNought;
        private int[,] board;
        private int boardsize;

        private void Turn(int collumn, int row, object obj)
        {
            if (board[row, collumn] == 0)
            {
                Button button = (obj as Button)!;
                if (isNought)
                {
                    board[row, collumn] = 10;
                    imgTurn.Source = ToSource("Cross");
                    button.Background = ToBrush("Nought");
                }
                else
                {
                    board[row, collumn] = 1;
                    imgTurn.Source = ToSource("Nought");
                    button.Background = ToBrush("Cross");
                }

                lblWin.Content = Scorecheck();

                isNought = !isNought;
            }
        }

        private string? Scorecheck()
        {
            int sum = 0;
            for (int row = 0; row < 3; row++) // check rows
            {
                for (int column = 0; column < 3; column++)
                {
                    sum += board[row, column];
                }

                if (sum == 3)
                {
                    return "cross";
               
                }
                if (sum==30)
                {
                    return "nought";
                }
                sum = 0;
            }

            sum = 0;
            for (int column = 0; column < 3; column++) // check columns
            {
                for (int row = 0; row < 3; row++)
                {
                    sum += board[row, column];
                }

                if (sum == 3)
                {
                    return "cross";
                    
                }
                if (sum == 30)
                {
                    return "nought";
                    
                }
                sum = 0;
            }

            sum = 0;

            if (board[0, 0] + board[1, 1] + board[2, 2] == 3 || board[0, 2] + board[1, 1] + board[2, 0] == 3)
            {
                return "cross";
            }

            if (board[0, 0] + board[1, 1] + board[2, 2] == 30 || board[0, 2] + board[1, 1] + board[2, 0] == 30)
            {
                return "nought";
            }

            return null;
        }

        public ImageBrush ToBrush(string input)
        {
            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/" + input + ".png")));
        }

        public ImageSource ToSource(string input)
        {
            return new BitmapImage(new Uri("pack://application:,,,/Resources/" + input + ".png"));
        }

        public MainWindow(int size)
        {
            InitializeComponent();
            boardsize = size;
            for (int i = 0; i < size; i++)
            {
                BrdMain.BoardGrid.RowDefinitions.Add(new RowDefinition());
                BrdMain.BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Button button = new Button();
                    button.Name = "btn_" + row + "_" + column;
                    button.Click += Btn_Click;
                    button.Background = new SolidColorBrush(new System.Windows.Media.Color());
                    button.BorderBrush = new SolidColorBrush(new System.Windows.Media.Color());

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    BrdMain.BoardGrid.Children.Add(button);
                }
            }

            board = new int[size, size];
        }


        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var array = button!.Name.Split('_');
            Turn(ToInt32(array[2]), ToInt32(array[1]), sender);
        }
    }
}
