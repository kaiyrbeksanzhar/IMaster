using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.ClientProfileMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IClientProfileService
    {
        ClientProfileView Get(int Id);
    }
}