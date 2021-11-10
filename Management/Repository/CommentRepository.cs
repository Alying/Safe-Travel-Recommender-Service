using Management.DomainModels;
using Management.Interface;
using Optional;
using Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using DomainComment = Management.DomainModels.Comment;
using StorageComment = Management.StorageModels.Comment;

namespace Management.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private const string _tableName = "comment";

        private const string _keyColumnName = "uniqueId";
        
        private readonly IRepository _repository;

        public CommentRepository(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            // Create the table if not exist.
            // - To start mysql server:
            // cd /c/Program\ Files/MySQL/MySQL\ Server\ 8.0/bin/
            // mkdir ../data (if it's the first time)
            // ./mysqld.exe
            // - To check if mysql server is started:
            // net start | grep MySQL
            // - To connect to mysql:
            // cd /c/Program\ Files/MySQL/MySQL\ Server\ 8.0/bin/
            // mysql.ext -uroot -pasesharp
            // password is asesharp
            Console.WriteLine("@mli: Before calling CreateTable...");
            _repository.CreateTable(_tableName,
                new Tuple<string, string>("uniqueId", "VARCHAR(100)"),
                new List<Tuple<string, string>>() {
                    new Tuple<string, string>("userId", "VARCHAR(100)"),
                    new Tuple<string, string>("country", "VARCHAR(50)"),
                    new Tuple<string, string>("state", "VARCHAR(50)"),
                    new Tuple<string, string>("createdAt", "VARCHAR(50)"),
                    new Tuple<string, string>("commentStr", "VARCHAR(500)")}).Wait();
        }

        public async Task<IEnumerable<DomainComment>> GetAllCommentsAsync(UserId userId, Location location)
        {
            var result = await _repository.GetSomeAsync<StorageComment>(_tableName, new List<string>() { "userId", "country", "state" }, new List<string>() { userId.Value, location.Country.Value, location.State.Value});
            return result.Select(comment => Mapping.StorageToDomainMapper.ToDomain(comment));
        }

        public async Task AddCommentAsync(DomainComment comment)
        {
            var storageComment = Mapping.DomainToStorageMapper.ToStorage(comment);
            // uniqueId, userId, country, state, createdAt, commentStr.
            var fields = new List<string>() { storageComment.UniqueId, storageComment.UserId, storageComment.Country, storageComment.State, storageComment.CreatedAt, storageComment.CommentStr };
            await _repository.InsertAsync<StorageComment>(_tableName, new List<List<string>>() { fields });
        }
    }
}
