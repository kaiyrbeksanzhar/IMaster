using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.GenderServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IGenderService
    {
        GenderNameView Get( int id );
        List<GenderNameView> GetList();
    }
}