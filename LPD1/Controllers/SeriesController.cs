using LPD1.Database;
using LPD1.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LPD1.Controllers
{
    public class SeriesController : Controller
    {
        private const string OMDBAPIstring = "http://www.omdbapi.com/?t={0}&y=&plot=full&r=json";
        private const string MazeAPISeriesString = "http://api.tvmaze.com/search/shows?q={0}";
        private const string MazeAPIEpisodesString = "http://api.tvmaze.com/shows/{0}/episodes";

        public bool isShowBooks = false;

        public ActionResult UserSeries(int? pageNumber)
        {
            if (pageNumber == null) pageNumber = 1;

            var userId = new ObjectId(User.Identity.Name.Substring(0, 24));
            var data = SeriesDB.FindByUserId(userId);

            return View("Index", data.ToPagedList(pageNumber.Value, 10));
        }

        public ActionResult AddTF(string id)
        {
            FavouritesDB.Save(new Favourite() { SeriesId = new ObjectId(id), UserId = new ObjectId(User.Identity.Name.Substring(0, 24)) });
            //RedirectToAction("Details", "Series", new { id = id });

            return RedirectToAction("Details", new RouteValueDictionary(
                new { controller = "Series", action = "Details", id = id }));
        }

        public ActionResult RemoveFF(string id)
        {
            FavouritesDB.Remove(new ObjectId(User.Identity.Name.Substring(0, 24)), new ObjectId(id));
            //RedirectToAction("Details", "Series", new { id = id });

            return RedirectToAction("Details", new RouteValueDictionary(
                new { controller = "Series", action = "Details", id = id }));
        }

        // GET: Series
        public ActionResult Index(int? pageNumber)
        {
            if (pageNumber == null) pageNumber = 1;
            long count = SeriesDB.CountAll();
            //List<Series> data = SeriesDB.FindForPage(pageNumber.Value);
            List<Series> data = SeriesDB.FindAll();

            return View(data.ToPagedList(pageNumber.Value, 10));
        }

        // GET: Series/Details/5
        public ActionResult Details(String id)
        {
            ObjectId oid = new ObjectId(id);
            Series data = SeriesDB.FindById(oid);

            //request for books
            //get book - null

            return View(GetFullSeries(data));
        }

        [HttpPost]
        public ActionResult Search(string Title)
        {
            var results = SeriesDB.FindByTitle(Title);
            if (results.Count == 1)
            {
                return View("Details", GetFullSeries(results[0]));
            }
            else if (results.Any())
            {
                //todo[vg]:Open /series/index
            }
            else
            {
                //Search

                string responseFromServer = GetJson(String.Format(OMDBAPIstring, Title));
                Series s = GetSeriesFromOMDBJson(responseFromServer);
                if (s == null)
                {
                    return RedirectToAction("Index", "Series", null);
                }
                SeriesDB.Save(s);

                List<Episode> episodes = GetEpisodesForSeries(s.Title, s._id);
                EpisodesDB.SaveAll(episodes);

                return View("Details", GetFullSeries(s));
            }
            return View("Details", GetFullSeries(results[0]));
        }

        private string GetJson(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.
            var dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            return reader.ReadToEnd();
        }

        public ActionResult AddBook(ObjectId id)
        {
            return RedirectToAction("List", new RouteValueDictionary(
                new { controller = "Books", action = "List", id = id }));
        }

        private List<Episode> GetEpisodesForSeries(string title, ObjectId id)
        {
            List<Episode> result = new List<Episode>();
            //get Series Id
            string json = GetJson(string.Format(MazeAPISeriesString, title));

            dynamic jo = JsonConvert.DeserializeObject(json);

            if (jo != null && (Newtonsoft.Json.Linq.JArray)jo != null && ((Newtonsoft.Json.Linq.JArray)jo).Any())
            {
                string seriesId = ((Newtonsoft.Json.Linq.JArray)jo).Children().First()["show"]["id"].ToString();

                json = GetJson(string.Format(MazeAPIEpisodesString, seriesId));
                jo = JsonConvert.DeserializeObject(json);

                //parse Episodes
                foreach (var ep in ((Newtonsoft.Json.Linq.JArray)jo).Children())
                {
                    Episode e = new Episode();
                    e.Title = ep["name"].ToString();
                    e.Number = Int32.Parse(ep["number"].ToString());
                    e.Season = Int32.Parse(ep["season"].ToString());
                    e.Description = ep["summary"].ToString();
                    e.SeriesId = id;

                    result.Add(e);
                }

            }
            return result;
        }

        private Series GetSeriesFromOMDBJson(string json)
        {
            dynamic jo = JsonConvert.DeserializeObject(json);
            string responce = (string)jo["Response"];

            if (responce != "True")
            {
                return null;
            }

            Series result = new Series();

            //Fill data
            result.Title = (string)jo["Title"];
            result.Genres = (string)jo["Genre"];
            result.Director = (string)jo["Director"];
            result.Writer = (string)jo["Writer"];
            result.Actors = (string)jo["Actors"];
            result.ShortDescription = result.Desctiption = (string)jo["Plot"];
            result.ImageLink = (string)jo["Poster"];
            if (jo["imdbRating"] == "N/A")
            {
                result.ImdbRank = null;
            }
            else
            {
                result.ImdbRank = (float)jo["imdbRating"];
            }
            result.LastUpdated = DateTime.Now;



            return result;
        }

        private FullSeries GetFullSeries(Series s, String bookTitle = null)
        {
            var result = new FullSeries()
            {
                _Series = s
            };

            if (s.BasedOnBook.HasValue)
            {
                result.BasedOnBook = BooksDB.FindById(s.BasedOnBook.Value).Title;
            }

            List<Episode> episodes = EpisodesDB.FindBySeriesId(s._id);

            var seasons = episodes.Select(x => x.Season).Distinct().OrderBy(x => x);

            result.Episodes = EpisodesDB.FindBySeriesId(s._id);

            result.Ratings = RatingsDB.FindBySeriesId(s._id);

            result.IsFavourite = System.Web.HttpContext.Current.User.Identity.IsAuthenticated ?
                FavouritesDB.IsFavourite(new ObjectId(System.Web.HttpContext.Current.User.Identity.Name.ToString().Substring(0, 24)), s._id)
                : false;

            if (bookTitle != null)
            {
                result.Books = BooksDB.FindByTitle(bookTitle)
                .Select<Book,SelectListItem>(x => (new SelectListItem() { Text = x.Title, Value = x.Id.ToString() })).ToList();
            }

            return result;
        }

        public ActionResult ShowBooks(String id)
        {
            ObjectId oid = new ObjectId(id);
            Series data = SeriesDB.FindById(oid);
            FullSeries series = GetFullSeries(data, "");

            ViewBag.Books = series.Books;

            series.showBooks = true;
            return View("Details", series);
        }

        [HttpPost]
        public ActionResult SearchBook(string Title, string Id)
        {
            ObjectId oid = new ObjectId(Id);
            Series data = SeriesDB.FindById(oid);
            FullSeries series = GetFullSeries(data, Title);

            ViewBag.Books = series.Books;

            series.showBooks = true;
            return View("Details", series);
        }

        [HttpPost]
        public ActionResult AddBookToSeries(string Id)
        {
            string sid = Request.Form["Books"];

            ObjectId oid = new ObjectId(Id);
            Series data = SeriesDB.FindById(oid);
            FullSeries series = GetFullSeries(data, "");

            if (string.IsNullOrEmpty(sid))
            {
                ViewBag.Books = series.Books;

                series.showBooks = true;
                return View("Details", series);
            }
            else
            {
                SeriesDB.AddBook(new ObjectId(Id), new ObjectId(sid));

                series.showBooks = false;
                return View("Details", series);
            }
        }

        public JsonResult GetAllSeries()
        {
            return Json(SeriesDB.FindAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSeries(string id)
        {
            ObjectId oid = new ObjectId(id);
            return Json(SeriesDB.FindById(oid), JsonRequestBehavior.AllowGet);
        }
    }
}
