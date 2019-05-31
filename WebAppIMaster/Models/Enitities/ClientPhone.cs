using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class ClientPhone
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int MarketId { get; set; }

        public DateTime CreatedAt_Date { get; set; }
    }
}