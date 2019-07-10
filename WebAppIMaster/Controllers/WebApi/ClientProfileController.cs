using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        [System.Web.Http.Route("api/GetClientProfileByPhoneNumber")]
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
        [System.Web.Http.Route("api/GetClientProfileById")]
        // GET: api/GetClientProfile/5
        public ClientProfileView GetClientIdClientProfileView ( string customerId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ClientProfileService repository = new ClientProfileService(db);
            try
            {
                var model = repository.GetCurrentCustomerClientProfileView(customerId);
                return model;
            }
            catch (KeyNotFoundException e)
            {

                return null;
            }
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



        /// <summary>
        /// api/SendPhotoToClientProfile [clientProfileId]  сохраняет аватар файла
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendPhotoToOrder
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SendPhotoToClientProfile")]
        public async void SendPhotoToOrder()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            string clientProfileId = request.Form["clientProfileId"];
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Images/Customer/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                ClientProfileService repository = new ClientProfileService(db);
                repository.SendPhotoToClientProfule(url, clientProfileId);
            }
        }

        /// <summary>
        /// api/GetClientProfilePhoto/{url} возвращает фото файла
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetClientProfilePhoto")]
        public HttpResponseMessage GetSupportPhoto(string url)
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

    }
}
