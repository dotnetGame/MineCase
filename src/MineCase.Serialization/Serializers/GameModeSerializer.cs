using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;

namespace MineCase.Serialization.Serializers
{
    public class GameModeSerializer : StructSerializerBase<GameMode>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GameMode value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName(nameof(GameMode.ModeClass));
            writer.WriteInt32((byte)value.ModeClass);

            writer.WriteName(nameof(GameMode.IsHardcore));
            writer.WriteBoolean(value.IsHardcore);

            writer.WriteEndDocument();
        }

        public override GameMode Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartDocument();

            var gameMode = default(GameMode);
            while (reader.ReadBsonType() != MongoDB.Bson.BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case nameof(GameMode.ModeClass):
                        gameMode.ModeClass = (GameMode.Class)reader.ReadInt32();
                        break;
                    case nameof(GameMode.IsHardcore):
                        gameMode.IsHardcore = reader.ReadBoolean();
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return gameMode;
        }
    }
}
