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

namespace WebAppIMaster.Models.WebApiService
{
    public class ClientExecutorService : IClientExecutorService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ClientExecutorService( ApplicationDbContext db ) => this.db = db;

        public void AddExecutorToOrder( string clientId, int orderId, string executorId )
        {
            string langcode = LanguageController.GetCurrentLanguageCode();
            WebAppIMaster.Models.Enitities.AddExecutorToOrder addExecutorToOrder = new Enitities.AddExecutorToOrder()
            {
                CustomerId = clientId,
                ExecutorId = executorId,
                OrderId = orderId
            };
            db.addExecutorToOrders.Add(addExecutorToOrder);
            db.SaveChanges();
        }

        public void CancelExecutorResponse( string clientId, int orderId, string executorId )
        {
            string langcode = LanguageController.GetCurrentLanguageCode();
            CancelExecutorResponse cancelExecutorResponse = new CancelExecutorResponse()
            {
                CustomerId = clientId,
                OrderId = orderId,
                ExecutorId = executorId
            };
            db.cancelExecutorResponses.Add(cancelExecutorResponse);
            db.SaveChanges();
        }

        public ClientExecutorServiceMdl.ExecutorDetails GetDetails( string executorId )
        {
            string langcode = LanguageController.GetCurrentLanguageCode();
            var model = db.Executors.Find(executorId);
            Image img;
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            foreach (var item in model.ExecutorPhotoFiles)
            {
                img = Image.FromFile(item.PhotoFileUrl);
                string PhotoType = item.PhotoFileUrl.Substring(item.PhotoFileUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                photos.Add(Imagesbyte, PhotoType);
            }

            return new ClientExecutorServiceMdl.ExecutorDetails()
            {
                LastName = model.User.LastName,
                FirstName = model.User.FirstName,
                FatherName = model.User.FatherName == null ? " " : model.User.FatherName,
                Online = model.ExecotorOnline,
                Rating = model.Rating.ToString(),
                Check = model.ExecutorCheck,
                RegisterDate = model.RegistrationDateTime,
                ClosedOrdersCount = model.ExecutorClosedOrdersCount,
                RegionId = model.CityId,
                RegionName = model.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                GenderId = model.Gender == Enums.Gender.Male ? 1 : 2,
                GenderName = model.Gender == Enums.Gender.Male ? "Мужской" : "Женский",
                Birthday = model.BirthDay,
                CategoryId = model.Categories.Select(c => c.Id).FirstOrDefault(),
                CategoryName = model.Categories.SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                SpecializationId = model.specializations.Select(c => c.Id).FirstOrDefault(),
                SpecializationName = model.specializations.SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                ExecutorType = model.ExecutorType,
                PhoneNumber = model.PhoneNumber,
                PhotoFiles = photos,
                YouTubeVideoUrl = model.YouTubeVideoUrl
            };
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemList()
        {
            throw new NotImplementedException();
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForCategory( int categoryId )
        {
            throw new NotImplementedException();
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForSpecialty( int specialtyId )
        {
            throw new NotImplementedException();
        }

        public List<ClientExecutorServiceMdl.ExecutorResponse> GetResponseList( string clientId )
        {
            throw new NotImplementedException();
        }
    }
}