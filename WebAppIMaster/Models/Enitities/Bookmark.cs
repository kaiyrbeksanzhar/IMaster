using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class Bookmark
    {
        public int Id { get; set; }


        public string ExecutorId { get; set; }

        public int OrderId { get; set; }
    }
}