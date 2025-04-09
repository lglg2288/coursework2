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
        private void LoadTree()
        {
            currentTable = "Tree";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Tree>(currentTable);
        }
        private void LoadSaveDocs()
        {
            currentTable = "SaveDocs";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<SaveDocs>(currentTable);
        }
        private void LoadPropValue()
        {
            currentTable = "PropValue";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<PropValue>(currentTable);
        }
        private void LoadProp()
        {
            currentTable = "Prop";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Prop>(currentTable);
        }
        private void LoadPost()
        {
            currentTable = "Post";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Post>(currentTable);
        }
        private void LoadPeoples()
        {
            currentTable = "Peoples";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Peoples>(currentTable);
        }
        private void LoadJornalTabel()
        {
            currentTable = "JornalTabel";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<JornalTabel>(currentTable);
        }
        private void LoadJornalKard()
        {
            currentTable = "JornalKard";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<JornalKard>(currentTable);
        }
        private void LoadFieldsJornal()
        {
            currentTable = "FieldsJornal";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<FieldsJornal>(currentTable);
        }
        private void LoadDepartment()
        {
            currentTable = "Department";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Department>(currentTable);
        }
        private void LoadClassName()
        {
            currentTable = "ClassName";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<ClassName>(currentTable);
        }
        private void LoadClassArr()
        {
            currentTable = "ClassArr";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<ClassArr>(currentTable);
        }
        private void LoadClass()
        {
            currentTable = "Class";
            MainDataGrid.ItemsSource = CRUDHelper.ReadAll<Class>(currentTable);
        }
        //DELETE
        private void MainDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

    }
}