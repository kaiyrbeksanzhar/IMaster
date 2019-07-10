using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models
{
    public class MarketLangs
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string LangCode { get; set; }
        public virtual Market Market { get; set; }
        public int MarketId { get; set; }
    }
}