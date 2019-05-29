using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ClientOrderCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
        public OrderStartDateType StartDateType { get; set; }
        public DateTime StartedDate { get; set; }
        public decimal Cost { get; set; }
        public int RegionId { get; set; }
        public bool ReceiveOnlyResponses { get; set; }
        public int CategoryAndSpecializationId;
        public decimal Bonus { get; set; }
        public bool PayWithBounce { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }

        public class Photo
        {
            public string PhotoUrl1 { get; set; }
            public string PhotoUrl2 { get; set; }
            public string PhotoUrl3 { get; set; }
            public string PhotoUrl4 { get; set; }
        }
    }

    public class ClientOrderEdit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
        public OrderStartDateType StartDateType { get; set; }
        public DateTime StartedDate { get; set; }
        public decimal Cost { get; set; }
        public int RegionId { get; set; }
        public bool ReceiveOnlyResponses { get; set; }
        public int CategoryAndSpecializationId;
        public decimal Bonus { get; set; }
        public bool PayWithBounce { get; set; }
        public string Address { get; set; }

        public class Photo
        {
            public string PhotoUrl1 { get; set; }
            public string PhotoUrl2 { get; set; }
            public string PhotoUrl3 { get; set; }
            public string PhotoUrl4 { get; set; }
        }

    }

    public class ClientOrderItemView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string StartingDatetime { get; set; }
        public int Cost { get; set; }
        public OrderState Type { get; set; }
        public int NewNotifications { get; set; }

    }
}