using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IRegionService
    {
        RegionNameView Get( int id );
        List<RegionNameView> GetList();
    }
}