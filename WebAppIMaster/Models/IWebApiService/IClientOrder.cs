using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IClientOrder
    {
        HttpResponseMessage Create( ClientOrderCreate model );
        ClientOrderItemView Get( int id );
        int Edit( ClientOrderEdit model );
    }
}