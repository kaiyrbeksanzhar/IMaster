using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.MarketingService;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IMarketingService 
    {
        MarketingNameView Get( int id );
        List<MarketingNameView> GetList();
    }
}