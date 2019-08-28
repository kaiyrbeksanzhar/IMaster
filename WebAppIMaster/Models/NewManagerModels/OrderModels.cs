using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class OrderModels
    {
        public class OrderList
        {
            public string OrderId { get; set; }
            public string Title { get; set; }
            public string CustomerName { get; set; }
            public string City { get; set; }
            public string SpecilizationName { get; set; }
            public bool State { get; set; }
        }
        

    }
}