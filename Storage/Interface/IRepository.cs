using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Interface
{
    /// <summary>
    /// Base repository interface that defines functions that handles service's database related requests
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get requested data from service's database based on value.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="columnName">the database table's column name.</param>
        /// <param name="value">value used for comparison with column name.</param>
        /// <returns>status code.</returns>
        Task<T> GetAsync<T>(string tableName, string columnName, string value);

        /// <summary>
        /// Get requested data from service's database based on conditions.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="colVals">conditions for the requested data.</param>
        /// <returns>status code.</returns>
        Task<IEnumerable<T>> GetSomeAsync<T>(string tableName, IReadOnlyDictionary<string, string> colVals);

        /// <summary>
        /// Get all data from service's database
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <returns>status code.</returns>
        Task<IEnumerable<T>> GetAllAsync<T>(string tableName);

        /// <summary>
        /// Insert parameter values into the service's database
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="parameterList">the parameter values that will be inserted.</param>
        /// <returns>status code.</returns>
        Task InsertAsync<T>(string tableName, IEnumerable<IReadOnlyList<string>> parameterList);

        /// <summary>
        /// Update service's database with new data
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="columnName">the database table's column name.</param>
        /// <param name="keyValue">value used for comparison with column name.</param>
        /// <param name="updateLookup">update lookup dictionary that has the new data.</param>
        /// <returns>status code.</returns>
        Task UpdateAsync(string tableName, string columnName, string keyValue, IReadOnlyDictionary<string, string> updateLookup);

        /// <summary>
        /// Remove specified item from the table.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <param name="columnName">the database table's column name.</param>
        /// <param name="keyValue">value used for comparison with column name.</param>
        /// <returns>status code.</returns>
        Task DeleteAsync<T>(string tableName, string columnName, string keyValue);

        /// <summary>
        /// Remove all items from the table.
        /// </summary>
        /// <param name="tableName">the service database's table name.</param>
        /// <returns>status code.</returns>
        Task DeleteAllAsync(string tableName);
    }
}
