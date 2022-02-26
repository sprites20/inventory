using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace inventory
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }
        private void txtClose_MouseEnter(object sender, MouseEventArgs e)
        {
            txtClose.Foreground = Brushes.White;
        }

        private void txtClose_MouseLeave(object sender, MouseEventArgs e)
        {
            txtClose.Foreground = Brushes.Black;
        }
        private void txtClose_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "admin" && txtPass.Password == "admin")
            {
                MainWindow newWindow = new MainWindow();
                newWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please Try Again");
        }
        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                if (txtUser.Text == "admin" && txtPass.Password == "admin")
                {
                    MainWindow newWindow = new MainWindow();
                    newWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Please Try Again");
            }
        }
        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                if (txtUser.Text == "admin" && txtPass.Password == "admin")
                {
                    MainWindow newWindow = new MainWindow();
                    newWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Please Try Again");
            }
        }
    }
}
