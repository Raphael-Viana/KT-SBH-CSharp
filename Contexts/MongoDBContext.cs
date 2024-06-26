﻿using KTCSharpApi.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KTCSharpApi.Contexts
{
    public abstract class MongoDBContext<TModel> : IMongoDBContext<TModel>
        where TModel : BaseModel
    {
        public MongoDBContext()
        {
            var connectionString = "mongodb://localhost:27017";
            MongoClient client = new(connectionString);
            _database = client.GetDatabase("KTCsharpApi");
        }

        private readonly IMongoDatabase _database;

        private IMongoCollection<TModel> Collection
        {
            get
            {
                return _database.GetCollection<TModel>(typeof(TModel).Name);
            }
        }

        protected IMongoQueryable<TModel> Query
        {
            get
            {
                return Collection.AsQueryable();
            }
        }

        public async Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Query
                .Where(w => w.Id == id)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(TModel model, CancellationToken cancellationToken)
        {
            await Collection.InsertOneAsync(model, cancellationToken);
        }

        public async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
        {
            await Collection.ReplaceOneAsync(w => w.Id == model.Id, model, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await Collection.DeleteOneAsync(w => w.Id == id, cancellationToken);
        }
    }
}
