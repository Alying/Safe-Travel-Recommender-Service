using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Interface
{
    public interface IRepository
    {
        Task<T> GetAsync<T>(string tableName, string columnName, string keyValue);

        Task<IEnumerable<T>> GetAllAsync<T>(string tableName);

        Task InsertAsync<T>(string tableName, IEnumerable<IReadOnlyList<string>> parameterList);

        Task UpdateAsync(string tableName, string columnName, string keyValue, IReadOnlyDictionary<string, string> updateLookup);

        Task DeleteAsync<T>(string tableName, string columnName, string keyValue);
    }
}
