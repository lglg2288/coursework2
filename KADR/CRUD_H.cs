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

                        if (value is DateTime dt)
                        {
                            if (dt < new DateTime(1753, 1, 1) || dt > DateTime.Now.AddYears(10)) // Пример ограничения
                                value = DateTime.Now;
                            else
                                value = DBNull.Value;
                        }

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
}
