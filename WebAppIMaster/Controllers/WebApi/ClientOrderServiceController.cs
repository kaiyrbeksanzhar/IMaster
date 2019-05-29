using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// Клиент заказы Service
    /// </summary>
    public class ClientOrderServiceController : ApiController
    {

        /// <summary>
        /// передайте id(api/ClientOrderService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: api/ClientOrderService/5
        public ClientOrderItemView Get( int id )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.Get(id);
            return model;
        }
        /// <summary>
        /// Клиент заказын косу ушин
        /// </summary>
        // POST: api/ClientOrderService
        [HttpPost,System.Web.Http.Route("api/ClientOrderService")]
        public HttpResponseMessage Post( [FromBody]ClientOrderCreate model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var result = repository.Create(model);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT: api/ClientOrderService/5
        public void Put( int id, [FromBody]string value )
        {
        }

        // DELETE: api/ClientOrderService/5
        public void Delete( int id )
        {
        }
    }
}
