using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var model = repository.GetItemList();
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

        [Route("api/GetExecutorAvatar")]
        public HttpResponseMessage GetExecutorAvatar(string executorId)
        {
            return null;
        }
        [Route("api/GetExecutorPhoto")]
        public HttpResponseMessage GetExecutorPhoto( string photoPath )
        {
            return null;
        }

    }
}
