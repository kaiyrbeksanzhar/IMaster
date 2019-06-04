using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class ClientOrderService : IClientOrder
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ClientOrderService( ApplicationDbContext db ) => this.db = db;

        public ClientOrderDetailsView GetClientOrderDetailsView( int id )
        {
            var model = db.CustomerOrders.Find(id);
            string langcode = LanguageController.CurrentCultureCode;
            ClientOrderDetailsView clientOrderDetailsView = new ClientOrderDetailsView();
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            string PhotoType = model.Photo1Url.Substring(model.Photo1Url.LastIndexOf(".") + 1);
            photos.Add(System.IO.File.ReadAllBytes(model.Photo1Url), PhotoType);
            string Photo1Type = model.Photo2Url.Substring(model.Photo2Url.LastIndexOf(".") + 1);
            photos.Add(System.IO.File.ReadAllBytes(model.Photo2Url), Photo1Type);
            string Photo2Type = model.Photo3Url.Substring(model.Photo3Url.LastIndexOf(".") + 1);
            photos.Add(System.IO.File.ReadAllBytes(model.Photo3Url), Photo2Type);
            string Photo3Type = model.Photo4Url.Substring(model.Photo4Url.LastIndexOf(".") + 1);
            photos.Add(System.IO.File.ReadAllBytes(model.Photo4Url), Photo3Type);
            return new ClientOrderDetailsView
            {
                Id = model.Id,
                Title = model.Title,
                OrderNumber = model.OrderNumber,
                OrderCostType = model.CostType,
                StartedDate = model.StartedDate,
                OrderStartDateType = model.StartDateType,
                CategoryName = model.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                CategoryId = model.CategoryId,
                Address = model.Address.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                Cost = (int)model.Cost,
                Description = model.Description,
                Region = model.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                ExecotorRating = model.Executor.Rating.ToString(),
                ExecutorId = model.ExecutorId.ToString(),
                ExecoturBirthday = (DateTime)model.Executor.BirthDay,
                ExecutorLastname = model.Executor.User.LastName,
                ExecutorFathername = model.Executor.User.FatherName,
                ExecutorFirstname = model.Executor.User.FirstName,
                ExecutorClosedOrdersCount = (int)model.Executor.ExecutorClosedOrdersCount,
                RegionId = model.InCityId,
                Photos = photos,
                ExecutorGenderId = (int)model.Executor.Gender,
                ExecutorCheck = (bool)model.Executor.ExecutorCheck,
                PayWithBounce = model.PayWithBounce,
                ExecutorAvatarFile = System.IO.File.ReadAllBytes(model.Executor.ExecutorPhotoFiles.Where(epf => epf.ExecutorId == model.ExecutorId).Select(epf => epf.PhotoFileUrl).FirstOrDefault()),
                ExecutorRegisterDate = (DateTime)model.Executor.RegistrationDateTime,
                ReceiveOnlyResponses = model.ReceiveOnlyResponses,
                ExecotorOnline = (bool)model.Executor.ExecotorOnline
            };
        }

        public ClientOrderItemView GetClientOrderItemView( int id )
        {
            var model = db.CustomerOrders.Find(id);
            return new ClientOrderItemView
            {
                Id = model.Id,
                StartingDatetime = model.StartedDate.ToShortDateString(),
                CategoryName = model.Category.Langs.Where(l => l.Langcode == LanguageController.CurrentCultureCode).FirstOrDefault().Name,
                Cost = Convert.ToInt32(model.Cost),
                Title = model.Title,
                Type = model.OrderState
            };
        }

        public List<ClientOrderItemView> GetList()
        {
            List<ClientOrderItemView> clientOrderItemViews = new List<ClientOrderItemView>();
            var model = db.CustomerOrders.ToList();
            string langcode = LanguageController.CurrentCultureCode;
            foreach (var item in model)
            {
                clientOrderItemViews.Add(new ClientOrderItemView
                {
                    CategoryName = item.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                    Cost = (int)item.Cost,
                    StartingDatetime = item.StartedDate.ToShortDateString(),
                    Id = item.Id,
                    NewNotifications = item.NewNotifications,
                    Title = item.Title,
                    Type = item.OrderState
                });
            }
            return clientOrderItemViews;
        }

        public int Create(ClientOrderCreate model )
        {
            Dictionary<int, string> photos = new Dictionary<int, string>();
            int i = 1;
            foreach (var item in model.Photos)
            {
                MemoryStream ms = new MemoryStream(item.Key);
                Image img = Image.FromStream(ms);
                string url = FileManager.SavePhoto(img);
                photos.Add(i, url);
                i++;
            }
            CustomerOrder customerOrder = new CustomerOrder()
            {
                Title = model.Title,
                Description = model.Description,
                OrderState = Enums.OrderState.Open,
                StartDateType = model.StartDateType,
                StartedDate = model.StartedDate,
                CategoryId = model.CategoryId,
                Cost = model.Cost,
                CreatedDateTime = DateTime.Now,
                InCityId = model.RegionId,
                ReceiveOnlyResponses = model.ReceiveOnlyResponses,
                CustomerId = model.CustomerId,
                Address = new Enitities.Address
                {
                    Langs = new List<AddressLang>
                         {

                             new AddressLang
                             {
                                 Name = model.Address,
                                 Langcode = LanguageController.GetRuCode()
                             }
                         },
                },
                Photo1Url = photos.Where(p => p.Key == 1).Select(p => p.Value).FirstOrDefault(),
                Photo2Url = photos.Where(p => p.Key == 2).Select(p => p.Value).FirstOrDefault(),
                Photo3Url = photos.Where(p => p.Key == 3).Select(p => p.Value).FirstOrDefault(),
                Photo4Url = photos.Where(p => p.Key == 4).Select(p => p.Value).FirstOrDefault(),
            };
            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();
            return customerOrder.Id;
        }

        public List<ClientOrderItemView> GetListForClient( string clientId )
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientOrderItemView> clientOrders = new List<ClientOrderItemView>();
            var model = db.CustomerOrders.Where(co => co.CustomerId == clientId).ToList();
            foreach(var clientorder in model)
            {
                clientOrders.Add(new ClientOrderItemView
                {
                    Id = clientorder.Id,
                    Cost = (int)clientorder.Cost,
                    CategoryName = clientorder.Category.Langs.Where(l=>l.Langcode==langcode).Select(l=>l.Name).FirstOrDefault(),
                    StartingDatetime = clientorder.StartedDate.ToShortDateString() +" "+clientorder.StartedDate.ToShortTimeString(),
                    NewNotifications = clientorder.NewNotifications,
                    Title = clientorder.Title,
                    Type = clientorder.OrderState
                });
            }
            return clientOrders;
        }

        public List<ClientOrderItemView> GetListForExecutor( string executorId )
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientOrderItemView> clientOrders = new List<ClientOrderItemView>();
            var model = db.CustomerOrders.Where(co => co.ExecutorId == executorId).ToList();
            foreach (var clientorder in model)
            {
                clientOrders.Add(new ClientOrderItemView
                {
                    Id = clientorder.Id,
                    Cost = (int)clientorder.Cost,
                    CategoryName = clientorder.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                    StartingDatetime = clientorder.StartedDate.ToShortDateString() + " " + clientorder.StartedDate.ToShortTimeString(),
                    NewNotifications = clientorder.NewNotifications,
                    Title = clientorder.Title,
                    Type = clientorder.OrderState
                });
            }
            return clientOrders;
        }
    }
}