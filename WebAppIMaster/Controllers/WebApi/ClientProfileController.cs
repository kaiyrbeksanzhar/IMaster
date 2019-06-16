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
        /// передайте phoneNumber(api/ClientProfile/{+77001611796}) возращает один элемент
        /// </summary>
        /// <param name="phoneNumber">Принимает параметр string.(phoneNumber)</param>
        // GET: api/ClientProfile/5
        public ClientProfileView Get(string phoneNumber)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientProfileService repository = new ClientProfileService(db);
            var model = repository.GetCurrentClientProfileView(phoneNumber);
            return model;
        }

        /// <summary>
        /// передайте id(api/GetClientProfile/5) возращает один элемент
        /// </summary>
        /// <param name="customerId">Принимает параметр string.(customerId)</param>
        [System.Web.Http.Route("api/GetClientProfile")]
        // GET: api/GetClientProfile/5
        public ClientProfileView GetClientIdClientProfileView ( string customerId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientProfileService repository = new ClientProfileService(db);
            var model = repository.GetCurrentCustomerClientProfileView(customerId);
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
