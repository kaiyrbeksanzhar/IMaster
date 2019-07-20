using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IMessageService
    {
         void PostKcell( int client_message_id, string sender, string recipient, string message_text );
    }
}