using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        /// <param name="id">Принимает параметр id.(GetUserId)</param>
        // GET: api/ClientProfile/5
        public ClientProfileView Get(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientProfileService repository = new ClientProfileService(db);
            var model = repository.Get(id);
            return model;
        }


        // POST: api/ClientProfile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ClientProfile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ClientProfile/5
        public void Delete(int id)
        {
        }
    }
}
