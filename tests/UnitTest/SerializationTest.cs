using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Engine.Serialization;
using MineCase.Serialization.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Xunit;

namespace MineCase.UnitTest
{
    public class SerializationTest
    {
        public DependencyProperty<Slot> SlotProperty = DependencyProperty.Register<Slot>("Slot", typeof(SerializationTest), new PropertyMetadata<Slot>(Slot.Empty));

        public SerializationTest()
        {
            Serializers.RegisterAll();
        }

        [Fact]
        public async Task Test1()
        {
            var slot = new Slot { BlockId = 1, ItemDamage = 2, ItemCount = 3 };

            var entity = new TestEntity();
            await entity.OnActivateAsync();
            await entity.SetCurrentValue(SlotProperty, slot);

            var doc = Serialize(entity);

            Assert.Equal(2, doc.ElementCount);
            var vs = doc.GetElement(1);
            Assert.Equal("ValueStorage", vs.Name);
            var vsv = (BsonDocument)vs.Value;
            Assert.Equal(1, vsv.ElementCount);

            entity = new TestEntity();
            entity.BsonDocument = doc;
            await entity.OnActivateAsync();
            Assert.Single(entity.ValueStorage.Keys);
            Assert.Equal(slot, entity.GetValue(SlotProperty));
        }

        private BsonDocument Serialize(TestEntity entity)
        {
            var doc = new BsonDocument();
            var writer = new BsonDocumentWriter(doc);
            var context = BsonSerializationContext.CreateRoot(writer);
            var serializer = new DependencyObjectStateSerializer();
            serializer.Serialize(context, new DependencyObjectState
            {
                GrainKeyString = "test",
                ValueStorage = entity.ValueStorage
            });
            return doc;
        }

        internal class TestEntity : DependencyObject
        {
            public BsonDocument BsonDocument;

            protected override Task<DependencyObjectState> DeserializeStateAsync()
            {
                if (BsonDocument == null) return base.DeserializeStateAsync();
                var reader = new BsonDocumentReader(BsonDocument);
                var serializer = new DependencyObjectStateSerializer();
                var context = BsonDeserializationContext.CreateRoot(reader);
                return Task.FromResult(serializer.Deserialize(context));
            }
        }
    }
}
