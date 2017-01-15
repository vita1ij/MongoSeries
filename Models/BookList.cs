using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPD1.Models
{
    public class BookList
    {
        public string SeriesTitle { get; set; }

        public List<Book> Books { get; set; }
    }
}