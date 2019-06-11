﻿using System;
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
    public class ExecutorServiceController : ApiController
    {
        /// <summary>
        /// передайте (ExecutorRegister) регистрация Executor-a
        /// </summary>
        [System.Web.Http.Route("api/ExecutorRegister")]
        public string PutExecutorRegister([FromBody]ExecutorServiceMdl.ExecutorRegister item)
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
        public ExecutorProfile GetById(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetById(id);
            return model;
        }

        // GET: api/ExecutorService/5
        public ExecutorProfile GetByPhoneNumber(string phoneNumber)
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
        public void PostExecutorProfileEdit([FromBody]ExecutorServiceMdl.ExecutorProfileEdit item)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.UpdateProfile(item);
        }

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
        public void UpdateServices(List<ExecutorServiceMdl.ExecutiveService> actualServices, string executorId)
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
        public void UpdateType(ExecutorServiceMdl.ExecutorTypeEdit item, string executorId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.UpdateType(item, executorId);
        }


    }
}
