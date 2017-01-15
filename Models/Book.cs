using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LPD1.Models
{
    public class Book
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String Type { get; set; }

        [Required]
        public String Language { get; set; }

        [Required]
        public ObjectId Pub_Id { get; set; }

        public Nullable<decimal> Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime Pub_Date { get; set; }

        [StringLength(200)]
        public String Notes { get; set; }

        public List<ObjectId> Authors { get; set; }

        public static Book Stub()
        {
            return new Book()
            {
                Title = "Song of Ice and Fire",
                Type = "Fantasy, Drama",
                Language = "English",
                Pub_Id = new ObjectId(),
                Price = ((decimal?)15.89),
                Pub_Date = new DateTime(1998, 5, 23),
                Notes = "Lorem ipsum",
                Authors = new List<ObjectId>()
            };
        }
    }
}