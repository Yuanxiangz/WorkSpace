using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QQApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string name = string.Empty;

        public Window1()
        {
            InitializeComponent();
            UserTextBox.Focus();
        }

        private void UserTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                name = UserTextBox.Text.Trim();
                if (name != string.Empty)
                {
                    this.Close();
                }
            }
        }

        public string GetName()
        {
            return name;
        }
    }
}
