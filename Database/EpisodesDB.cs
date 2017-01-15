using LPD1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1.Database
{
    public class EpisodesDB
    {
        private static IMongoCollection<Episode> collection
        {
            get
            {
                return Helper.GetTable<Episode>("Episodes");
            }
        }

        public static void Save(Episode s)
        {
            collection.InsertOne(s);
        }

        public static void SaveAll(List<Episode> s)
        {
            collection.InsertMany(s);
        }

        public static Episode FindById(ObjectId id)
        {
            var builder = Builders<Episode>.Filter;
            var filter = Builders<Episode>.Filter.Eq("_id", id);
            var result = collection.Find(filter);
            return result.FirstOrDefault();
        }

        public static List<Episode> FindBySeriesId(ObjectId id)
        {
            var builder = Builders<Episode>.Filter;
            var filter = Builders<Episode>.Filter.Eq("SeriesId", id);
            var result = collection.Find(filter);
            return result.ToList();
        }
    }
}