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
        public const string DatabaseName = "minecase";

        private readonly IMongoDatabase _db;

        public AppDbContext(IOptions<PersistenceOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(DatabaseName);
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
