﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enitities.Enums;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    ///   ExecutorOrderService
    /// </summary>
    public class ExecutorOrderServiceController : ApiController
    {

        /// <summary>
        /// передайте id(api/ExecutorOrderService/5)  один элемент от ExecutorOrder
        /// </summary>
        /// <param name="orderId">Принимает параметр orderId.</param>
        // GET: api/ExecutorOrderService/5
        public ExecutorOrderMdl.ExecutorOrderDetails Get(int orderId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            if (orderId == 0)
            {
                return null;
            }
            var model = repository.GetDetails(orderId, userId);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// передайте (api/Response/{executorId,orderId,responseComment}) добавляет Response
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        /// <param name="responseComment">Принимает параметр responseComment.</param>
        [System.Web.Http.Route("api/Response")]
        public void Response(string executorId, int orderId, string responseComment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            repository.Response(executorId, orderId, responseComment);
        }

        /// <summary>
        /// передайте (api/CancelOrder/{executorId,orderId,responseComment}) добавляет CancelOrder
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        /// <param name="cancelType">Принимает параметр cancelType.</param>
        [System.Web.Http.Route("api/ExecutorCancelOrder")]
        public void CancelOrder(string executorId, int orderId, String cancelReason)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            repository.CancelOrder(executorId, orderId, cancelReason);
        }

        /// <summary>
        /// передайте (api/CallToClient/{executorId,orderId,responseComment}) добавляет CallToClient
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        [System.Web.Http.Route("api/CallToClient")]
        public void CallToClient(string executorId, int orderId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            repository.CallToClient(executorId, orderId);
        }

        /// <summary>
        /// передайте (api/GetItemList/{CategoryIds,SpecializationIds}) добавляет CallToClient
        /// </summary>
        /// <param name="CategoryIds">Принимает параметр CategoryIds.</param>
        /// <param name="SpecializationIds">Принимает параметр SpecializationIds.</param>
        /// 
        //[HttpGet]
        //[System.Web.Http.Route("api/GetItemList")]
        //public List<ExecutorOrderMdl.ExecutorOrderItem> GetItemList(int[] CategoryIds = null, int[] SpecializationIds = null)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    ExecutorOrderService repository = new ExecutorOrderService(db);
        //    string userName = User.Identity.Name;
        //    var model = repository.GetItemList(CategoryIds.ToList(), SpecializationIds.ToList(), userName);
        //    return model;
        //}
        [HttpGet]
        [System.Web.Http.Route("api/GetItemList")]
        public List<ExecutorOrderMdl.ExecutorOrderItem> GetItemList(string userId = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            List<ExecutorOrderMdl.ExecutorOrderItem> model = repository.GetItemList(null, null, userId);
            return model;
        }

        /// <summary>
        /// передайте (api/AddBookMark/{userId,orderId})  сохраняет bookMark
        /// </summary>
        /// <param name="userId">Принимает параметр userId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        // GET: api/AddBookMark/{userId,orderId}
        [System.Web.Http.Route("api/AddBookMark")]
        public void AddBookMark(string userId, int orderId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            repository.AddBookmark(userId, orderId);
        }

        /// <summary>
        /// передайте (api/DeleteBookMark/{userId,orderId})  сохраняет bookMark
        /// </summary>
        /// <param name="userId">Принимает параметр userId.</param>
        /// <param name="orderId">Принимает параметр orderId.</param>
        // GET: api/DeleteBookMark/{userId,orderId}
        [System.Web.Http.Route("api/DeleteBookMark")]
        public void Delete(string userId, int orderId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorOrderService repository = new ExecutorOrderService(db);
            repository.RemoveBookmark(userId, orderId);
        }

        /// <summary>
        /// api/GetClientExecutorOrderPhoto/{url} возвращает фото файла
        /// </summary>
        /// <param name="url">Принимает параметр url.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetClientExecutorOrderPhoto")]
        public HttpResponseMessage GetClientExecutorOrderPhoto(string url)
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
