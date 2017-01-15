using LPD1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using PagedList;

namespace LPD1.Database
{
    public static class SeriesDB
    {
        private static IMongoCollection<Series> collection
        {
            get
            {
                return Helper.GetTable<Series>("Series");
            }
        }

        public static List<Series> FindByTitle(string title)
        {
            var builder = Builders<Series>.Filter;
            var filter = Builders<Series>.Filter.Where(u => u.Title.ToLower().Contains(title)); 
            //.Regex("Title", new BsonRegularExpression(title));
            var result = collection.Find(filter);
            return result.ToList();
        }

        public static long CountAll()
        {
            var query = collection.Find(x => true);
            return query.Count();
        }

        public static List<Series> FindByUserId(ObjectId userId)
        {
            var favourites = FavouritesDB.FindByUserId(userId);
            var ids = favourites.Select(x => x.SeriesId);

            var builder = Builders<Series>.Filter;
            var filter = Builders<Series>.Filter.Where(u => ids.Contains(u._id));

            var result = collection.Find(filter);
            return result.ToList();
        }

        public static List<Series> FindForPage(int pageNumber)
        {
            var query = collection.Find(x => true).Sort(Builders<Series>.Sort.Descending("_id"));
            var totalTask = query.Count();
            return query.Skip((pageNumber-1)*10).Limit(10).ToList();
        }

        public static List<Series> FindAll()
        {
            var query = collection.Find(x => true).Sort(Builders<Series>.Sort.Descending("_id"));
            return query.ToList();
        }

        public static Series FindById(ObjectId id)
        {
            var builder = Builders<Series>.Filter;
            var filter = Builders<Series>.Filter.Eq("_id", id);
            var result = collection.Find(filter);
            return result.FirstOrDefault();
        }

        public static void AddBook(ObjectId seriesId, ObjectId bookId)
        {
            var filter = Builders<Series>.Filter.Eq("_id", seriesId);
            var update = Builders<Series>.Update.Set("BasedOnBook", bookId);

            collection.UpdateOne(
                filter,
                update
                );
        }

        public static void Save(Series s)
        {
            collection.InsertOne(s);
        }

    }
}