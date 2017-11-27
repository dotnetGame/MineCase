using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.World;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Serialization.Serializers
{
    public class ChunkSectionCompactStorageSerializer : SealedClassSerializerBase<ChunkSectionCompactStorage>
    {
        private readonly IBsonSerializer<ChunkSectionCompactStorage.NibbleArray> _nibbleSerializer = new NibbleArraySerializer();
        private readonly IBsonSerializer<ChunkSectionCompactStorage.DataArray> _dataSerializer = new DataArraySerializer();

        protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, ChunkSectionCompactStorage value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(ChunkSectionCompactStorage.Data));
            _dataSerializer.Serialize(context, value.Data);

            writer.WriteName(nameof(ChunkSectionCompactStorage.BlockLight));
            _nibbleSerializer.Serialize(context, value.BlockLight);

            if (value.SkyLight != null)
            {
                writer.WriteName(nameof(ChunkSectionCompactStorage.SkyLight));
                _nibbleSerializer.Serialize(context, value.SkyLight);
            }

            writer.WriteEndDocument();
        }

        protected override ChunkSectionCompactStorage DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var data = default(ChunkSectionCompactStorage.DataArray);
            var blockLight = default(ChunkSectionCompactStorage.NibbleArray);
            var skyLight = default(ChunkSectionCompactStorage.NibbleArray);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(ChunkSectionCompactStorage.Data):
                        data = _dataSerializer.Deserialize(context);
                        break;
                    case nameof(ChunkSectionCompactStorage.BlockLight):
                        blockLight = _nibbleSerializer.Deserialize(context);
                        break;
                    case nameof(ChunkSectionCompactStorage.SkyLight):
                        skyLight = _nibbleSerializer.Deserialize(context);
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return new ChunkSectionCompactStorage(data, blockLight, skyLight);
        }

        private class DataArraySerializer : SealedClassSerializerBase<ChunkSectionCompactStorage.DataArray>
        {
            protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, ChunkSectionCompactStorage.DataArray value)
            {
                var writer = context.Writer;

                var data = new byte[value.Storage.Length * sizeof(ulong)];
                using (var bw = new BinaryWriter(new MemoryStream(data, true)))
                {
                    foreach (var item in value.Storage)
                        bw.Write(item);
                }

                writer.WriteBytes(data);
            }

            protected override ChunkSectionCompactStorage.DataArray DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
            {
                var reader = context.Reader;

                var data = reader.ReadBytes();
                var storage = new ulong[data.Length / sizeof(ulong)];
                using (var br = new BinaryReader(new MemoryStream(data, false)))
                {
                    for (int i = 0; i < storage.Length; i++)
                        storage[i] = br.ReadUInt64();
                }

                return new ChunkSectionCompactStorage.DataArray(storage);
            }
        }

        private class NibbleArraySerializer : SealedClassSerializerBase<ChunkSectionCompactStorage.NibbleArray>
        {
            protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, ChunkSectionCompactStorage.NibbleArray value)
            {
                var writer = context.Writer;

                writer.WriteBytes(value.Storage);
            }

            protected override ChunkSectionCompactStorage.NibbleArray DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
            {
                var reader = context.Reader;

                return new ChunkSectionCompactStorage.NibbleArray(reader.ReadBytes());
            }
        }
    }
}
