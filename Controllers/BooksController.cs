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
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            return View("Index", Book.Stub());
        }

        public ActionResult Add4Series(ObjectId seriesId)
        {


            return null;
        }

        public ActionResult Details(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book data = BooksDB.FindById(bookId);

            return View("Details", data);
        }

    }
}