using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using System;
using System.Windows.Input;
using System.Collections;
using static KADR.MainForm;
using System.Linq;


namespace KADR
{
    public partial class MainForm : Window
    {
        private void MainDataGridHide(Action onCompleted = null)
        {
            isAnimating = true;

            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500)
            };
            fadeOut.Completed += (_, __) =>
            {
                MainDataGrid.Visibility = Visibility.Collapsed;
                isAnimating = false;
                onCompleted?.Invoke();
            };
            MainDataGrid.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
        private void MainDataGridShow(Action onCompleted = null)
        {
            if (MainDataGrid.Visibility == Visibility.Collapsed)
            {
                isAnimating = true;
                MainDataGrid.Visibility = Visibility.Visible;

                DoubleAnimation fadeIn = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(500),
                };

                fadeIn.Completed += (_, __) => {
                    isAnimating = false;
                    onCompleted?.Invoke();
                };
                MainDataGrid.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            }
        }
    }
}