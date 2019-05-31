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
        /// передайте id(api/ClientOrderItemView/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: api/Get/5
        [System.Web.Http.Route("api/Get")]
        public ClientOrderItemView Get( int id )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.GetClientOrderItemView(id);
            return model;
        }
        /// <summary>
        /// передайте id(api/GetClientOrderDetailsView/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: api/GetClientOrderDetailsView/5
        [System.Web.Http.Route("api/GetClientOrderDetailsView")]
        public ClientOrderDetailsView GetClientOrderDetailsView( int id )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.GetClientOrderDetailsView(id);
            return model;
        }
        /// <summary>
        /// ClientOrderCreate чтоб передать данные
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
        /// <summary>
        /// ClientOrderItemView лист возвращает
        /// </summary>
        // GET: api/Get

        [System.Web.Http.Route("api/GetClientOrderItemView")]
        public List<ClientOrderItemView> GetClientOrderItemView()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.GetList();
            return model;
        }
    }
}
