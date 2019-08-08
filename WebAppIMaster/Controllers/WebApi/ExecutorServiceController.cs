using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;
using static WebAppIMaster.Models.WebApiModel.ExecutorServiceMdl;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// ExecutorService api-исполнителя
    /// </summary>
    public class ExecutorServiceController : ApiController
    {
        /// <summary>
        /// передайте (ExecutorRegister) регистрация Executor-a
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ExecutorRegister")]
        public string PutExecutorRegister( [FromBody]ExecutorServiceMdl.ExecutorRegister item )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            string id = repository.Register(item);
            return id;
        }

        /// <summary>
        /// передайте (api/GetById/{id}) возвращает профиль исполнителя
        /// </summary>
        /// <param name="id">Принимает параметр id(Executor).</param>
        // GET: api/ExecutorService/5
        public ExecutorProfile GetById( string id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetById(id);
            return model;
        }

        /// <summary>
        /// api/GetItemExecutorList возвращает Лист ExecutorItem
        /// </summary>
        // GET: api/ExecutorService/5
        [System.Web.Http.Route("api/GetItemExecutorList")]
        public List<ExecutorServiceMdl.ExecutorItem> GetItemList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            List<ExecutorItem> model = repository.GetItemList();
            return model;
        }

        /// <summary>
        /// api/GetItemListForSpecialization/{specializationId} возвращает Лист ExecutorItem
        /// </summary>
        /// <param name="specializationId">Принимает параметр specializationId.</param>
        // GET: api/GetItemListForSpecialization/5
        [System.Web.Http.Route("api/GetItemListForSpecialization")]
        public List<ExecutorServiceMdl.ExecutorItem> GetItemListForSpecialization( int specializationId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetItemListForSpecialization(specializationId);
            return model;
        }

        /// <summary>
        /// api/GetItemListSuitableForOrder/{orderId} возвращает Лист ExecutorItem
        /// </summary>
        /// <param name="orderId">Принимает параметр orderId.</param>
        // GET: api/GetItemListSuitableForOrder/5
        [System.Web.Http.Route("api/GetItemListSuitableForOrder")]
        public List<ExecutorServiceMdl.ExecutorItem> GetItemListSuitableForOrder( int orderId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetItemListSuitableForOrder(orderId);
            return model;
        }

        /// <summary>
        /// передайте (api/ExecutorService/{phoneNumber}) возвращает профиль исполнителя
        /// </summary>
        /// <param name="phoneNumber">Принимает параметр phoneNumber.</param>
        // GET: api/ExecutorService/5
        public ExecutorProfile GetByPhoneNumber( string phoneNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetByPhoneNumber(phoneNumber);
            return model;
        }

        /// <summary>
        /// передайте (api/ExecutorService/{id}) ссылает смс
        /// </summary>
        /// <param name="newPhoneNumber">Принимает параметр newPhoneNumber(Executor).</param>
        // GET: api/ExecutorService/5
        public void SendCheckingCodeForUpdatePhoneNumber( string newPhoneNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.SendCheckingCodeForUpdatePhoneNumber(newPhoneNumber);

        }
        /// <summary>
        /// передайте (api/UpdatePhoneNumber/{executorId,newPhoneNumber,checkingCode}) обновляет номер телефона Executor
        /// </summary>
        /// <param name="executorId">Принимает параметр сlientId.</param>
        /// <param name="newPhoneNumber">Принимает параметр newPhoneNumber.</param>
        /// <param name="checkingCode">Принимает параметр checkingCode.</param>
        // Post: api/UpdatePhoneNumber/5

        /// <summary>
        /// передайте (api/ExecutorProfileEdit/{ExecutorProfileEdit}) обновляется профиль Executor
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ExecutorProfileEdit")]
        public void PostExecutorProfileEdit( [FromBody]ExecutorServiceMdl.ExecutorProfileEdit item )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.UpdateProfile(item);
        }

        /// <summary>
        /// передайте (api/UpdatePhoneNumber/{executorId,newPhoneNumber,checkingCode}) обновляет сотовый номер исполнителя
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        /// <param name="newPhoneNumber">Принимает параметр newPhoneNumber.</param>
        /// <param name="checkingCode">Принимает параметр checkingCode.</param>
        [System.Web.Http.Route("api/UpdatePhoneNumber")]
        public bool UpdatePhoneNumber( string executorId, string newPhoneNumber, string checkingCode )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            bool result = repository.UpdatePhoneNumber(executorId, newPhoneNumber, checkingCode);
            if (!result)
                return false;
            return true;
        }

        /// <summary>
        /// передайте (api/UpdatePhotoFiles/{executorId,actualPhotoFiles}) обновляется фото Executor
        /// </summary>
        /// <param name="executorId">Принимает параметр id(executorId).</param>
        /// <param name="actualPhotoFiles">Принимает параметр (actualPhotoFile).</param>
        [System.Web.Http.Route("api/UpdatePhotoFiles")]
        public void PostUpdatePhotoFiles( string executorId, Dictionary<byte[], string> actualPhotoFiles )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.UpdatePhotoFiles(executorId, actualPhotoFiles);
        }

        /// <summary>
        /// передайте (api/UpdateExecutorServices/{actualServices,executorId}) обновляется  ExecutorService
        /// </summary>
        /// <param name="actualServices">Принимает параметр actualServices.</param>
        /// <param name="executorId">Принимает параметр (executorId).</param>
        [System.Web.Http.Route("api/UpdateExecutorServices")]
        public void UpdateServices( List<ExecutorServiceMdl.ExecutiveService> actualServices, string executorId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.UpdateServices(actualServices, executorId);
        }


        /// <summary>
        /// передайте (api/UpdateExecutorType/{(ExecutorTypeEdit)-item,executorId}) обновляется  ExecutorService
        /// </summary>
        /// <param name="item">Принимает параметр ExecutorTypeEdit-item.</param>
        /// <param name="executorId">Принимает параметр (executorId).</param>
        [System.Web.Http.Route("api/UpdateExecutorType")]
        public void UpdateType( ExecutorServiceMdl.ExecutorTypeEdit item, string executorId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.UpdateType(item, executorId);
        }

        /// <summary>
        /// api/GetExecutorAvatar/{url} возвращает фото
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetExecutorAvatar")]
        public HttpResponseMessage GetExecutorAvatar(string url)
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
        /// api/SendExecutorAvatar  сохраняет фото  Request.Form["executorId"] и url
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendPhotoToOrder
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SendExecutorAvatar")]
        public async void SendExecutorAvatar()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            string executorId = request.Form["executorId"];
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Images/Executor/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                ExecutorService repository = new ExecutorService(db);
                repository.SendExecutorAvatar(url, executorId);
            }
        }

        /// <summary>
        /// api/SendExecutorPhoto  сохраняет фото ExecutorFile  Request.Form["executorId"] и url
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendExecutorPhoto
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SendExecutorAvatar")]
        public async void SendExecutorPhoto()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            string executorId = request.Form["executorId"];
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Images/ExecutorFiles/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                ExecutorService repository = new ExecutorService(db);
                repository.SendExecutorPhotos(url, executorId);
            }
        }

        /// <summary>
        /// api/GetExecutorPhoto/{url} возвращает фото файла
        /// </summary>
        /// <param name="url">Принимает параметр url.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetExecutorPhoto")]
        public HttpResponseMessage GetExecutorPhoto(string url)
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
        /// api/SendExecutorPhoto  сохраняет фото ExecutprPassportFile  Request.Form["executorId"] и url
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendExecutorPhoto
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SaveExecutprPassportFile")]
        public async void SaveExecutprPassportFile()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            string executorId = request.Form["executorId"];
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Images/ExecutorFiles/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff-") + Guid.NewGuid().ToString() + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                ExecutorService repository = new ExecutorService(db);
                repository.SendExecutroPassportFile(url, executorId);
            }
        }

        /// <summary>
        /// api/SendExecutorPhoto  сохраняет фото ExecutprPassportFile  Request.Form["executorId"] и url
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendExecutorPhoto
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SaveExecutorWorkPhoto")]
        public async void SaveExecutorWorkPhoto()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            string executorId = request.Form["executorId"];
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Images/ExecutorFiles/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                ExecutorService repository = new ExecutorService(db);
                repository.SendExecutorWorkPhotoFile(url, executorId);
            }
        }
        /// <summary>
        /// передайте (api/DeleteExecutorWorkPhoto/{Url}) обновляется  ExecutorService
        /// </summary>
        /// <param name="Url">Принимает параметр (Url).</param>
        [System.Web.Http.Route("api/DeleteExecutorWorkPhoto")]
        public void DeleteExecutorWorkPhoto(string Url)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.DeleteWorkPhotoFile(Url);
        }
        /// <summary>
        /// передайте (api/GetExecutorPassportFiles/{executorId}) обновляется  ExecutorService
        /// </summary>
        /// <param name="executorId">Принимает параметр (executorId).</param>
        [System.Web.Http.Route("api/GetExecutorPassportFiles")]
        public ExecutorPassportFile GetExecutorPassportFiles( string executorId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var result = repository.GetExecutorPassportFile(executorId);
            return result;
        }

        /// <summary>
        /// передайте (api/DeletePassportFiles/{Url}) обновляется  ExecutorService
        /// </summary>
        /// <param name="Url">Принимает параметр (Url).</param>
        [System.Web.Http.Route("api/DeletePassportFiles")]
        public void DeletePassportFiles(string Url)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.deletePassportFile(Url);
        }

    }
}
