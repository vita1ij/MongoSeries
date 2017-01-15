using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Web.Script.Serialization;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Serializers;

namespace LPD1.Database
{
    public class Helper
    {
        const string ConnectionString = "mongodb://localhost/?safe=true";

        public static IMongoDatabase Database
        {
            get
            {
                MongoClient client = new MongoClient(ConnectionString);
                return client.GetDatabase("SeriesWeekend");
            }
        }

        public static IMongoCollection<T> GetTable<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public  static void InsertObjectIntoTable<T>(T obj, string tableName)
        {
            var json = new JavaScriptSerializer().Serialize(obj);

            BsonDocument doc = null;

            using (var reader = new JsonReader(json))
            {
                var context = BsonDeserializationContext.CreateRoot(reader);
                doc = BsonDocumentSerializer.Instance.Deserialize(context);
            }

            var collection = GetTable<BsonDocument>(tableName);
            collection.InsertOne(doc);
        }

        

    }
}