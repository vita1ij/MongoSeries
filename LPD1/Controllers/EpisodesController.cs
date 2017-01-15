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
    public class EpisodesController : Controller
    {
        // GET: Episodes
        public ActionResult Index(String id)
        {
            var oid = new ObjectId(id);
            var ep = EpisodesDB.FindById(oid);
            var data = new FullEpisode()
            {
                _Episode = ep
            };

            data.Ratings = RatingsDB.FindByEpisodeId(oid);

            return View(data);
        }
    }
}