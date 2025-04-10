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

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно добавлен.");
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не был добавлен.");
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

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены.");
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден или не был обновлен");
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

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены.");
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден или не был обновлен");
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

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Пользователь успешно удален.");
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден или не был удален.");
                        }
                    }
                }
            }
            if (AdminPanelRadioLogin.IsChecked == true)
            {
                using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Users WHERE Login = @Login", connection))
                    {
                        command.Parameters.AddWithValue("@Login", AdminPaneltxtBoxLogin.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            { }
                        }
                        //int rowsAffected = command.Execute();
                        //if (rowsAffected > 0)
                        //{
                        //    MessageBox.Show("Пользователь успешно удален.");
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Пользователь не найден или не был удален.");
                        //}
                    }
                }
            }
        }
    }
}
