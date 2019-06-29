using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.WebApiModel
{
    public class messages
    {
        public int client_message_id { get; set; }
        public string sender { get; set; }
        public string recipient { get; set; }
        public string message_text { get; set; }
    }
}