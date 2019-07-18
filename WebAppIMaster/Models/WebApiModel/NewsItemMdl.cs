using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.WebApiModel
{
    public class NewsItemMdl
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int ViewCount { get; set; }
        public string PhotoFile { get; set; }
        public string PhotoFileType { get; set; }
    }

    public class PaginationNewsItemMdl
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int ViewCount { get; set; }
        public string PhotoFile { get; set; }
        public string PhotoFileType { get; set; }
    }
}