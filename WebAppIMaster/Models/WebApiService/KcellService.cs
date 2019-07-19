using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Web.Script.Serialization;

namespace WebAppIMaster.Models.WebApiService
{
    public class KcellService /*: IMessageService*/
    {

        public static void PostKcell( string client_message_id, string sender, string recipient, string message_text )
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.kcell.kz/app/smsgw/rest/v2/messages");
            httpWebRequest.ContentType = " application/json;charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Host = "api.kcell.kz";
            httpWebRequest.Accept = "application/json, text/plain, */*";
            httpWebRequest.Headers["Authorization"] = "Basic aXBfaWNvcnBfcmVzdDpES1kzamdRaA==";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    client_message_id = client_message_id,
                    sender = sender,
                    recipient = recipient,
                    message_text = message_text,
                    priority = "2",
                });

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }


    }
}