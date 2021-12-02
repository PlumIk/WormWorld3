using System;
using System.Data.SqlClient;

namespace AnyTests
{
    public class SqlBase
    {
        private string _connectionString = "Server=localhost;Database=ForWorms;User Id=SA;Password=Sasha353!;";
        private SqlConnection _connection;

        public SqlBase()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public bool SetConnect()
        {
            try
            {
                // Открываем подключение
                
                _connection.Open();
            }
            catch (SqlException)
            {
                return false;
            }

            return true;
        }

        public void EndConnect()
        {
            _connection.Close();
        }

        public SqlDataReader ExecuteCommand(string commandLine)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = commandLine;
            command.Connection = _connection;
            return command.ExecuteReader();
        }
        
    }
}