using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// “¿¿µ÷µ¥Ê¥¢ - –Ú¡–ªØ
    /// </summary>
    internal partial class DependencyValueStorage
    {
        public void Serialize(BsonDocument document)
        {
            foreach (var keyPair in _dict)
                document.Add(PropertyToString(keyPair.Key), SerializeValues(keyPair.Key.PropertyType, keyPair.Value));
        }

        private static BsonValue SerializeValues(Type valueType, SortedList<float, IEffectiveValue> values)
        {
            var doc = new BsonDocument();
            foreach (var valuePair in values)
                doc.Add(valuePair.Key.ToString(), SerializeValue(valueType, valuePair.Value));
            return doc;
        }

        private static BsonValue SerializeValue(Type valueType, IEffectiveValue value)
        {
            var serializer = BsonSerializer.LookupSerializer(valueType);
            return serializer.ToBsonValue(value);
        }

        private static string PropertyToString(DependencyProperty key) =>
            $"{DependencyProperty.OwnerTypeToString(key.OwnerType)}.{key.Name}";

        public void Deserialize(BsonDocument document)
        {
            foreach (var keyPair in document.Elements)
            {
                var property = ParsePropertyFromString(keyPair.Name);
                DeserializeValues(property, keyPair.Value.AsBsonDocument);
            }
        }

        private void DeserializeValues(DependencyProperty property, BsonDocument values)
        {
            var container = new SortedList<float, IEffectiveValue>(values.ElementCount);
            _dict.Add(property, container);
            foreach (var value in values)
                DeserializeValue(container, value);
        }

        private static void DeserializeValue(SortedList<float, IEffectiveValue> container, BsonElement value)
        {
        }

        private static DependencyProperty ParsePropertyFromString(string name)
        {
            var lastPoint = name.LastIndexOf('.');
            var ownerType = DependencyProperty.StringToOwnerType(name.Substring(0, lastPoint));
            return DependencyProperty.FromName(name.Substring(lastPoint + 1), ownerType);
        }
    }
}