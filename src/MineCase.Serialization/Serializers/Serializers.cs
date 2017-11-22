using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine.Serialization;
using MongoDB.Bson.Serialization;

namespace MineCase.Serialization.Serializers
{
    public static class Serializers
    {
        public static void RegisterAll(IServiceProvider serviceProvider)
        {
            BsonSerializer.RegisterSerializer(new SlotSerializer());
            BsonSerializer.RegisterSerializer(new DependencyObjectStateSerializer());
            BsonSerializer.RegisterSerializationProvider(new GrainRerferenceSerializerProvider(serviceProvider));
        }
    }
}
