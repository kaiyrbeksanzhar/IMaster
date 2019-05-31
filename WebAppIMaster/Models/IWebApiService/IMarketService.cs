using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.MarketServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IMarketService
    {
        List<MarketItem> GetList();
        MarketDetails GetDetails( int id );
        void ExecutorPhone( string executorId, int marketId );
        void ClientPhone( string clientId, int marketId );
    }
}