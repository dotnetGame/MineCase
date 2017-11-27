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

    public class BlockWorldPosSerializer : StructSerializerBase<BlockWorldPos>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BlockWorldPos value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(BlockWorldPos.X));
            writer.WriteInt32(value.X);

            writer.WriteName(nameof(BlockWorldPos.Y));
            writer.WriteInt32(value.Y);

            writer.WriteName(nameof(BlockWorldPos.Z));
            writer.WriteInt32(value.Z);

            writer.WriteEndDocument();
        }

        public override BlockWorldPos Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(BlockWorldPos);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(BlockWorldPos.X):
                        value.X = reader.ReadInt32();
                        break;
                    case nameof(BlockWorldPos.Y):
                        value.Y = reader.ReadInt32();
                        break;
                    case nameof(BlockWorldPos.Z):
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

    public class BlockSectionPosSerializer : StructSerializerBase<BlockSectionPos>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BlockSectionPos value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(BlockSectionPos.X));
            writer.WriteInt32(value.X);

            writer.WriteName(nameof(BlockSectionPos.Y));
            writer.WriteInt32(value.Y);

            writer.WriteName(nameof(BlockSectionPos.Z));
            writer.WriteInt32(value.Z);

            writer.WriteEndDocument();
        }

        public override BlockSectionPos Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(BlockSectionPos);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(BlockSectionPos.X):
                        value.X = reader.ReadInt32();
                        break;
                    case nameof(BlockSectionPos.Y):
                        value.Y = reader.ReadInt32();
                        break;
                    case nameof(BlockSectionPos.Z):
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

    public class ChunkWorldPosSerializer : StructSerializerBase<ChunkWorldPos>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ChunkWorldPos value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(ChunkWorldPos.X));
            writer.WriteInt32(value.X);

            writer.WriteName(nameof(ChunkWorldPos.Z));
            writer.WriteInt32(value.Z);

            writer.WriteEndDocument();
        }

        public override ChunkWorldPos Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(ChunkWorldPos);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(ChunkWorldPos.X):
                        value.X = reader.ReadInt32();
                        break;
                    case nameof(ChunkWorldPos.Z):
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

    public class EntityWorldPosSerializer : StructSerializerBase<EntityWorldPos>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, EntityWorldPos value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(EntityWorldPos.X));
            writer.WriteDouble(value.X);

            writer.WriteName(nameof(EntityWorldPos.Y));
            writer.WriteDouble(value.Y);

            writer.WriteName(nameof(EntityWorldPos.Z));
            writer.WriteDouble(value.Z);

            writer.WriteEndDocument();
        }

        public override EntityWorldPos Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(EntityWorldPos);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(EntityWorldPos.X):
                        value.X = (float)reader.ReadDouble();
                        break;
                    case nameof(EntityWorldPos.Y):
                        value.Y = (float)reader.ReadDouble();
                        break;
                    case nameof(EntityWorldPos.Z):
                        value.Z = (float)reader.ReadDouble();
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return value;
        }
    }

    public class EntityChunkPosSerializer : StructSerializerBase<EntityChunkPos>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, EntityChunkPos value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(EntityChunkPos.X));
            writer.WriteDouble(value.X);

            writer.WriteName(nameof(EntityChunkPos.Y));
            writer.WriteDouble(value.Y);

            writer.WriteName(nameof(EntityChunkPos.Z));
            writer.WriteDouble(value.Z);

            writer.WriteEndDocument();
        }

        public override EntityChunkPos Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(EntityChunkPos);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(EntityChunkPos.X):
                        value.X = (float)reader.ReadDouble();
                        break;
                    case nameof(EntityChunkPos.Y):
                        value.Y = (float)reader.ReadDouble();
                        break;
                    case nameof(EntityChunkPos.Z):
                        value.Z = (float)reader.ReadDouble();
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
