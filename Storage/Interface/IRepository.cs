using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Interface
{
    public interface IRepository
    {
        Task<int> CreateTable(string tableName, Tuple<string, string> primaryKey, List<Tuple<string, string>> columns);

        Task<T> GetAsync<T>(string tableName, string columnName, string value);

        Task<IEnumerable<T>> GetSomeAsync<T>(string tableName, List<string> columnNames, List<string> values);

        Task<IEnumerable<T>> GetAllAsync<T>(string tableName);

        Task InsertAsync<T>(string tableName, IEnumerable<IReadOnlyList<string>> parameterList);

        Task UpdateAsync(string tableName, string columnName, string keyValue, IReadOnlyDictionary<string, string> updateLookup);

        Task DeleteAsync<T>(string tableName, string columnName, string keyValue);
    }
}
