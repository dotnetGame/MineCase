using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Graphics;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Serialization.Serializers
{
    public class SizeSerializer : StructSerializerBase<Size>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Size value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(Size.Width));
            writer.WriteDouble(value.Width);

            writer.WriteName(nameof(Size.Height));
            writer.WriteDouble(value.Height);

            writer.WriteName(nameof(Size.Length));
            writer.WriteDouble(value.Length);

            writer.WriteEndDocument();
        }

        public override Size Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(Size);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(Size.Width):
                        value.Width = (float)reader.ReadDouble();
                        break;
                    case nameof(Size.Height):
                        value.Height = (float)reader.ReadDouble();
                        break;
                    case nameof(Size.Length):
                        value.Length = (float)reader.ReadDouble();
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
