using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace MineCase.Engine.Serialization
{
    public class DependencyObjectState
    {
        [BsonId]
        public string GrainKeyString { get; set; }

        public IDependencyValueStorage ValueStorage { get; set; }
    }

    /// <summary>
    /// DependencyObject 状态序列化器
    /// </summary>
    public class DependencyObjectStateSerializer : ClassSerializerBase<DependencyObjectState>, IBsonDocumentSerializer
    {
        private readonly IBsonSerializer<DependencyValueStorage> _valueStorageSerializer = new DependencyValueStorage.DependencyValueStorageSerializer();

        protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, DependencyObjectState value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();

            writer.WriteName("_id");
            writer.WriteString(value.GrainKeyString);

            writer.WriteName("ValueStorage");
            _valueStorageSerializer.Serialize(context, value.ValueStorage);

            writer.WriteEndDocument();
        }

        protected override DependencyObjectState DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var state = new DependencyObjectState();
            var reader = context.Reader;
            reader.ReadStartDocument();

            while (reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                switch (name)
                {
                    case "_id":
                        state.GrainKeyString = reader.ReadString();
                        break;
                    case "ValueStorage":
                        state.ValueStorage = _valueStorageSerializer.Deserialize(context);
                        break;
                    default:
                        break;
                }
            }

            reader.ReadEndDocument();
            return state;
        }

        public bool TryGetMemberSerializationInfo(string memberName, out BsonSerializationInfo serializationInfo)
        {
            switch (memberName)
            {
                case "GrainKeyString":
                    serializationInfo = new BsonSerializationInfo("_id", new StringSerializer(), typeof(string));
                    return true;
                case "ValueStorage":
                    serializationInfo = new BsonSerializationInfo("ValueStorage", _valueStorageSerializer, typeof(DependencyValueStorage));
                    return true;
                default:
                    serializationInfo = null;
                    return false;
            }
        }
    }
}
