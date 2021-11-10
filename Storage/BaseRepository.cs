﻿using Storage.Interface;
using System;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Storage
{
    public class BaseRepository : IRepository
    {
        private readonly IConfiguration _configuration;

        private string _connectionString => _configuration.GetConnectionString("mysql");

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<int> CreateTable(string tableName, Tuple<string, string> primaryKey, List<Tuple<string, string>> columns)
        {
            var otherCols = columns.Select(entry => entry.Item1 + " " + entry.Item2);
            var cols = "(" + primaryKey.Item1 + " " + primaryKey.Item2 + " PRIMARY KEY, " + String.Join(", ", otherCols) + ")";
            var sql = $"CREATE TABLE IF NOT EXISTS {tableName} {cols}";
            Console.WriteLine(sql);
            using (var con = Connect())
            {
                return con.ExecuteAsync(sql);
            }
        }

        public Task<IEnumerable<T>> GetAllAsync<T>(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Missing tablename");
            }

            var sql = $"SELECT * FROM {tableName}";

            using (var con = Connect()) 
            {
                return con.QueryAsync<T>(sql);
            }
        }

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

        public Task<IEnumerable<T>> GetSomeAsync<T>(
            string tableName, 
            List<string> columnNames, 
            List<string> values)
        {
            if (string.IsNullOrEmpty(tableName) || columnNames == null || !columnNames.Any() || values == null || !values.Any() || columnNames.Count != values.Count) 
            {
                throw new ArgumentException("Invalid tablename/columnname/value");
            }

            string conditions = "";
            for(int idx = 0; idx < columnNames.Count; idx++)
            {
                conditions += columnNames[idx] + "='" + values[idx] + "'";
                if(idx != columnNames.Count - 1)
                {
                    conditions += " AND ";
                }
            }

            var sql = $"SELECT * FROM {tableName} WHERE {conditions}";

            Console.WriteLine(sql);

            using (var con = Connect())
            {
                return con.QueryAsync<T>(sql);
            }
        }

        public Task InsertAsync<T>(string tableName, IEnumerable<IReadOnlyList<string>> parameterList)
        {
            if (string.IsNullOrEmpty(tableName) || parameterList == null)
            {
                throw new ArgumentException("Missing tablename/object");
            }

            var values = parameterList.Select(inner => inner.Select(p => $"'{p}'")).Select(list => $"({string.Join(", ", list)})");

            var sql = $"INSERT INTO {tableName} VALUES {string.Join(", ", values)}";
            using (var con = Connect())
            {
                return con.ExecuteAsync(sql);
            }
        }

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

            if(updateLookup.Count == 0) 
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

        public MySqlConnection Connect()
        {
            var con = new MySqlConnection(_connectionString);
            con.Open();
            return con;
        }
    }
}
