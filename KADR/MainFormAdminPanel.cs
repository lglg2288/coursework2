using System;
using System.Windows;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace KADR
{
    public partial class MainForm : Window
    {
        private void AdminPanelAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AdminPaneltxtBoxName.Text) ||
                string.IsNullOrWhiteSpace(AdminPaneltxtBoxLogin.Text) ||
                string.IsNullOrWhiteSpace(AdminPanelPassword.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO Users (Name, Login, PASS, Adm) VALUES (@Name, @Login,  @PASS,  @Adm)", connection))
                {

                    command.Parameters.AddWithValue("@Name", AdminPaneltxtBoxName.Text);
                    command.Parameters.AddWithValue("@Login", AdminPaneltxtBoxLogin.Text);
                    command.Parameters.AddWithValue("@PASS", AdminPanelPassword.Password);
                    command.Parameters.AddWithValue("@Adm", CheckboxIsAdmin.IsChecked == true);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        MessageBox.Show(reader.HasRows.ToString());
                        while (reader.Read())
                        {
                            MessageBox.Show(reader.HasRows.ToString());
                        }
                    }
                }
            }
        }
        private void AdminPanelUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (AdminPanelRadioId.IsChecked == true)
            {
                using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE Users SET Name = @Name, Login = @Login, PASS = @PASS, Adm = @Adm WHERE ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@Name", AdminPaneltxtBoxName.Text);
                        command.Parameters.AddWithValue("@Login", AdminPaneltxtBoxLogin.Text);
                        command.Parameters.AddWithValue("@PASS", AdminPanelPassword.Password);
                        command.Parameters.AddWithValue("@Adm", CheckboxIsAdmin.IsChecked == true);
                        command.Parameters.AddWithValue("@ID", AdminPaneltxtBoxID.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MessageBox.Show(reader.HasRows.ToString());
                            while (reader.Read())
                            {
                                MessageBox.Show(reader.HasRows.ToString());
                            }
                        }
                    }
                }
            }
            if (AdminPanelRadioLogin.IsChecked == true)
            {
                using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE Users SET Name = @Name, PASS = @PASS, Adm = @Adm WHERE Login = @Login", connection))
                    {
                        command.Parameters.AddWithValue("@Name", AdminPaneltxtBoxName.Text);
                        command.Parameters.AddWithValue("@Login", AdminPaneltxtBoxLogin.Text);
                        command.Parameters.AddWithValue("@PASS", AdminPanelPassword.Password);
                        command.Parameters.AddWithValue("@Adm", CheckboxIsAdmin.IsChecked == true);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MessageBox.Show(reader.HasRows.ToString());
                            while (reader.Read())
                            {
                                MessageBox.Show(reader.HasRows.ToString());
                            }
                        }
                    }
                }
            }
        }
        private void AdminPanelDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (AdminPanelRadioId.IsChecked == true)
            {
                using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", Convert.ToInt32(AdminPaneltxtBoxID.Text));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MessageBox.Show(reader.HasRows.ToString());
                            while (reader.Read())
                            {
                                MessageBox.Show(reader.HasRows.ToString());
                            }
                        }
                    }
                }
            }
            if (AdminPanelRadioLogin.IsChecked == true)
            {
                using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    SqlCommand command = null;

                    if (AdminPanelRadioId.IsChecked == true)
                    {
                        command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection);
                        command.Parameters.AddWithValue("@ID", Convert.ToInt32(AdminPaneltxtBoxID.Text));
                    }
                    else if (AdminPanelRadioLogin.IsChecked == true)
                    {
                        command = new SqlCommand("DELETE FROM Users WHERE Login = @Login", connection);
                        command.Parameters.AddWithValue("@Login", AdminPaneltxtBoxLogin.Text);
                    }

                    if (command != null)
                    {
                        int rows = command.ExecuteNonQuery();
                        MessageBox.Show(rows > 0 ? "Пользователь удалён" : "Ничего не найдено для удаления");
                    }

                    AdminPaneltxtBoxID.Clear();
                    AdminPaneltxtBoxLogin.Clear();
                }
            }
        }
    }
}
