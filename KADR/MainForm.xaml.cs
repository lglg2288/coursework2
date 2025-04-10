using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

            MenuList.Items.Clear();
            MenuList.ItemsSource = MyLeftMenus.myMenus[(int)MyLeftMenus.MenuType.Root];//привязка коллекции
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
                WindowBorder.Margin = new Thickness(20);
                WindowBorder.CornerRadius = new CornerRadius(10);
                TitleBorder.CornerRadius = new CornerRadius(10, 10, 0, 0);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                WindowBorder.Margin = new Thickness(0);
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

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border border = (Border)sender;

            border.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, border.ActualWidth, border.ActualHeight),
                RadiusX = border.CornerRadius.TopLeft,
                RadiusY = border.CornerRadius.TopLeft
            };
        }
        private void MenuList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (isAnimating)
                return;

            if (MainDataGrid.ItemsSource != null)
            {
                var itemType = MainDataGrid.ItemsSource.GetType().GetGenericArguments()[0];
                var method = typeof(CRUDHelper).GetMethod("Save");
                var genericMethod = method.MakeGenericMethod(itemType);
                genericMethod.Invoke(null, new object[] { MainDataGrid.ItemsSource, currentTable });
            }

            DoubleAnimation animeHide = new DoubleAnimation()
            {
                From = 200,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                FillBehavior = FillBehavior.Stop
            };
            DoubleAnimation animeShow = new DoubleAnimation()
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromMilliseconds(300),
                FillBehavior = FillBehavior.Stop
            };

            AdminPanel.Visibility = Visibility.Hidden;
            JornalKardDataGrid.Visibility = Visibility.Hidden;
            btnSaveJornalKardDataGrid.Visibility = Visibility.Hidden;
            ReportToExel.Visibility = Visibility.Hidden;

            MainDataGridHide(() =>
            {

                switch (MyLeftMenus.currentMenuType)
                {
                    case MyLeftMenus.MenuType.Root:
                        switch (MenuList.SelectedIndex)
                        {
                            case int menuElem when menuElem == MyLeftMenus.Element["📁 Справочники"]:
                                animeHide.Completed += (s, eIn) =>
                                {
                                    MenuList.ItemsSource = null;
                                    MenuList.ItemsSource = MyLeftMenus.myMenus[(int)MyLeftMenus.MenuType.Tables];
                                    MyLeftMenus.currentMenuType = MyLeftMenus.MenuType.Tables;

                                    MenuList.BeginAnimation(WidthProperty, animeShow);
                                };
                                MenuList.BeginAnimation(WidthProperty, animeHide);
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["⚙️ Админист-е"]:
                                AdminPanel.Visibility = Visibility.Visible;
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["📝 Приказы"]: 
                                JornalKardDataGrid.Visibility = Visibility.Visible;
                                btnSaveJornalKardDataGrid.Visibility = Visibility.Visible;
                                LoadJornalKardData();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["📊 Отчеты"]:
                                ReportToExel.Visibility = Visibility.Visible;
                                break;
                        }
                        break;
                    case MyLeftMenus.MenuType.Tables:
                        switch (MenuList.SelectedIndex)
                        {
                            case 0: //..
                                animeHide.Completed += (s, eIn) =>
                                {
                                    MenuList.ItemsSource = null;
                                    MenuList.ItemsSource = MyLeftMenus.myMenus[(int)MyLeftMenus.MenuType.Root];
                                    MyLeftMenus.currentMenuType = MyLeftMenus.MenuType.Root;

                                    MenuList.BeginAnimation(WidthProperty, animeShow);
                                };
                                MenuList.BeginAnimation(WidthProperty, animeHide);
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Users"]:
                                LoadUsers();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["TypeDocs"]:
                                LoadTypeDosc();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Tree"]:
                                LoadTree();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["SaveDocs"]:
                                LoadSaveDocs();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["PropValue"]:
                                LoadPropValue();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Prop"]:
                                LoadProp();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Post"]:
                                LoadPost();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Peoples"]:
                                LoadPeoples();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["JornalTabel"]:
                                LoadJornalTabel();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["JornalKard"]:
                                LoadJornalKard();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["FieldsJornal"]:
                                LoadFieldsJornal();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Department"]:
                                LoadDepartment();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["ClassName"]:
                                LoadClassName();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["ClassArr"]:
                                LoadClassArr();
                                MainDataGridShow();
                                break;
                            case int menuElem when menuElem == MyLeftMenus.Element["Class"]:
                                LoadClass();
                                MainDataGridShow();
                                break;
                        }
                        break;
                }
            });


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}