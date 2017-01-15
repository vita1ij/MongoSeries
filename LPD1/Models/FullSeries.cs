using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPD1.Models
{
    public class FullSeries
    {
        public Series _Series;

        public List<Episode> Episodes;

        public List<Rating> Ratings;

        public bool IsFavourite;

        public bool showBooks = false;

        public List<SelectListItem> Books;

        public string BasedOnBook;
    }
}