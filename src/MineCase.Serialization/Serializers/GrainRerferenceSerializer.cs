using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Serialization.Serializers
{
    public class GrainRerferenceSerializer<TInterface> : SealedClassSerializerBase<TInterface>
        where TInterface : class, IAddressable
    {
        private IGrainReferenceConverter _grainReferenceConverter;
        private IGrainFactory _grainFactory;

        public GrainRerferenceSerializer(IServiceProvider serviceProvider)
        {
            _grainReferenceConverter = serviceProvider.GetRequiredService<IGrainReferenceConverter>();
            _grainFactory = serviceProvider.GetRequiredService<IGrainFactory>();
        }

        protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, TInterface value)
        {
            var refer = (GrainReference)(object)value;
            var key = refer.ToKeyString();
            context.Writer.WriteString(key);
        }

        protected override TInterface DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var key = context.Reader.ReadString();
            var refer = _grainReferenceConverter.GetGrainFromKeyString(key);
            if (refer != null)
            {
                refer.BindGrainReference(_grainFactory);
                return refer.AsReference<TInterface>();
            }

            return null;
        }
    }

    public class GrainRerferenceSerializerProvider : IBsonSerializationProvider
    {
        private readonly ConcurrentDictionary<Type, IBsonSerializer> _bsonSerializers = new ConcurrentDictionary<Type, IBsonSerializer>();
        private readonly Type _referType = typeof(IAddressable);
        private readonly IServiceProvider _serviceProvider;
        private readonly Type _serializerTypeGen = typeof(GrainRerferenceSerializer<>);

        public GrainRerferenceSerializerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            if (_referType.IsAssignableFrom(type))
                return _bsonSerializers.GetOrAdd(type, t => (IBsonSerializer)Activator.CreateInstance(_serializerTypeGen.MakeGenericType(t), _serviceProvider));
            return null;
        }
    }
}
