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


namespace KADR
{
    public partial class MainForm : Window
    {
        private const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;";
        private bool isAnimating = false;
        private CrudHelper<Users> currentCrud;
        private Type currentEntityType;
        public interface ICrud<T> where T : IDbEntity
        {
            void Insert(T entity);
            void Update(T entity);
            void Delete(int id); // Удаление по ID
            void Save(IEnumerable<T> entities);
            IEnumerable<T> GetAll();
        }


        private void LoadUsers()
        {
            currentCrud = new CrudHelper<Users>("Users", connectionString);
            MainDataGrid.ItemsSource = currentCrud.GetAll();
            currentEntityType = typeof(Users);
        }
        //DELETE
        private void MainDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

    }
}