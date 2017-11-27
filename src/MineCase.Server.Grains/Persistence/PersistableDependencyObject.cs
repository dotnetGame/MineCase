using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Engine;
using MineCase.Engine.Serialization;
using MongoDB.Driver;
using Orleans.Concurrency;

namespace MineCase.Server.Persistence
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class PersistTableName : Attribute
    {
        public string TableName { get; }

        public PersistTableName(string tableName)
        {
            TableName = tableName;
        }
    }

    [Reentrant]
    public abstract class PersistableDependencyObject : DependencyObject
    {
        protected override async Task SerializeStateAsync(DependencyObjectState state)
        {
            var coll = GetStateCollection();
            var key = GrainReference.ToKeyString();
            await coll.ReplaceOneAsync(o => o.GrainKeyString == key, state, new UpdateOptions { IsUpsert = true });
        }

        protected override async Task<DependencyObjectState> DeserializeStateAsync()
        {
            var coll = GetStateCollection();
            var key = GrainReference.ToKeyString();
            return await coll.Find(o => o.GrainKeyString == key).FirstOrDefaultAsync();
        }

        protected override async Task ClearStateAsync()
        {
            var coll = GetStateCollection();
            var key = GrainReference.ToKeyString();
            await coll.DeleteOneAsync(o => o.GrainKeyString == key);
        }

        private IMongoCollection<DependencyObjectState> GetStateCollection()
        {
            var db = ServiceProvider.GetRequiredService<AppDbContext>();
            return db.GetEntityStateCollection(GetTablePrefix());
        }

        private string GetTablePrefix()
        {
            return GetType().GetCustomAttribute<PersistTableName>().TableName;
        }
    }
}
