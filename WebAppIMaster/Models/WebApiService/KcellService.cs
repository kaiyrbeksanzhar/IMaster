using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;

namespace WebAppIMaster.Models.WebApiService
{
    public class KcellService : IMessageService
    {


        public void PostKcell( int client_message_id, string sender, string recipient, string message_text )
        {
            WebRequest request = WebRequest.Create("https://api.kcell.kz/app/smsgw/rest/v2/messages");
            request.Method = "POST";
            string messages = "client_message_id=" + client_message_id.ToString() + "@&sender=" + sender + "@&recipient=" + recipient + "message_text" + message_text + "";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(messages);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            WebResponse response = request.GetResponse();
            response.Close();
        }


    }
}