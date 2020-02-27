using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Serialization.Serializers
{
    public class ChunkColumnCompactStorageSerializer : SealedClassSerializerBase<ChunkColumnCompactStorage>
    {
        private readonly IBsonSerializer<ChunkSectionCompactStorage> _serializer = new ChunkSectionCompactStorageSerializer();

        protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, ChunkColumnCompactStorage value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            if (value.Biomes != null)
            {
                writer.WriteName(nameof(ChunkColumnCompactStorage.Biomes));
                writer.WriteStartArray();
                foreach (var eachBiome in value.Biomes)
                    writer.WriteInt32(eachBiome);
                writer.WriteEndArray();
            }

            writer.WriteName(nameof(ChunkColumnCompactStorage.Sections));
            writer.WriteStartArray();
            foreach (var section in value.Sections)
                _serializer.Serialize(context, section);
            writer.WriteEndArray();

            writer.WriteEndDocument();
        }

        protected override ChunkColumnCompactStorage DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var biomes = new int[1024];
            var sections = new ChunkSectionCompactStorage[ChunkConstants.SectionsPerChunk];
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(ChunkColumnCompactStorage.Biomes):
                        reader.ReadStartArray();
                        for (int i = 0; i < 1024; i++)
                            biomes[i] = reader.ReadInt32();
                        reader.ReadEndArray();
                        break;
                    case nameof(ChunkColumnCompactStorage.Sections):
                        reader.ReadStartArray();
                        for (int i = 0; i < sections.Length; i++)
                            sections[i] = _serializer.Deserialize(context);
                        reader.ReadEndArray();
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            var value = new ChunkColumnCompactStorage(biomes);
            for (int i = 0; i < sections.Length; i++)
                value.Sections[i] = sections[i];
            return value;
        }
    }
}
