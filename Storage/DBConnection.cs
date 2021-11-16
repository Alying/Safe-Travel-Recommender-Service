// <copyright file="DBConnection.cs" company="ASE#">
//     Copyright (c) ASE#. All rights reserved.
// </copyright>

namespace Storage
{
    using System;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;

    public class DBConnection
    {
        private readonly IConfiguration _configuration;

        private string ConnectionString => this._configuration.GetConnectionString("mysql");

        public DBConnection(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private MySqlConnection Connection { get; set; }

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
