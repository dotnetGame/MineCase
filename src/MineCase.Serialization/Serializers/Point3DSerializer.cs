using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Graphics;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Serialization.Serializers
{
    public class Point3dSerializer : StructSerializerBase<Point3d>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Point3d value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(Point3d.X));
            writer.WriteDouble(value.X);

            writer.WriteName(nameof(Point3d.Y));
            writer.WriteDouble(value.Y);

            writer.WriteName(nameof(Point3d.Z));
            writer.WriteDouble(value.Z);

            writer.WriteEndDocument();
        }

        public override Point3d Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var value = default(Point3d);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(Point3d.X):
                        value.X = (float)reader.ReadDouble();
                        break;
                    case nameof(Point3d.Y):
                        value.Y = (float)reader.ReadDouble();
                        break;
                    case nameof(Point3d.Z):
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
