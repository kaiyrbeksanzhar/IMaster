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
    /// Клиент  ClientExecutorService
    /// </summary>
    public class ClientExecutorServiceController : ApiController
    {
        /// <summary>
        /// ClientExecutorService лист возвращает
        /// </summary>
        // GET: api/ClientExecutorService
        public List<ClientExecutorServiceMdl.ExecutorItem> Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            var model = repository.GetItemList();
            if(model == null)
            {
                return null;
            }
            return model;
        }
        /// <summary>
        /// передайте id(api/ClientExecutorService/5)  лист элементов от ExecutorResponse
        /// </summary>
        /// <param name="clientId">Принимает параметр clientId.</param>
        // GET: api/ClientExecutorService/5
        public List<ClientExecutorServiceMdl.ExecutorResponse> Get(string clientId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            if(String.IsNullOrEmpty(clientId) && clientId == "")
            {
                return null;
            }
            var model = repository.GetResponseList(clientId);
            return model;

        }
        /// <summary>
        /// передайте (api/AddExecutorToOrder/{clientId,orderId,executorId}) добавляет AddExecutorToOrder
        /// </summary>
        /// <param name="clientId">Принимает параметр сlientId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        /// <param name="executorId">Принимает параметр orderId.</param>
        [System.Web.Http.Route("api/AddExecutorToOrder")]
        public void AddExecutorToOrder(string clientId, int orderId, string executorId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            repository.AddExecutorToOrder(clientId, orderId, executorId);
        }

        /// <summary>
        /// передайте (api/CancelExecutorResponse/{clientId,orderId,executorId}) добавляет CancelExecutorResponse
        /// </summary>
        /// <param name="clientId">Принимает параметр сlientId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        /// <param name="executorId">Принимает параметр orderId.</param>
        [System.Web.Http.Route("api/CancelExecutorResponse")]
        public void CancelExecutorResponse( string clientId, int orderId, string executorId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            repository.CancelExecutorResponse(clientId, orderId, executorId);
        }

        /// <summary>
        /// передайте executorId(api/GetDetails/{executorId}) возвращает один элемент
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        //[System.Web.Http.Route("api/GetDetails")]
        public ClientExecutorServiceMdl.ExecutorDetails GetDetails( string executorId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            var model = repository.GetDetails(executorId);
            return model;
        }

        /// <summary>
        /// передайте categoryId(api/GetItemListForCategory/{categoryId}) возвращает лист элементов
        /// </summary>
        /// <param name="categoryId">Принимает параметр categoryId.</param>
        // GET: api/GetItemListForCategory
        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForCategory( int categoryId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            if (categoryId == 0)
            {
                return null;
            }
            var model = repository.GetItemListForCategory(categoryId);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// передайте specialtyId(api/GetItemListForSpecialty/{specialtyId}) возвращает лист элементов
        /// </summary>
        /// <param name="specialtyId">Принимает параметр specialtyId.</param>
        // GET: api/GetItemListForSpecialty
        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForSpecialty( int specialtyId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            if (specialtyId == 0)
            {
                return null;
            }
            var model = repository.GetItemListForSpecialty(specialtyId);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// передайте clientId(api/GetResponseList/{clientId}) возвращает лист элементов
        /// </summary>
        /// <param name="clientId">Принимает параметр clientId.</param>
        // GET: api/GetResponseList
        [System.Web.Http.Route("api/GetResponseList")]
        public List<ClientExecutorServiceMdl.ExecutorResponse> GetResponseList( string clientId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientExecutorService repository = new ClientExecutorService(db);
            if (String.IsNullOrEmpty(clientId))
            {
                return null;
            }
            var model = repository.GetResponseList(clientId);
            if (model == null)
            {
                return null;
            }
            return model;
        }
    }
}
