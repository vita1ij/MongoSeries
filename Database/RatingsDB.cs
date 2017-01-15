using LPD1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1.Database
{
    public class RatingsDB
    {
        private static IMongoCollection<Rating> collection
        {
            get
            {
                return Helper.GetTable<Rating>("Ratings");
            }
        }

        internal static void Update(Rating data)
        {
            var builder = Builders<Rating>.Filter;
            var filter = Builders<Rating>.Filter.Eq("_id", data._id);
            collection.ReplaceOne(filter,data);
        }

        public static void Save(Rating s)
        {
            collection.InsertOne(s);
        }

        public static Rating FindById(ObjectId id)
        {
            var builder = Builders<Rating>.Filter;
            var filter = Builders<Rating>.Filter.Eq("_id", id);
            var result = collection.Find(filter);
            return result.FirstOrDefault();
        }

        public static List<Rating> FindBySeriesId(ObjectId id)
        {
            var builder = Builders<Rating>.Filter;
            var filter = Builders<Rating>.Filter.Eq("SeriesId", id);
            var result = collection.Find(filter);
            return result.ToList();
        }

        public static List<Rating> FindByEpisodeId(ObjectId id)
        {
            var builder = Builders<Rating>.Filter;
            var filter = Builders<Rating>.Filter.Eq("EpisodeId", id);
            var result = collection.Find(filter);
            return result.ToList();
        }
    }
}