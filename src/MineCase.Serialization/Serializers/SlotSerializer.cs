using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;

namespace MineCase.Serialization.Serializers
{
    public class SlotSerializer : StructSerializerBase<Slot>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Slot value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(Slot.BlockId));
            writer.WriteInt32(value.BlockId);

            writer.WriteName(nameof(Slot.ItemCount));
            writer.WriteInt32(value.ItemCount);

            writer.WriteName(nameof(Slot.ItemDamage));
            writer.WriteInt32(value.ItemDamage);

            writer.WriteEndDocument();
        }

        public override Slot Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var slot = default(Slot);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(Slot.BlockId):
                        slot.BlockId = (short)reader.ReadInt32();
                        break;
                    case nameof(Slot.ItemCount):
                        slot.ItemCount = (byte)reader.ReadInt32();
                        break;
                    case nameof(Slot.ItemDamage):
                        slot.ItemDamage = (short)reader.ReadInt32();
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return slot;
        }
    }
}
