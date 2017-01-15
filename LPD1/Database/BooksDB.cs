using LPD1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1.Database
{
    public class BooksDB
    {
        private static IMongoCollection<Book> collection
        {
            get
            {
                return Helper.GetTable<Book>("Books");
            }
        }

        public static List<Book> FindByTitle(string title)
        {
            var builder = Builders<Book>.Filter;
            var filter = Builders<Book>.Filter.Where(u => u.Title.ToLower().Contains(title));
            var result = collection.Find(filter);
            return result.ToList();
        }

        public static Book FindById(ObjectId id)
        {
            var builder = Builders<Book>.Filter;
            var filter = Builders<Book>.Filter.Where(u => u.Id == id);
            var result = collection.Find(filter);
            return result.First();
        }

        public static void Save(Book s)
        {
            collection.InsertOne(s);
        }
    }
}