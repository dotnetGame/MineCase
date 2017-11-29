using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Engine.Serialization;
using MineCase.Graphics;
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
        public DependencyProperty<Shape> ShapeProperty = DependencyProperty.Register<Shape>("Shape", typeof(SerializationTest));
        public DependencyProperty<Cuboid> CuboidProperty = DependencyProperty.Register<Cuboid>("Cuboid", typeof(SerializationTest));
        public DependencyProperty<StateHolder> StateProperty = DependencyProperty.Register<StateHolder>("State", typeof(SerializationTest));

        public class Pair
        {
            public int Int { get; set; }

            public Shape Collider { get; set; }
        }

        public class StateHolder
        {
            public List<Pair> Shape { get; set; }
        }

        public SerializationTest()
        {
            Serializers.RegisterAll();
        }

        [Fact]
        public async Task Test1()
        {
            var slot = new Slot { BlockId = 1, ItemDamage = 2, ItemCount = 3 };
            var shape = new Cuboid(new Point3d(0, 1, 2), new Size(10, 20, 30));
            var state = new StateHolder { Shape = new List<Pair> { new Pair { Collider = shape } } };

            var entity = new TestEntity();
            await entity.ReadStateAsync();
            entity.SetCurrentValue(SlotProperty, slot);
            entity.SetCurrentValue(ShapeProperty, shape);
            entity.SetCurrentValue(CuboidProperty, shape);
            entity.SetCurrentValue(StateProperty, state);

            var doc = Serialize(entity);

            Assert.Equal(2, doc.ElementCount);
            var vs = doc.GetElement(1);
            Assert.Equal("ValueStorage", vs.Name);
            var vsv = (BsonDocument)vs.Value;
            Assert.Equal(4, vsv.ElementCount);

            entity = new TestEntity();
            entity.BsonDocument = doc;
            await entity.ReadStateAsync();
            Assert.Equal(4, entity.ValueStorage.Keys.Count());
            Assert.Equal(slot, entity.GetValue(SlotProperty));
            Assert.Equal(shape, entity.GetValue(ShapeProperty));
            Assert.Equal(shape, entity.GetValue(CuboidProperty));
            Assert.Equal(shape, entity.GetValue(StateProperty).Shape[0].Collider);
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
