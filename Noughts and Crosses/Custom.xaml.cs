﻿using System;
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
using System.Windows.Shapes;

namespace Noughts_and_Crosses
{
    /// <summary>
    /// Interaction logic for Custom.xaml
    /// </summary>
    public partial class Custom : Window
    {
        public Custom()
        {
            InitializeComponent();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var bytes = Convert.FromHexString(txtEntry.Text);
                var chars = Encoding.BigEndianUnicode.GetChars(bytes);
                string str = new string(chars);

        
                Symbols.Source.Add(str);
                Close();

            }
            catch
            {
                MessageBox.Show("Unrecognised Unicode", "Error", MessageBoxButton.OK);
                Close();
            }
        }
    }
}