using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.Persistence
{
    public abstract class PersistableStateBase
    {
        [BsonId]
        public string GrainKeyString { get; set; }
    }

    public abstract class PersistableGrain<TState> : Grain
        where TState : PersistableStateBase
    {
        private IGrainReferenceConverter _grainReferenceConverter;

        public override async Task OnActivateAsync()
        {
            _grainReferenceConverter = ServiceProvider.GetRequiredService<IGrainReferenceConverter>();

            var coll = GetStateCollection();
            var key = GrainReference.ToKeyString();

            await LoadStateAsync(await coll.Find(o => o.GrainKeyString == key).FirstOrDefaultAsync());
        }

        protected abstract Task LoadStateAsync(TState state);

        public override async Task OnDeactivateAsync()
        {
            var coll = GetStateCollection();
            var key = GrainReference.ToKeyString();
            var state = await SaveStateAsync();
            await coll.ReplaceOneAsync(o => o.GrainKeyString == key, state, new UpdateOptions { IsUpsert = true });
        }

        protected abstract Task<TState> SaveStateAsync();

        private IMongoCollection<TState> GetStateCollection()
        {
            var db = ServiceProvider.GetRequiredService<AppDbContext>();
            return db.GetCollection<TState>(GetTablePrefix());
        }

        private string GetTablePrefix()
        {
            return GetType().GetCustomAttribute<PersistTableName>().TableName;
        }

        protected static string GetGrainKeyString(IGrain grain)
        {
            var refer = (GrainReference)grain.AsReference<IGrain>();
            return refer.ToKeyString();
        }

        protected TGrain GetGrainFromKeyString<TGrain>(string grainKey)
            where TGrain : IGrain
        {
            var refer = _grainReferenceConverter.GetGrainFromKeyString(grainKey);
            var grain = refer.Cast<TGrain>();
            GrainFactory.BindGrainReference(grain);
            return grain;
        }
    }
}
