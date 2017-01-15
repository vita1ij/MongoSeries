using LPD1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1.Database
{
    public class FavouritesDB
    {
        private static IMongoCollection<Favourite> collection
        {
            get
            {
                return Helper.GetTable<Favourite>("Favourites");
            }
        }

        public static bool IsFavourite(ObjectId user, ObjectId series)
        {
            var builder = Builders<Favourite>.Filter;
            var filter = Builders<Favourite>.Filter.Eq("UserId", user) & Builders<Favourite>.Filter.Eq("SeriesId", series);
            var result = collection.Find(filter);

            return result.Any();
        }

        public static List<Favourite> FindByUserId(ObjectId user)
        {
            var builder = Builders<Favourite>.Filter;
            var filter = Builders<Favourite>.Filter.Eq("UserId", user);
            var result = collection.Find(filter);

            return result.ToList();
        }

        public static void Save(Favourite s)
        {
            collection.InsertOne(s);
        }


        public static void Remove(ObjectId user, ObjectId series)
        {
            var builder = Builders<Favourite>.Filter;
            var filter = Builders<Favourite>.Filter.Eq("UserId", user) & Builders<Favourite>.Filter.Eq("SeriesId", series);
            collection.DeleteOne(filter);
        }
    }
}