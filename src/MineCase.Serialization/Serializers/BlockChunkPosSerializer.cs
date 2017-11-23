using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Serialization.Serializers
{
    public class BlockChunkPosSerializer : StructSerializerBase<BlockChunkPos>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BlockChunkPos value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(BlockChunkPos.X));
            writer.WriteInt32(value.X);

            writer.WriteName(nameof(BlockChunkPos.Y));
            writer.WriteInt32(value.Y);

            writer.WriteName(nameof(BlockChunkPos.Z));
            writer.WriteInt32(value.Z);

            writer.WriteEndDocument();
        }

        public override BlockChunkPos Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(BlockChunkPos);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(BlockChunkPos.X):
                        value.X = reader.ReadInt32();
                        break;
                    case nameof(BlockChunkPos.Y):
                        value.Y = reader.ReadInt32();
                        break;
                    case nameof(BlockChunkPos.Z):
                        value.Z = reader.ReadInt32();
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return value;
        }
    }
}
