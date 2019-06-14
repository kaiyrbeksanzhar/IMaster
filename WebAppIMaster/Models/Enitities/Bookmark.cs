using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class Bookmark
    {
        [Key, Column(Order = 1)]
        public string BookMarkUserId { get; set; }
        [Key, Column(Order = 2)]
        public int OrderId { get; set; }
    }
}