using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiService
{
    public class ClientExecutorService : IClientExecutorService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ClientExecutorService(ApplicationDbContext db) => this.db = db;

        public void AddExecutorToOrder(string clientId, int orderId, string executorId)
        {
            CustomerOrder customerOrder = db.CustomerOrders.Find(orderId);

            customerOrder.OrderState = OrderState.InProcess;
            customerOrder.OrderType = Enitities.Enums.OrderStatus.Processing;

            customerOrder.ExecutorId = executorId;
            db.Entry(customerOrder).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void CancelExecutorResponse(string clientId, int orderId, string executorId)
        {
            throw new NotImplementedException();
        }

        public ClientExecutorServiceMdl.ExecutorDetails GetDetails(string executorId)
        {
            string langcode = LanguageController.GetCurrentLanguageCode();
            //var model = db.Executors.Where(e=>e.Id == executorId).SingleOrDefault();
            var model = (from e in db.Executors

                         where e.Id == executorId

                         select new
                         {
                             LastName = e.User.LastName,
                             FirstName = e.User.FirstName,
                             FatherName = e.User.FatherName,
                             Online = e.ExecotorOnline,
                             Rating = e.Rating,
                             Check = e.ExecutorCheck,
                             RegisterDate = e.RegistrationDateTime,
                             ClosedOrdersCount = e.ExecutorClosedOrdersCount,
                             RegionId = e.CityId,
                             RegionName = e.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                             GenderId = e.User.GenderId,
                             Birthday = e.BirthDay,
                             CategoryId = e.Categories.Select(c => c.Id).FirstOrDefault(),
                             CategoryName = e.Categories.SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                             SpecializationId = e.ExecutorSpecializations.Select(es => es.SpecializationId).FirstOrDefault(),
                             SpecializationName = e.ExecutorSpecializations.Select(es => es.Specialization).SelectMany(s => s.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                             ExecutorType = e.ExecutorType,
                             PhoneNumber = e.PhoneNumber,
                             AvatarUri = e.AvatarUrl == null ? null : "http://i-master.kz/api/GetExecutorAvatar?executorId=" + e.AvatarUrl,
                             PhotoFiles = (from epf in e.ExecutorPhotoFiles

                                           select epf).ToList(),
                             YouTubeVideoUrl = e.YouTubeVideoUrl,
                         }).SingleOrDefault();
            Image img;
            List<string> photoUri = new List<string>();
            if (model.PhotoFiles.Count > 0)
            {
                foreach (var item in model.PhotoFiles)
                {
                    photoUri.Add("http://i-master.kz/api/GetExecutorPhoto?photoPath=" + item.PhotoFileUrl);
                }
            }

            return new ClientExecutorServiceMdl.ExecutorDetails()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                FatherName = model.FatherName == null ? " " : model.FatherName,
                Online = model.Online,
                Rating = model.Rating.ToString(),
                Check = model.Check,
                RegisterDate = model.RegisterDate == null ? DateTime.MinValue : model.RegisterDate,
                ClosedOrdersCount = model.ClosedOrdersCount,
                RegionId = model.RegionId,
                RegionName = model.RegionName,
                GenderId = model.GenderId,
                Birthday = model.Birthday,
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                SpecializationId = model.SpecializationId,
                SpecializationName = model.SpecializationName,
                ExecutorType = model.ExecutorType,
                PhoneNumber = model.PhoneNumber,
                PhotoUris = photoUri,
                AvatarUri = model.AvatarUri,
                YouTubeVideoUrl = model.YouTubeVideoUrl
            };
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemList()
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientExecutorServiceMdl.ExecutorItem> list = db.Executors.Select(u => new ClientExecutorServiceMdl.ExecutorItem
            {
                Id = u.Id,
                Lastname = u.User.LastName,
                Firstname = u.User.FirstName,
                PhoneNumber = u.PhoneNumber,
                AvatarUri = u.AvatarUrl == null ? null : "http://i-master.kz/api/GetClientExecutorPhoto?url=" + u.AvatarUrl,
                Check = u.ExecutorCheck == true ? true : false,
                ClosedOrdersCount = u.Orders.Where(o => o.OrderState == OrderState.Finished).Count(),
                Online = true,
                Rating = u.Rating + "",
                RegisterDate = (DateTime)u.RegistrationDateTime,
            }).ToList();

            return list;
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForCategory(int categoryId)
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientExecutorServiceMdl.ExecutorItem> list = db.Executors.Where(u => u.specializations.Any(s => s.CategoryId == categoryId)).Select(u => new ClientExecutorServiceMdl.ExecutorItem
            {
                Id = u.Id,
                Lastname = u.User.LastName,
                Firstname = u.User.FirstName,
                PhoneNumber = u.PhoneNumber,
                AvatarUri = u.AvatarUrl == null ? null : "http://i-master.kz/api/GetClientExecutorPhoto?url=" + u.AvatarUrl,
                Check = u.ExecutorCheck == true ? true : false,
                ClosedOrdersCount = u.Orders.Where(o => o.OrderState == OrderState.Finished).Count(),
                Online = true,
                Rating = u.Rating + "",
                RegisterDate = (DateTime)u.RegistrationDateTime,
            }).ToList();

            return list;
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForSpecialty(int specialtyId)
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientExecutorServiceMdl.ExecutorItem> list = db.Executors.Where(u => u.specializations.Any(s => s.Id == specialtyId)).Select(u => new ClientExecutorServiceMdl.ExecutorItem
            {
                Id = u.Id,
                Lastname = u.User.LastName,
                Firstname = u.User.FirstName,
                PhoneNumber = u.PhoneNumber,
                AvatarUri = u.AvatarUrl == null ? null : "http://i-master.kz/api/GetClientExecutorPhoto?url=" + u.AvatarUrl,
                Check = u.ExecutorCheck == true ? true : false,
                ClosedOrdersCount = u.Orders.Where(o => o.OrderState == OrderState.Finished).Count(),
                Online = true,
                Rating = u.Rating + "",
                RegisterDate = (DateTime)u.RegistrationDateTime,
            }).ToList();

            return list;
        }

        public List<ClientExecutorServiceMdl.ExecutorResponse> GetResponseList(string clientId, int lastResponseId = -1)
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientExecutorServiceMdl.ExecutorResponse> list =
                (from u in db.Responses
                 let lastResponse = lastResponseId == -1 ? null : (from r in db.Responses
                                                                   where r.OrderId == lastResponseId
                                                                   select r).FirstOrDefault()
                 where lastResponse == null ? true : lastResponse.CreatedAt_Date < u.CreatedAt_Date
                 where u.Order.CustomerId == clientId
                 select new
                 {
                     ExecutorId = u.ExecutorId,
                     Lastname = u.Executor.User.LastName,
                     Firstname = u.Executor.User.FirstName,
                     PhoneNumber = u.Executor.PhoneNumber,
                     AvatarUri = u.Executor.AvatarUrl == null ? null : "http://i-master.kz/api/GetClientExecutorPhoto?url=" + u.Executor.AvatarUrl,
                     Check = u.Executor.ExecutorCheck == true ? true : false,
                     ClosedOrdersCount = u.Executor.Orders.Where(o => o.OrderState == OrderState.Finished).Count(),
                     CreateAt = u.CreatedAt_Date,
                     ExecutorMessage = u.ResponseComment,
                     Online = true,
                     OrderId = u.OrderId,
                     Rating = u.Executor.Rating + "",
                     RegisterDate = (DateTime)u.Executor.RegistrationDateTime,
                     ExecutorType = u.Executor.ExecutorType,
                 }).ToList().Select(u => new ClientExecutorServiceMdl.ExecutorResponse
                 {
                     ExecutorId = u.ExecutorId,
                     Lastname = u.Lastname,
                     Firstname = u.Firstname,
                     PhoneNumber = u.PhoneNumber,
                     AvatarUri = u.AvatarUri,
                     Check = u.Check,
                     ClosedOrdersCount = u.ClosedOrdersCount,
                     CreateAt = u.CreateAt,
                     ExecutorMessage = u.ExecutorMessage,
                     Online = u.Online,
                     OrderId = u.OrderId,
                     Rating = u.Rating,
                     RegisterDate = u.RegisterDate,
                     ExecutorType = u.ExecutorType ?? ExecutorType.Individual,
                 }).ToList();

            return list;
        }

        public List<ClientExecutorServiceMdl.ExecutorResponse> GetResponseListForOrder(int orderId)
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientExecutorServiceMdl.ExecutorResponse> list = db.Responses.Where(u => u.OrderId == orderId).Select(u => new
            {
                ExecutorId = u.ExecutorId,
                Lastname = u.Executor.User.LastName,
                Firstname = u.Executor.User.FirstName,
                PhoneNumber = u.Executor.PhoneNumber,
                AvatarUri = u.Executor.AvatarUrl == null ? null : "http://i-master.kz/api/GetClientExecutorPhoto?url=" + u.Executor.AvatarUrl,
                Check = u.Executor.ExecutorCheck == true ? true : false,
                ClosedOrdersCount = u.Executor.Orders.Where(o => o.OrderState == OrderState.Finished).Count(),
                CreateAt = u.CreatedAt_Date,
                ExecutorMessage = u.ResponseComment,
                Online = true,
                OrderId = u.OrderId,
                Rating = u.Executor.Rating + "",
                RegisterDate = (DateTime)u.Executor.RegistrationDateTime,
                ExecutorType = u.Executor.ExecutorType,
            }).ToList().Select(u => new ClientExecutorServiceMdl.ExecutorResponse
            {
                ExecutorId = u.ExecutorId,
                Lastname = u.Lastname,
                Firstname = u.Firstname,
                PhoneNumber = u.PhoneNumber,
                AvatarUri = u.AvatarUri,
                Check = u.Check,
                ClosedOrdersCount = u.ClosedOrdersCount,
                CreateAt = u.CreateAt,
                ExecutorMessage = u.ExecutorMessage,
                Online = u.Online,
                OrderId = u.OrderId,
                Rating = u.Rating,
                RegisterDate = u.RegisterDate,
                ExecutorType = u.ExecutorType ?? ExecutorType.Individual,
            }).ToList();

            return list;
        }
    }
}