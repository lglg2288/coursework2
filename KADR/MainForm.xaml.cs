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

namespace KADR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                InternalGrid.Margin = new Thickness(0);
                WindowBorder.CornerRadius = new CornerRadius(10);
                TitleBorder.CornerRadius = new CornerRadius(10,10,0,0);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                InternalGrid.Margin = new Thickness(-20);
                WindowBorder.CornerRadius = new CornerRadius(0);
                TitleBorder.CornerRadius = new CornerRadius(0);
            }
            Keyboard.ClearFocus();
        }

        private void btnMinimized_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            Keyboard.ClearFocus();
            InternalGrid.Focus();
        }
    }
}
