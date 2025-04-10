using System;
using System.Data.SqlClient;
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
using System.Windows.Media.Animation;

namespace KADR
{
    /// <summary>
    /// Логика взаимодействия для FormPass.xaml
    /// </summary>
    public partial class FormPass : Window
    {
        public FormPass()
        {
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AuthenticateUser(TextBoxLogin.Text, PasswordBoxPass.Password, out currentUserStatus.isAdmin))
            {
                new MainForm().Show();
                this.Close();
            }
            else
            {
                AnimationCrest();
            }
        }

        private void AnimationCrest()
        {
            int speedOffset = -1;

            CrossPath.Visibility = Visibility.Visible;
            
            xt1.Point = new Point(180, 105);
            xt2.Point = new Point(180, 105);
            xt3.Point = new Point(180, 105);
            xt4.Point = new Point(180, 105);

            var xt1Anime = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(160, 85),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            var xt2Anime = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(200, 85),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var xt3Anime = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(200, 125),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var xt4Anime = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(160, 125),
                Duration = TimeSpan.FromSeconds(2.3 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };



            var lop1Anime1 = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(145, 100),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var lop2Anime1 = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(185, 70),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var lop3Anime1 = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(215, 110),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var lop4Anime1 = new PointAnimation
            {
                From = new Point(180, 105),
                To = new Point(175, 140),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };



            var lop1Anime2 = new PointAnimation
            {
                From = new Point(145, 100),
                To = new Point(150, 75),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            var lop2Anime2 = new PointAnimation
            {
                From = new Point(185, 70),
                To = new Point(210, 75),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var lop3Anime2 = new PointAnimation
            {
                From = new Point(215, 110),
                To = new Point(210, 135),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            var lop4Anime2 = new PointAnimation
            {
                From = new Point(175, 140),
                To = new Point(150, 135),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };


            var final = new PointAnimation
            {
                To = new Point(180, 105),
                Duration = TimeSpan.FromSeconds(2 + speedOffset),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            xt1Anime.Completed += (s, e) =>
            {
                lop1Anime1.Completed += (ss, ee) =>
                {
                    lop1Anime2.Completed += (sss, eee) =>
                    {
                        final.Completed += (ssss, eeee) =>
                        {
                            CrossPath.Visibility = Visibility.Hidden;
                        };
                        lop1.Point = new Point(150, 75);
                        lop2.Point = new Point(210, 75);
                        lop3.Point = new Point(210, 135);
                        lop4.Point = new Point(150, 135);
                        lineFigure5.StartPoint = new Point(180, 105);
                        lineFigure6.StartPoint = new Point(180, 105);
                        lineFigure7.StartPoint = new Point(180, 105);
                        lineFigure8.StartPoint = new Point(180, 105);
                        xt1.BeginAnimation(LineSegment.PointProperty, final);
                        xt2.BeginAnimation(LineSegment.PointProperty, final);
                        xt3.BeginAnimation(LineSegment.PointProperty, final);
                        xt4.BeginAnimation(LineSegment.PointProperty, final);
                        lop1.BeginAnimation(LineSegment.PointProperty, final);
                        lop2.BeginAnimation(LineSegment.PointProperty, final);
                        lop3.BeginAnimation(LineSegment.PointProperty, final);
                        lop4.BeginAnimation(LineSegment.PointProperty, final);
                    };
                    lop1.BeginAnimation(LineSegment.PointProperty, lop1Anime2);
                    lop2.BeginAnimation(LineSegment.PointProperty, lop2Anime2);
                    lop3.BeginAnimation(LineSegment.PointProperty, lop3Anime2);
                    lop4.BeginAnimation(LineSegment.PointProperty, lop4Anime2);
                };
                lineFigure5.StartPoint = new Point(160, 85);
                lineFigure6.StartPoint = new Point(200, 85);
                lineFigure7.StartPoint = new Point(200, 125);
                lineFigure8.StartPoint = new Point(160, 125);
                lop1.BeginAnimation(LineSegment.PointProperty, lop1Anime1);
                lop2.BeginAnimation(LineSegment.PointProperty, lop2Anime1);
                lop3.BeginAnimation(LineSegment.PointProperty, lop3Anime1);
                lop4.BeginAnimation(LineSegment.PointProperty, lop4Anime1);
            };
            xt1.BeginAnimation(LineSegment.PointProperty, xt1Anime);
            xt2.BeginAnimation(LineSegment.PointProperty, xt2Anime);
            xt3.BeginAnimation(LineSegment.PointProperty, xt3Anime);
            xt4.BeginAnimation(LineSegment.PointProperty, xt4Anime);
        }

        bool AuthenticateUser(string login, string password, out bool isAdmin)
        {
            isAdmin = false;

            using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
            {
                connection.Open();

                string query = "SELECT Adm FROM Users WHERE Login = @login AND PASS = @pass";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@pass", password);
                    
                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        isAdmin = Convert.ToBoolean(result);
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
