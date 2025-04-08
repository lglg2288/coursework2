using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Reflection;

namespace KADR
{
    interface ICrud<T>
    {
        void Save(IEnumerable<T> entities);
    }
    class CRUDHelper
    {
        private const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Кадр;Integrated Security=True;TrustServerCertificate=True;";
        static public List<T> ReadAll<T>(string tableName) where T : new()
        {
            var result = new List<T>();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (var dbCommand = new SqlCommand($"SELECT * FROM {tableName}", dbConnection))
                using (var reader = dbCommand.ExecuteReader())
                {
                    var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    while (reader.Read())
                    {
                        var obj = new T();

                        foreach (var prop in props)
                        {
                            if (!reader.HasColumn(prop.Name)) continue;

                            var value = reader[prop.Name];
                            if (value == DBNull.Value) continue;

                            prop.SetValue(obj, value);
                        }

                        result.Add(obj);
                    }
                }
            }

            return result;
        }
        static public void Save<T>(IEnumerable<T> itemsSource, string tableName)
        {
            if (itemsSource == null) return;

            var itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var insertableProps = props.Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase)).ToList();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                using (var deleteCommand = new SqlCommand($"DELETE FROM {tableName}", dbConnection))
                {
                    deleteCommand.ExecuteNonQuery();
                }

                using (var reseedCommand = new SqlCommand($"DBCC CHECKIDENT ('{tableName}', RESEED, 0)", dbConnection))
                {
                    reseedCommand.ExecuteNonQuery();
                }

                // Вставляем новые записи
                foreach (var item in itemsSource)
                {
                    var columnNames = new List<string>();
                    var paramNames = new List<string>();
                    var parameters = new List<SqlParameter>();

                    foreach (var prop in insertableProps)
                    {
                        var value = prop.GetValue(item, null);
                        var paramName = $"@{prop.Name}";

                        columnNames.Add(prop.Name);
                        paramNames.Add(paramName);
                        parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                    }

                    string insertQuery = $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) VALUES ({string.Join(",", paramNames)})";

                    using (var insertCommand = new SqlCommand(insertQuery, dbConnection))
                    {
                        insertCommand.Parameters.AddRange(parameters.ToArray());
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }


    }

    public static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }














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
