using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Storage.Interface;
using Dapper;
using MySql.Data.MySqlClient;

using Microsoft.Extensions.Configuration;

namespace Storage
{
    /// <summary>
    /// Base repository that handles service's database related requests
    /// </summary>
    public class BaseRepository : IRepository
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="configuration">configuration for base repository.</param>
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string _connectionString => _configuration.GetConnectionString("mysql");

        /// <summary>
        /// Get all data from service's database
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <returns>status code.</returns>
        public Task<IEnumerable<T>> GetAllAsync<T>(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Missing tablename");
            }

            var sql = $"SELECT * FROM {tableName}";

            using (var con = Connect()) 
            {
                Console.Write(con.QueryAsync<T>(sql));
                return con.QueryAsync<T>(sql);
            }
        }

        /// <summary>
        /// Get requested data from service's database based on value.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="columnName">the database table's column name.</param>
        /// <param name="value">value used for comparison with column name.</param>
        /// <returns>status code.</returns>
        public async Task<T> GetAsync<T>(
            string tableName, 
            string columnName, 
            string value)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(value)) 
            {
                throw new ArgumentException("Missing tablename/columnname/value");
            }

            var sql = $"SELECT * FROM {tableName} WHERE {columnName}='{value}'";

            using (var con = Connect())
            {
                return (await con.QueryAsync<T>(sql)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get requested data from service's database based on conditions.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="colVals">conditions for the requested data.</param>
        /// <returns>status code.</returns>
        public Task<IEnumerable<T>> GetSomeAsync<T>(
            string tableName, 
            IReadOnlyDictionary<string, string> colVals)
        {
            if (string.IsNullOrEmpty(tableName) || colVals == null || !colVals.Any()) 
            {
                throw new ArgumentException("Invalid tablename/columnname/value");
            }

            var conditions = string.Join(" AND ", colVals.Select(kvp => string.Format("{0}='{1}'", kvp.Key, kvp.Value)));
            var sql = $"SELECT * FROM {tableName} WHERE {conditions}";

            Console.WriteLine(sql);

            using (var con = Connect())
            {
                return con.QueryAsync<T>(sql);
            }
        }

        /// <summary>
        /// Insert parameter values into the service's database
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="parameterList">the parameter values that will be inserted.</param>
        /// <returns>status code.</returns>
        public Task InsertAsync<T>(string tableName, IEnumerable<IReadOnlyList<string>> parameterList)
        {
            if (string.IsNullOrEmpty(tableName) || parameterList == null)
            {
                throw new ArgumentException("Missing tablename/object");
            }

            var values = parameterList.Select(inner => inner.Select(p => $"'{p}'")).Select(list => $"({string.Join(", ", list)})");
            
            var sql = $"INSERT INTO {tableName} VALUES {string.Join(", ", values)}";
            
            Console.WriteLine(sql);
            
            using (var con = Connect())
            {
                return con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// Update service's database with new data
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="columnName">the database table's column name.</param>
        /// <param name="keyValue">value used for comparison with column name.</param>
        /// <param name="updateLookup">update lookup dictionary that has the new data.</param>
        /// <returns>status code.</returns>
        public Task UpdateAsync(
            string tableName, 
            string columnName, 
            string keyValue, 
            IReadOnlyDictionary<string, string> updateLookup) 
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(keyValue) || updateLookup == null)
            {
                throw new ArgumentException("Missing tablename/columnname/keyvalue/updatelookup");
            }

            if (updateLookup.Count == 0) 
            {
                return Task.CompletedTask;
            }

            var setCaluse = updateLookup.Select(kvp => $"{kvp.Key} = '{kvp.Value}'");
            var sql = $"UPDATE {tableName} SET {string.Join(", ", setCaluse)} WHERE {columnName} = '{keyValue}'";

            using (var con = Connect()) 
            {
                return con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// Remove specified item from the table.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="columnName">the database table's column name.</param>
        /// <param name="keyValue">value used for comparison with column name.</param>
        /// <returns>status code.</returns>
        public Task DeleteAsync<T>(string tableName, string columnName, string keyValue) 
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(keyValue))
            {
                throw new ArgumentException("Missing tablename/columnname/keyvalue");
            }

            var sql = $"DELETE FROM {tableName} WHERE {columnName} = '{keyValue}'";

            using (var con = Connect())
            {
                return con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// Remove all items from the table.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <returns>status code.</returns>
        public Task DeleteAllAsync(string tableName)
        {
            var sql = $"TRUNCATE TABLE {tableName}";
            
            using (var con = Connect())
            {
                return con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// Make connection to the service's database.
        /// </summary>
        /// <returns>connection.</returns>
        public MySqlConnection Connect()
        {
            var con = new MySqlConnection(_connectionString);
            con.Open();
            return con;
        }
    }
}
