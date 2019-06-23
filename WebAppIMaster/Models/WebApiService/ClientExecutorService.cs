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

        public ClientExecutorService( ApplicationDbContext db ) => this.db = db;

        public void AddExecutorToOrder( string clientId, int orderId, string executorId )
        {
            string langcode = LanguageController.GetCurrentLanguageCode();
            WebAppIMaster.Models.AddExecutorToOrder addExecutorToOrder = new AddExecutorToOrder()
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
                             RegionName = e.City.Langs.Where(l=>l.Langcode == langcode).Select(l=>l.Name).FirstOrDefault(),
                             GenderId = e.User.GenderId,
                             Birthday = e.BirthDay,
                             CategoryId = e.Categories.Select(c=>c.Id).FirstOrDefault(),
                             CategoryName = e.Categories.SelectMany(c=>c.Langs).Where(l=>l.Langcode == langcode).Select(l=>l.Name).FirstOrDefault(),
                             SpecializationId = e.ExecutorSpecializations.Select(es=>es.SpecializationId).FirstOrDefault(),
                             SpecializationName = e.ExecutorSpecializations.Select(es=>es.Specialization).SelectMany(s=>s.Langs).Where(l=>l.Langcode == langcode).Select(l=>l.Name).FirstOrDefault(),
                             ExecutorType = e.ExecutorType,
                             PhoneNumber = e.PhoneNumber,
                             PhotoFiles = (from epf in e.ExecutorPhotoFiles

                                           select epf).ToList(),
                             YouTubeVideoUrl = e.YouTubeVideoUrl,
                         }).SingleOrDefault();
            Image img;
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            if (model.PhotoFiles.Count > 0)
            {
                foreach (var item in model.PhotoFiles)
                {
                    img = Image.FromFile(item.PhotoFileUrl);
                    string PhotoType = item.PhotoFileUrl.Substring(item.PhotoFileUrl.LastIndexOf(".") + 1);
                    byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                    photos.Add(Imagesbyte, PhotoType);
                }
            }
            else
            {
                photos = null;
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
                PhotoFiles = photos,
                YouTubeVideoUrl = model.YouTubeVideoUrl
            };
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemList()
        {
            List<ClientExecutorServiceMdl.ExecutorItem> result = new List<ClientExecutorServiceMdl.ExecutorItem>();
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            string langcode = LanguageController.GetCurrentLanguageCode();
            var model = db.Executors.ToList();
            Image img;
            string PhotoType = "";
            foreach (var executor in model)
            {
                img = Image.FromFile(executor.AvatarUrl);
                PhotoType = executor.AvatarUrl.Substring(executor.AvatarUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                photos.Add(Imagesbyte, PhotoType);
                Status check = executor.ExecutorPasswordFiles.Select(epf => epf.Status).FirstOrDefault();
                result.Add(new ClientExecutorServiceMdl.ExecutorItem
                {
                    Id = executor.Id,
                    AvatarFile = Imagesbyte,
                    AvatarFileType = PhotoType,
                    Lastname = executor.User.LastName,
                    Firstname = executor.User.FirstName,
                    Fathername = executor.User.FatherName,
                    RegisterDate = (DateTime)executor.RegistrationDateTime,
                    ClosedOrdersCount = (int)executor.ExecutorClosedOrdersCount,
                    Rating = executor.Rating.ToString(),
                    Check = check == Status.Active ? true : false,
                    Online = true

                });
            }
            return result;
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForCategory( int categoryId )
        {
            List<ClientExecutorServiceMdl.ExecutorItem> listitem = new List<ClientExecutorServiceMdl.ExecutorItem>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.Categories.Where(c => c.Id == categoryId).SelectMany(c => c.executorSpecializations).Select(es => es.Executor).ToList();
            Image img;
            string PhotoType = "";
            foreach (var executor in model)
            {
                img = Image.FromFile(executor.AvatarUrl);
                PhotoType = executor.AvatarUrl.Substring(executor.AvatarUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                Status check = executor.ExecutorPasswordFiles.Select(epf => epf.Status).FirstOrDefault();
                int statuscheck = 0;
                switch (check)
                {
                    case Status.Active:
                        statuscheck = (int)Status.Active;
                        break;
                    case Status.Archived:
                        statuscheck = (int)Status.Archived;
                        break;
                    case Status.Deleted:
                        statuscheck = (int)Status.Deleted;
                        break;
                }
                listitem.Add(new ClientExecutorServiceMdl.ExecutorItem
                {
                    Id = executor.Id,
                    Firstname = executor.User.FatherName,
                    Lastname = executor.User.LastName,
                    Fathername = executor.User.FatherName,
                    AvatarFile = Imagesbyte,
                    AvatarFileType = PhotoType,
                    Rating = executor.Rating.ToString(),
                    RegisterDate = (DateTime)executor.RegistrationDateTime,
                    Check = statuscheck == 1 ? true : false,
                    ClosedOrdersCount = (int)executor.ExecutorClosedOrdersCount,
                    Online = true,

                });
            }
            return listitem;
        }

        public List<ClientExecutorServiceMdl.ExecutorItem> GetItemListForSpecialty( int specialtyId )
        {
            List<ClientExecutorServiceMdl.ExecutorItem> listitem = new List<ClientExecutorServiceMdl.ExecutorItem>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.ExecutorSpecializations.Where(es => es.SpecializationId == specialtyId).Select(es => es.Executor).ToList();
            Image img;
            string PhotoType = "";
            foreach (var specialty in model)
            {
                img = Image.FromFile(specialty.AvatarUrl);
                PhotoType = specialty.AvatarUrl.Substring(specialty.AvatarUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                Status check = specialty.ExecutorPasswordFiles.Select(epf => epf.Status).FirstOrDefault();
                int statuscheck = 0;
                switch (check)
                {
                    case Status.Active:
                        statuscheck = (int)Status.Active;
                        break;
                    case Status.Archived:
                        statuscheck = (int)Status.Archived;
                        break;
                    case Status.Deleted:
                        statuscheck = (int)Status.Deleted;
                        break;
                }
                listitem.Add(new ClientExecutorServiceMdl.ExecutorItem
                {
                    Id = specialty.Id,
                    Firstname = specialty.User.FatherName,
                    Lastname = specialty.User.LastName,
                    Fathername = specialty.User.FatherName,
                    AvatarFile = Imagesbyte,
                    AvatarFileType = PhotoType,
                    Rating = specialty.Rating.ToString(),
                    RegisterDate = (DateTime)specialty.RegistrationDateTime,
                    Check = statuscheck == 1 ? true : false,
                    ClosedOrdersCount = (int)specialty.ExecutorClosedOrdersCount,
                    Online = true,

                });
            }
            return listitem;
        }

        public List<ClientExecutorServiceMdl.ExecutorResponse> GetResponseList( string clientId )
        {
            List<ClientExecutorServiceMdl.ExecutorResponse> executorResponses = new List<ClientExecutorServiceMdl.ExecutorResponse>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.Executors.Where(e => e.Id == clientId).ToList();
            Image img;
            string PhotoType = "";
            foreach (var executor in model)
            {
                img = Image.FromFile(executor.AvatarUrl);
                PhotoType = executor.AvatarUrl.Substring(executor.AvatarUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                Status check = executor.ExecutorPasswordFiles.Select(epf => epf.Status).FirstOrDefault();
                int statuscheck = 0;
                switch (check)
                {
                    case Status.Active:
                        statuscheck = (int)Status.Active;
                        break;
                    case Status.Archived:
                        statuscheck = (int)Status.Archived;
                        break;
                    case Status.Deleted:
                        statuscheck = (int)Status.Deleted;
                        break;
                }
                executorResponses.Add(new ClientExecutorServiceMdl.ExecutorResponse
                {
                    Id = executor.Id,
                    Lastname = executor.User.LastName,
                    Firstname = executor.User.FirstName,
                    Fathername = executor.User.FatherName,
                    AvatarFile = Imagesbyte,
                    AvatarFileType = PhotoType,
                    Check = statuscheck == 1 ? true : false,
                    ClosedOrdersCount = (int)executor.ExecutorClosedOrdersCount,
                    CreateAt = executor.Orders.Select(o => o.CreatedDateTime).FirstOrDefault(),
                    ExecutorMessage = executor.Orders.Select(o => o.ExecutorComment).FirstOrDefault(),
                    Online = true,
                    OrderId = executor.Orders.Select(o => o.Id).FirstOrDefault(),
                    Rating = executor.Rating.ToString(),
                    RegisterDate = (DateTime)executor.RegistrationDateTime,
                });
            }
            return executorResponses;
        }
    }
}