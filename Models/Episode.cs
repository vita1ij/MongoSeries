using MongoDB.Bson;

namespace LPD1.Models
{
    public class Episode
    {
        public BsonObjectId _id { get; set; }

        //Number
        public int Number { get; set; }

        //Season
        public int Season { get; set; }

        //Title
        public string Title { get; set; }

        //Description
        public string Description { get; set; }

        //Rank
        public float Rank { get; set; }

        public long RankCount { get; set; }

        //Series
        public ObjectId SeriesId { get; internal set; }
    }
}