using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace libveil
{
    public class DBConnection
    {
        private static string connectionString = @"Data Source=nnbs23\SQLEXPRESS;Initial Catalog=LibraryDB;Integrated Security=True";
        private static DBConnection instance = null;
        private SqlConnection connection = null;

        private DBConnection()
        {
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка создания подключения: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBConnection();
                return instance;
            }
        }

        public bool OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия подключения: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка закрытия подключения: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения запроса: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        // Перегрузка метода для простых запросов без параметров
        public int ExecuteNonQuery(string query)
        {
            int affected = 0;
            try
            {
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        affected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения команды: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                affected = -1;
            }
            finally
            {
                CloseConnection();
            }
            return affected;
        }

        // Перегрузка метода для параметризованных запросов
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            int affected = 0;
            try
            {
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                        affected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения команды: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                affected = -1;
            }
            finally
            {
                CloseConnection();
            }
            return affected;
        }

        public object ExecuteScalar(string query)
        {
            object result = null;
            try
            {
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения запроса: " + ex.Message, "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}