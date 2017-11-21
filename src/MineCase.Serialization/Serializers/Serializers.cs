using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Serialization.Serializers
{
    public static class Serializers
    {
        public static void RegisterAll()
        {
            BsonSerializer.RegisterSerializer(new SlotSerializer());
        }
    }
}
