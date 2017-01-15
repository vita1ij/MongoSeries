using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPD1.Models
{
    public class Rating
    {
        //Id
        public ObjectId _id { get; set; }

        public string Title { get; set; }

        //SeriesId
        public ObjectId SeriesId { get; set; }

        //EpisodeId
        public ObjectId EpisodeId { get; set; }

        //UserId
        public ObjectId UserId { get; set; }

        //Rank/3 (Positive/Neutral/Negative)
        public int OverallValue { get; set; }

        //Rank/10
        public int Value { get; set; }

        //Description
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public static List<SelectListItem> OverallValues
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Negative", Value = "-1"},
                    new SelectListItem { Text = "Neutral", Value = "0"},
                    new SelectListItem { Text = "Positive", Value = "1"},
                };
            }
        }

        public static List<SelectListItem> Values
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "1", Value = "1"},
                    new SelectListItem { Text = "2", Value = "2"},
                    new SelectListItem { Text = "3", Value = "3"},
                    new SelectListItem { Text = "4", Value = "4"},
                    new SelectListItem { Text = "5", Value = "5"},
                    new SelectListItem { Text = "6", Value = "6"},
                    new SelectListItem { Text = "7", Value = "7"},
                    new SelectListItem { Text = "8", Value = "8"},
                    new SelectListItem { Text = "9", Value = "9"},
                    new SelectListItem { Text = "10", Value = "10"},
                };
            }
        }
    }
}