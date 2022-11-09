using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Noughts_and_Crosses
{
    public class Data : DependencyObject
    {
        /*
        public int Height { get; set; }
        public int Width { get; set; }
        public int Win { get; set; }
        public int[,] Board { get; set; } = new int[1, 1];
        public string Winner { get; set; } = "";
        public string P1 { get; set; } = "";
        public string P2 { get; set; } = "" ;
        public bool Turn { get; set; }

        */
        public static readonly DependencyProperty WinnerProperty =
            DependencyProperty.Register(
                nameof(Winner),
                typeof(string),
                typeof(Data), 
                new PropertyMetadata("Hello")
            );

        public string Winner
        {
            get => (string)GetValue(WinnerProperty);
            set => SetValue(WinnerProperty, value);
        }


        public static readonly DependencyProperty TurnProperty =
            DependencyProperty.Register(
                nameof(Turn),
                typeof(bool),
                typeof(Data), null
            );
        public bool Turn
        {
            get => (bool)GetValue(TurnProperty);
            set => SetValue(TurnProperty, value);
        }

        public static readonly DependencyProperty P1Property =
            DependencyProperty.Register(
                nameof(P1),
                typeof(string),
                typeof(Data), null
            );
        public string P1
        {
            get => (string)GetValue(P1Property);
            set => SetValue(P1Property, value);
        }

        public static readonly DependencyProperty P2Property =
            DependencyProperty.Register(
                nameof(P2),
                typeof(string),
                typeof(Data), null
            );
        public string P2
        {
            get => (string)GetValue(P2Property);
            set => SetValue(P2Property, value);
        }

        public static readonly DependencyProperty BoardProperty =
            DependencyProperty.Register(
                nameof(Board),
                typeof(int[,]),
                typeof(Data), null
            );
        public int[,] Board
        {
            get => (int[,])GetValue(BoardProperty);
            set => SetValue(BoardProperty, value);
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register(
                nameof(Height),
                typeof(string),
                typeof(Data), null
            );
        public int Height
        {
            get => (int)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register(
                nameof(Width),
                typeof(string),
                typeof(Data), null
            );
        public int Width
        {
            get => (int)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static readonly DependencyProperty WinProperty =
            DependencyProperty.Register(
                nameof(Win),
                typeof(string),
                typeof(Data), null
            );
        public int Win
        {
            get => (int)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }
        /*
        private string winner = "";
        public string Winner
        {
            get => winner;
            set
            {
                winner = value;
                RaisePropertyChanged("winner");
            }
        }

        private string p1 = "";
        public string P1
        {
            get => p1;
            set
            {
                p1 = value;
                RaisePropertyChanged("p1");
            }
        }

        private string p2 ="";
        public string P2
        {
            get => p2;
            set
            {
                p2 = value;
                RaisePropertyChanged("p2");
            }
        }

        private bool turn = false;
        public bool Turn
        {
            get => turn;
            set
            {
                turn = value;
                RaisePropertyChanged("turn");
            }
        }

        private int[,] board = new int[3,3];
        public int[,] Board
        {
            get => board;
            set
            {
                board = value;
                RaisePropertyChanged("board");
            }
        }

        private int height = 3;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                RaisePropertyChanged("height");
            }
        }

        private int width = 3;
        public int Width
        {
            get => width;
            set
            {
                width = value;
                RaisePropertyChanged("width");
            }
        }

        private int win = 3;
        public int Win
        {
            get => win;
            set
            {
                win = value;
                RaisePropertyChanged("board");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propertyName)
        {
            var handlers = PropertyChanged;
            handlers(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}
