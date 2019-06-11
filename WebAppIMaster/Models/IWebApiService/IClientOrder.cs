using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IClientOrder
    {
        ClientOrderDetailsView GetClientOrderDetailsView(int id);
        ClientOrderItemView GetClientOrderItemView(int id);
        List<ClientOrderItemView> GetList();
        int Create(ClientOrderCreate item);
        List<ClientOrderItemView> GetListForClient( string clientId );
        List<ClientOrderItemView> GetListForExecutor( string executorId );
        ClientCommonOrderDetailsView GetClientCommonOrderDetailsView(int orderId);
    }
}