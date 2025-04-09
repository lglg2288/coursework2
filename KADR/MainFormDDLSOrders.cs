using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Navigation;

namespace KADR
{
    public partial class MainForm : Window
    {
        List<JornalKard> JornalKardObj;
        public class JornalKardViewModel
        {
            public int ID { get; set; }
            public int PostId { get; set; }
            public int PeoplesId { get; set; }
            public int ClassId { get; set; }
            public int TypeDoscId { get; set; }
            public string NumDoc { get; set; }
            public DateTime DateDoc { get; set; }
            public string Status { get; set; }
            public string Name { get; set; }
            public string FullName { get; set; }

            public List<Post> PostList { get; set; }
            public List<Peoples> PeoplesList { get; set; }
            public List<Class> ClassList { get; set; }
            public List<TypeDosc> TypeDoscList { get; set; }
        }

        // Пример метода загрузки данных в DataGrid
        public void LoadJornalKardData()
        {
            List<JornalKardViewModel> data = new List<JornalKardViewModel>();

            const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT * FROM JornalKard"; // Запрос для вашей таблицы

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var viewModel = new JornalKardViewModel
                        {
                            ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            PostId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                            PeoplesId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                            ClassId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                            TypeDoscId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                            NumDoc = reader.IsDBNull(5) ? null : reader.GetString(5),
                            DateDoc = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                            Status = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Name = reader.IsDBNull(8) ? null : reader.GetString(8),
                            FullName = reader.IsDBNull(9) ? null : reader.GetString(9),
                        };

                        // Загружаем данные для ComboBox
                        viewModel.PostList = GetPosts();// Метод для получения данных для Post
                        viewModel.PeoplesList = GetPeople(); // Метод для получения данных для People
                        viewModel.ClassList = GetClasses(); // Метод для получения данных для Classes
                        viewModel.TypeDoscList = GetTypeDocs(); // Метод для получения данных для TypeDocs

                        data.Add(viewModel);
                    }
                }
            }

            JornalKardDataGrid.ItemsSource = data;
        }
        public List<Post> GetPosts()
        {
            return CRUDHelper.ReadAll<Post>("Post").ToList();
        }

        public List<Peoples> GetPeople()
        {
            return CRUDHelper.ReadAll<Peoples>("Peoples");
        }

        public List<Class> GetClasses()
        {
            return CRUDHelper.ReadAll<Class>("Class");
        }

        public List<TypeDosc> GetTypeDocs()
        {
            return CRUDHelper.ReadAll<TypeDosc>("TypeDosc");
        }

        public void SaveUpdatedJornalKard(List<JornalKard> updatedData)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=YourDatabase;Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var jurnal in updatedData)
                {
                    string updateQuery = "UPDATE JornalKard SET PostId = @PostId, PeoplesId = @PeoplesId, " +
                                         "ClassId = @ClassId, TypeDoscId = @TypeDoscId, NumDoc = @NumDoc, " +
                                         "DateDoc = @DateDoc, Status = @Status, Name = @Name, FullName = @FullName " +
                                         "WHERE ID = @ID";

                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PostId", jurnal.PostId);
                        command.Parameters.AddWithValue("@PeoplesId", jurnal.PeoplesId);
                        command.Parameters.AddWithValue("@ClassId", jurnal.ClassId);
                        command.Parameters.AddWithValue("@TypeDoscId", jurnal.TypeDoscId);
                        command.Parameters.AddWithValue("@NumDoc", jurnal.NumDoc);
                        command.Parameters.AddWithValue("@DateDoc", jurnal.DateDoc);
                        command.Parameters.AddWithValue("@Status", jurnal.Status);
                        command.Parameters.AddWithValue("@Name", jurnal.Name);
                        command.Parameters.AddWithValue("@FullName", jurnal.FullName);
                        command.Parameters.AddWithValue("@ID", jurnal.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<JornalKard> GetUpdatedJornalKardData()
        {
            List<JornalKard> updatedData = new List<JornalKard>();

            foreach (var item in JornalKardDataGrid.ItemsSource)
            {
                if (item is JornalKard jurnal)
                {
                    updatedData.Add(jurnal);
                }
            }

            return updatedData;
        }


        private void btnSaveJornalKardDataGrid_Click(object sender, RoutedEventArgs e)
        {
            SaveUpdatedJornalKard(GetUpdatedJornalKardData());
        }


    }
}
