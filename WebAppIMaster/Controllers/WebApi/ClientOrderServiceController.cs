using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAppIMaster.Models;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;
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
        /// передайте Post запрос (api/ClientOrderEdit) возращает true false
        /// </summary>
        /// <param name="model">Принимает параметр ClientOrderEdit</param>
        // GET: api/Get/5
        [System.Web.Http.Route("api/ClientOrderEdit")]
        public bool ClientOrderEdit( ClientOrderEdit model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            var result = repository.ClientOrderEdit(model);
            return result;
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
            ClientOrderDetailsView model = repository.GetClientOrderDetailsView(id);
            return model;
        }
        /// <summary>
        /// ClientOrderCreate чтоб передать данные
        /// </summary>
        // POST: api/ClientOrderService
        [System.Web.Http.HttpPost, System.Web.Http.Route("api/ClientOrderService")]
        public int Post( [FromBody]ClientOrderCreate model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            int id = repository.Create(model);

            return id;
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
        /// SendPhotoToOrder сохранять фото в базу
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendPhotoToOrder
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SendPhotoToOrder")]
        public async void SendPhotoToOrder()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            int orderId = Convert.ToInt32(request.Form["orderId"]);
            string url = "~/Images/CustomerOrder/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                ClientOrderService repository = new ClientOrderService(db);
                repository.SendPhotoToOrder(url,orderId);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetOrderPhoto")]
        public HttpResponseMessage GetOrderPhoto(string url)
        {
            if(url == "")
            {
                return null;
            }
            byte[] content = File.ReadAllBytes(HttpContext.Current.Server.MapPath(url));
            MemoryStream ms = new MemoryStream(content);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

            return response;
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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/FinishClientMyOrder")]
        public bool FinishClientMyOrder(ClientMyOrderFinish item)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            bool res = repository.ClientMyOrderFinish(item);
            return res;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CancelClientMyOrder")]
        public bool CancelClientMyOrder(int orderId, CancelReason cancelReason)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            bool res = repository.CancelClientMyOrder(orderId, cancelReason);
            return res;
        }

        /// <summary>
        /// api/GetClientOrderPhoto/{url} возвращает фото
        /// </summary>
        /// <param name="url">Принимает параметр url.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetClientOrderPhoto")]
        public HttpResponseMessage GetClientOrderPhoto(string url)
        {
            if (url == "")
            {
                return null;
            }
            byte[] content = File.ReadAllBytes(HttpContext.Current.Server.MapPath(url));
            MemoryStream ms = new MemoryStream(content);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

            return response;
        }

        /// <summary>
        /// api/DeletePhotoClientOrder/{url} удаляет фото возвращает true false
        /// </summary>
        /// <param name="url">Принимает параметр url.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/DeletePhotoClientOrder")]
        public bool DeletePhotoClientOrder( string url , int orderId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientOrderService repository = new ClientOrderService(db);
            bool res = repository.DeletePhotoUrlAddress(url, orderId);
            return res;
        }




    }
}
