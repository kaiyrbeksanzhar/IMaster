using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class NotificationItemViewMdl
    {
        public class NotificationItemView
        {

            public int Id { get; set; }
            public NotificationType Type { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Datetime { get; set; }
        }
    }
}