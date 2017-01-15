using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1
{
    public class DatabaseHelper
    {
        private static string ConnectionString
        {
            get
            {
                return "mongodb://localhost";
            }
        }

        public MongoClient Client;
        //public MongoServer Server;

        //public static DatabaseHelper Instance
        //{
        //    //get
        //    //    {
        //    //    DatabaseHelper result = new DatabaseHelper();
        //    //    // Create a MongoClient object by using the connection string
        //    //    result.Client = new MongoClient(DatabaseHelper.ConnectionString);

        //    //    //Use the MongoClient to access the server
        //    //    result.Server = result.Client.GetServer();

        //    //    return result;

        //    //    }
        //}
    }
}