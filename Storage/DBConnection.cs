using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Storage
{
    public class DBConnection
    {
        private MySqlConnection Connection { get; set; }

        private readonly IConfiguration _configuration;

        private string _connectionString => _configuration.GetConnectionString("mysql");

        public DBConnection(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public MySqlConnection Connect()
        {
            if (Connection == null) 
            {
                Connection = new MySqlConnection(_connectionString);
            }
            Connection.Open();
            return Connection;
        }
    }
}
