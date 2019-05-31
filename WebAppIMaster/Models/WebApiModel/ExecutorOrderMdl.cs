using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ExecutorOrderMdl
    {
        public class ExecutorOrderItem
        {
            public int OrderId { get; set; }
            public string OrderTitle { get; set; }
            public string ClientId { get; set; }
            public string ClientLastName { get; set; }
            public string ClientFirstName { get; set; }
            public string ClientFatherName { get; set; }
            public bool Bookmark { get; set; }
            public bool Responded { get; set; }
            public OrderStartDateType StartDateType { get; set; }
            public DateTime StartDate { get; set; }
            public OrderCostType CostType { get; set; }
            public int Cost { get; set; }
            public OrderState OrderStatus { get; set; }
        }

        public class ExecutorOrderDetails
        {
            public int OrderId { get; set; }
            public string OrderTitle { get; set; }
            public string ClientId { get; set; }
            public string ClientLastName { get; set; }
            public string ClientFirstName { get; set; }
            public string ClientFatherName { get; set; }
            public bool Bookmark { get; set; }
            public bool Responded { get; set; }
            public OrderStartDateType StartDateType { get; set; }
            public DateTime StartDate { get; set; }
            public OrderCostType CostType { get; set; }
            public int Cost { get; set; }
            public OrderState OrderStatus { get; set; }

            public string Description { get; set; }
            public int RegionId { get; set; }
            public string RegionName { get; set; }
            public string Address { get; set; }
            public Dictionary<byte[], String> PhotoFiles { get; set; }
        }

    }
}