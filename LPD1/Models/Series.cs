using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LPD1.Models
{
    public class Series
    {

        //Id
        public ObjectId _id { get; set; }
        //Title
        public string Title { get; set; }
        //Imdb Rank
        public float? ImdbRank { get; set; }
        //Local Rank
        public float LocalRank { get; set; }
        public long RankCount { get; set; }
        //Short Description
        public string ShortDescription { get; set; }
        //Description
        public string Desctiption { get; set; }
        //Genre
        public String Genres { get; set; }
        //Poster
        public string ImageLink { get; set; }
        //Director n stuff
        public string Director { get; set; }

        public string Writer { get; set; }

        public string Actors { get; set; }
        //LastUpdated
        public DateTime LastUpdated { get; set; }
        
        public ObjectId? BasedOnBook { get; set; }

        public static Series Stub(ObjectId id)
        {
            return new Series()
            {
                _id = id,
                Title = "Breaking Bad",
                ImdbRank = 9,
                LocalRank = 10,
                ShortDescription = "ASD",
                Desctiption = "qwe",
                Genres = "Drama",
                ImageLink = "https://lh3.googleusercontent.com/-5wnq0z86NCc/TtU1oB59fzI/AAAAAAAAAPc/CardefKs2NsBeYsuU5Y6IeL-0jsvYx-1A/s281-p/Screen%2Bshot%2B2011-11-29%2Bat%2B9.36.14%2BAM.png",
                Director = "zxc"
            };
        }

        public static List<Series> Stubs(int cnt)
        {
            List<Series> result = new List<Series>();
            for (int i = 0; i < cnt; i++, result.Add(Stub(new ObjectId(DateTime.Now,42,1,i))));
            return result;
        }
    }
}