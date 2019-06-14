using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;
using static WebAppIMaster.Models.WebApiModel.ClientProfileMdl;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// Клиент ClientProfile Api
    /// </summary>
    public class ClientProfileController : ApiController
    {
        /// <summary>
        /// передайте id(api/ClientProfile/5) возращает один элемент
        /// </summary>
        /// <param name="string">Принимает параметр string.(GetUserId)</param>
        // GET: api/ClientProfile/5
        public ClientProfileView Get(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientProfileService repository = new ClientProfileService(db);
            var model = repository.GetCurrentClientProfileView(id);
            return model;
        }
        /// <summary>
        /// Post запрос  ClientProfileEdit изменить данные
        /// </summary>
        // GET: api/ClientProfile/5
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post( [FromBody]ClientProfileEdit model )
        {
            System.Web.Mvc.Controller controller = null;
            ApplicationDbContext db = new ApplicationDbContext();
            ClientProfileService repository = new ClientProfileService(db);
            repository.EditCurrentClientProfile(controller, model);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

    }
}
