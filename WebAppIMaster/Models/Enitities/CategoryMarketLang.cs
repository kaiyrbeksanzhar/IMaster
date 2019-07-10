using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class CategoryMarketLang
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Langcode { get; set; }
        public virtual CategoryMarket CategoryMarket { get; set; }
        public int CategoryMarketId { get; set; }
    }
}