using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NorminalType = System.Collections.Generic.Dictionary<MineCase.Engine.DependencyProperty, System.Collections.Generic.SortedList<float, MineCase.Engine.Data.IEffectiveValue>>;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// 依赖值存储 - 序列化
    /// </summary>
    internal partial class DependencyValueStorage
    {
        internal class DependencyValueStorageSerializer : ClassSerializerBase<DependencyValueStorage>
        {
            protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, DependencyValueStorage value)
            {
                var writer = context.Writer;
                writer.WriteStartDocument();

                foreach (var keyPair in value._dict)
                {
                    writer.WriteName(PropertyToString(keyPair.Key));
                    SerializeValues(context, keyPair.Key, keyPair.Value);
                }

                writer.WriteEndDocument();
            }

            private void SerializeValues(BsonSerializationContext context, DependencyProperty key, SortedList<float, IEffectiveValue> values)
            {
                var writer = context.Writer;
                writer.WriteStartDocument();
                foreach (var valuePair in values)
                {
                    writer.WriteName(valuePair.Key.ToString());
                    var value = key.Helper.GetValue(valuePair.Value);
                    var serializer = BsonSerializer.LookupSerializer(key.PropertyType);
                    serializer.Serialize(context, value);
                }

                writer.WriteEndDocument();
            }

            protected override DependencyValueStorage DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
            {
                var dict = new Dictionary<DependencyProperty, SortedList<float, IEffectiveValue>>();
                var reader = context.Reader;
                reader.ReadStartDocument();

                while (reader.ReadBsonType() != BsonType.EndOfDocument)
                    DeserializeValues(context, dict);

                reader.ReadEndDocument();
                return new DependencyValueStorage(dict);
            }

            private void DeserializeValues(BsonDeserializationContext context, NorminalType dict)
            {
                var reader = context.Reader;
                var key = ParsePropertyFromString(reader.ReadName());
                dict.Add(key, DeserializeValues(key, context));
            }

            private SortedList<float, IEffectiveValue> DeserializeValues(DependencyProperty key, BsonDeserializationContext context)
            {
                var list = new SortedList<float, IEffectiveValue>();
                var serializer = BsonSerializer.LookupSerializer(key.PropertyType);
                var reader = context.Reader;
                reader.ReadStartDocument();

                while (reader.ReadBsonType() != BsonType.EndOfDocument)
                {
                    var priority = float.Parse(reader.ReadName());
                    var value = serializer.Deserialize(context);
                    list.Add(priority, key.Helper.FromValue(value));
                }

                reader.ReadEndDocument();
                return list;
            }

            private static string PropertyToString(DependencyProperty key) =>
                $"{DependencyProperty.OwnerTypeToString(key.OwnerType)}.{key.Name}";

            private static DependencyProperty ParsePropertyFromString(string name)
            {
                var lastPoint = name.LastIndexOf('.');
                var ownerType = DependencyProperty.StringToOwnerType(name.Substring(0, lastPoint));
                return DependencyProperty.FromName(name.Substring(lastPoint + 1), ownerType);
            }
        }
    }
}