using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Storage
{
    /// <summary>
    /// Handles service's database connection
    /// </summary>
    public class DBConnection
    {
        private readonly IConfiguration _configuration;

        private string ConnectionString => this._configuration.GetConnectionString("mysql");

        /// <summary>
        /// Initializes a new instance of the <see cref="DBConnection"/> class.
        /// </summary>
        /// <param name="configuration">connection string for the database.</param>
        public DBConnection(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private MySqlConnection Connection { get; set; }

        /// <summary>
        /// Make connection to the service's database.
        /// </summary>
        /// <returns>connection.</returns>
        public MySqlConnection Connect()
        {
            if (this.Connection == null) 
            {
                this.Connection = new MySqlConnection(ConnectionString);
            }

            this.Connection.Open();
            return this.Connection;
        }
    }
}
