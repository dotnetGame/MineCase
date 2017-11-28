using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MineCase.Engine.Serialization;
using MineCase.Server.Settings;
using MongoDB.Driver;

namespace MineCase.Server.Persistence
{
    internal class AppDbContext
    {
        private readonly IMongoDatabase _db;

        public AppDbContext(IOptions<PersistenceOptions> options)
        {
            var url = new MongoUrl(options.Value.ConnectionString);
            var client = new MongoClient(url);
            _db = client.GetDatabase(url.DatabaseName);
        }

        public IMongoCollection<DependencyObjectState> GetEntityStateCollection(string name)
        {
            return _db.GetCollection<DependencyObjectState>(name);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
