using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1.Models
{
    public class Favourite
    {
        public ObjectId _id;

        public ObjectId SeriesId;

        public ObjectId UserId;
    }
}