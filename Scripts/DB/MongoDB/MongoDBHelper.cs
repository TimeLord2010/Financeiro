using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Financeiro.Scripts.DB {

    public class MongoDBHelper<T> {

        public MongoDBHelper(string clusterName, string user_name, string passowrd, string database, string collection_name) {
            DatabaseStr = database;
            string url = $"mongodb+srv://{user_name}:{passowrd}@{clusterName}.rtrmk.gcp.mongodb.net/{database}?retryWrites=true&w=majority";
            var client = new MongoClient(url);
            Client = client;
            CollectionName = collection_name;
            Database = Client.GetDatabase(DatabaseStr);
        }

        MongoClient Client { get; }

        public string DatabaseStr { get; }

        public string CollectionName { get; }

        public IMongoDatabase Database { get; }

        public IMongoCollection<T> Collection { get => Database.GetCollection<T>(CollectionName); }

        public async Task InsertOneAsync (T item, bool? by_pass_validation = null, CancellationToken cancellationToken = default) {
            var options = new InsertOneOptions();
            options.BypassDocumentValidation = by_pass_validation;
            await Collection.InsertOneAsync(item, options, cancellationToken);
        }

        public async Task<IAsyncCursor<T>> FindAsync (FilterDefinition<T> filter, FindOptions<T> options = default) {
            return await Collection.FindAsync(filter, options);
        }

        public async Task<T> FindOneAsync(FilterDefinition<T> filter, FindOptions<T> options = default) {
            var cursor = await Collection.FindAsync(filter, options);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = default) {
            return await Collection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<UpdateResult> UpdateAsync (FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = default, CancellationToken cancellationToken = default) {
            return await Collection.UpdateOneAsync(filter, update, options, cancellationToken);
        }

        public async Task<DeleteResult> DeleteOneAsync (FilterDefinition<T> filter, CancellationToken cancellationToken = default) {
            return await Collection.DeleteOneAsync(filter, cancellationToken);
        }

    }
}
