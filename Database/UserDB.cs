using LPD1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LPD1.Database
{
    public class UserDB
    {
        public static bool Login(string login, string password)
        {
            var collection = Helper.GetTable<UserInfo>("Users");
            var hash = GetHashString(password);
            var builder = Builders<UserInfo>.Filter;
            var filter = builder.Eq("Login", login) & builder.Eq("Password", hash);
            var result = collection.Find(filter);

            return result.Any();
        }

        public static bool Exists(string login)
        {
            var collection = Helper.GetTable<UserInfo>("Users");
            var builder = Builders<UserInfo>.Filter;
            var filter = builder.Eq("Login", login);
            var result = collection.Find(filter);
            return result.Any();
        }

        public static bool Register(UserInfo userInfo)
        {
            //var collection = Helper.GetTable<UserInfo>("Users");
            try
            {
                var hash = GetHashString(userInfo.Password);
                userInfo.Password = hash;
                var collection = Helper.GetTable<UserInfo>("Users");
                collection.InsertOne(userInfo);
                //Helper.InsertObjectIntoTable<UserInfo>(userInfo, "Users");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static UserInfo FindByLogin(string login)
        {
            var collection = Helper.GetTable<UserInfo>("Users");
            var builder = Builders<UserInfo>.Filter;
            var filter = Builders<UserInfo>.Filter.Regex("Login", new BsonRegularExpression(login));
            var result = collection.Find(filter);
            return result.FirstOrDefault();
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        
    }
}