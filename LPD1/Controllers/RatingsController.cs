using LPD1.Database;
using LPD1.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPD1.Controllers
{
    public class RatingsController : Controller
    {
        [HttpPost]
        public ActionResult Index(Rating data)
        {
            RatingsDB.Update(data);
            return View("Index", data);
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            ObjectId oid = new ObjectId(id);
            var series = SeriesDB.FindById(oid);

            ViewBag.OverallValues = Rating.OverallValues;
            ViewBag.Values = Rating.Values;

            var data = RatingsDB.FindById(oid);

            return View("Edit", data);
        }

        [HttpPost]
        public ActionResult Series(Rating data)
        {
            RatingsDB.Save(data);
            return View("Index", data);
        }

        [HttpGet]
        public ActionResult Series(string id)
        {
            ObjectId oid = new ObjectId(id);
            var series = SeriesDB.FindById(oid);

            var r = new Rating()
            {
                Title = series.Title,
                SeriesId = oid,
                UserId = new ObjectId(User.Identity.Name.Substring(0,24))
            };

            ViewBag.OverallValues = Rating.OverallValues;
            ViewBag.Values = Rating.Values;

            return View("Edit",r);
        }

        [HttpPost]
        public ActionResult Episode(Rating data)
        {
            RatingsDB.Save(data);
            return View("Index", data);
        }

        [HttpGet]
        public ActionResult Episode(string id)
        {
            ObjectId oid = new ObjectId(id);
            var episode = EpisodesDB.FindById(oid);
            var series = SeriesDB.FindById(episode.SeriesId);

            var r = new Rating()
            {
                Title = String.Format("[{0}] S{1}E{2}: {3}", series.Title, episode.Season, episode.Number, episode.Title),
                EpisodeId = oid,
                UserId = new ObjectId(User.Identity.Name.Substring(0, 24))
            };

            ViewBag.OverallValues = Rating.OverallValues;
            ViewBag.Values = Rating.Values;

            return View("Edit", r);
        }

        public ActionResult Edit(Rating data)
        {
            return View(data);
        }

    }
}