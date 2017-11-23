using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Graphics;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Serialization.Serializers
{
    public class CuboidSerializer : ClassSerializerBase<Cuboid>
    {
        private readonly IBsonSerializer<Point3d> _point3dSerializer = new Point3dSerializer();
        private readonly IBsonSerializer<Size> _sizeSerializer = new SizeSerializer();

        protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, Cuboid value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(Cuboid.Point));
            _point3dSerializer.Serialize(context, value.Point);

            writer.WriteName(nameof(Cuboid.Size));
            _sizeSerializer.Serialize(context, value.Size);

            writer.WriteEndDocument();
        }

        public override Cuboid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var point = default(Point3d);
            var size = default(Size);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(Cuboid.Point):
                        point = _point3dSerializer.Deserialize(context);
                        break;
                    case nameof(Cuboid.Size):
                        size = _sizeSerializer.Deserialize(context);
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return new Cuboid(point, size);
        }
    }
}
