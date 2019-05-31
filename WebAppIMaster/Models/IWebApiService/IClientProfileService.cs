using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebAppIMaster.Models.WebApiModel.ClientProfileMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IClientProfileService
    {
        ClientProfileView GetCurrentClientProfileView(int id);
        void EditCurrentClientProfile(Controller controller, ClientProfileEdit item );
    }
}