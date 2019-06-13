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
        public HttpResponseMessage Post( [FromBody]ClientOrderCreate model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var result = repository.Create( model);

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

        /// <summary>
        /// передайте api/GetListForClient/5 возращает лист элементов
        /// </summary>
        /// <param name="clientId">Принимает параметр clientId.</param>
        // GET: api/GetListForClient/5
        [System.Web.Http.Route("api/GetListForClient")]
        public List<ClientOrderItemView> GetListForClient( string clientId )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.GetListForClient(clientId);
            return model;
        }

        /// <summary>
        /// передайте api/GetListForExecutor/5 возращает лист элементов
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        // GET: api/GetListForExecutor/5
        [System.Web.Http.Route("api/GetListForExecutor")]
        public List<ClientOrderItemView> GetListForExecutor( string executorId )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.GetListForExecutor(executorId);
            return model;
        }

        /// <summary>
        /// передайте api/GetClientCommonOrderDetails/5 возращает заказ клиента
        /// </summary>
        /// <param name="orderId">Принимает параметр orderId.</param>
        // GET: api/GetClientCommonOrderDetails/5
        [System.Web.Http.Route("api/GetClientCommonOrderDetails")]
        public ClientCommonOrderDetailsView GetClientCommonOrderDetailsView( int orderId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var model = repository.GetClientCommonOrderDetailsView(orderId);
            return model;
        }
    }
}
