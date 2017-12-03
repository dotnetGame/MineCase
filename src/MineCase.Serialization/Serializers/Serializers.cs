using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine.Serialization;
using MineCase.Graphics;
using MongoDB.Bson.Serialization;

namespace MineCase.Serialization.Serializers
{
    public static class Serializers
    {
        public static void RegisterAll(IServiceProvider serviceProvider)
        {
            BsonSerializer.RegisterSerializationProvider(new GrainRerferenceSerializerProvider(serviceProvider));
        }

        public static void RegisterAll()
        {
            BsonClassMap.RegisterClassMap<Shape>(c =>
            {
                c.SetDiscriminatorIsRequired(true);
                c.SetIsRootClass(true);
                c.AddKnownType(typeof(Cuboid));
            });

            BsonSerializer.RegisterSerializer(new SlotSerializer());
            BsonSerializer.RegisterSerializer(new DependencyObjectStateSerializer());
            BsonSerializer.RegisterSerializer(new BlockChunkPosSerializer());
            BsonSerializer.RegisterSerializer(new BlockWorldPosSerializer());
            BsonSerializer.RegisterSerializer(new ChunkWorldPosSerializer());
            BsonSerializer.RegisterSerializer(new BlockSectionPosSerializer());
            BsonSerializer.RegisterSerializer(new EntityWorldPosSerializer());
            BsonSerializer.RegisterSerializer(new EntityChunkPosSerializer());
            BsonSerializer.RegisterSerializer(new Point3dSerializer());
            BsonSerializer.RegisterSerializer(new SizeSerializer());
            BsonSerializer.RegisterSerializer(new ChunkColumnCompactStorageSerializer());
            BsonSerializer.RegisterSerializer(new ChunkSectionCompactStorageSerializer());
            BsonSerializer.RegisterSerializer(new GameModeSerializer());
        }
    }
}
