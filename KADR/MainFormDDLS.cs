using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using System;
using System.Windows.Input;
using System.Collections;
using Newtonsoft.Json;
using System.Linq;
using Dapper;
using System.ComponentModel;
using System.Windows.Controls;


namespace KADR
{
    public partial class MainForm : Window
    {
        private string currentTable;
        private Type currentType;
        private void LoadUsers()
        {
            currentTable = "Users";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Users>(currentTable);
        }
        private void LoadTypeDosc()
        {
            currentTable = "TypeDosc";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<TypeDosc>(currentTable);
        }
        //DELETE
        private void MainDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

    }
}