using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KADR.MainForm;
using Dapper;

namespace KADR
{
    //WTF
    public class CrudHelper<T> : ICrud<T> where T : IDbEntity, new()
    {
        private readonly string tableName;
        private readonly string connectionString;

        public CrudHelper(string tableName, string conn)
        {
            this.tableName = tableName;
            connectionString = conn;
        }
    public void Save(IEnumerable<T> entities)
        {
            // Реализация сохранения коллекции объектов в базу данных
            // Например, можно пройтись по коллекции и вызвать Insert/Update для каждого элемента
            foreach (var entity in entities)
            {
                if (entity.ID == 0) // Например, если ID == 0, то это новый объект
                {
                    Insert(entity); // Вставка нового объекта
                }
                else
                {
                    Update(entity); // Обновление существующего объекта
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            var list = new List<T>();
            using (var conn = new SqlConnection(connectionString))
            {
                // Пример для Dapper:
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = $"SELECT * FROM {tableName}";
                    var result = connection.Query<T>(query).ToList();
                    return result;
                }
            }
        }
        public void Insert(T item)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "ID").ToList();
            var columns = string.Join(", ", props.Select(p => p.Name));
            var parameters = string.Join(", ", props.Select(p => "@" + p.Name));

            string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    foreach (var prop in props)
                    {
                        cmd.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(item) ?? DBNull.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(T item)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "ID").ToList();
            var setClause = string.Join(", ", props.Select(p => $"{p.Name} = @{p.Name}"));
            string sql = $"UPDATE {tableName} SET {setClause} WHERE ID = @ID";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    foreach (var prop in props)
                        cmd.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(item) ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@ID", item.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sql = $"DELETE FROM {typeof(T).Name} WHERE ID = @id";  // Параметр @id теперь будет в запросе с маленькой буквы

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    // Используем правильное имя параметра
                    cmd.Parameters.AddWithValue("@id", id);  // Параметр теперь с маленькой буквы для согласованности с SQL-запросом
                    cmd.ExecuteNonQuery();  // Выполнение команды
                }
            }
        }

    }

}
