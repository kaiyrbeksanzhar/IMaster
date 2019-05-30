using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        public ClientOrderDetailsView GetClientOrderDetailsView(int id)
        {
            var model = db.CustomerOrders.Find(id);
            string langcode = LanguageController.CurrentCultureCode;
            return new ClientOrderDetailsView
            {
                Id = model.Id,
                Title = model.Title,
                OrderNumber = model.OrderNumber,
                OrderCostType = model.CostType,
                StartedDate = model.StartedDate,
                OrderStartDateType = model.StartDateType,
                CategoryAndSpecialization = model.CategoryAndSpecialization.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                CategoryAndSpecializationId = model.CategoryAndSpecializationId,
                Address = model.Address.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                Cost = (int)model.Cost,
                Description = model.Description,
                Region = model.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                ExecotorRating = model.Executor.Rating.ToString(),
                ExecutorId = model.ExecutorId.ToString(),
                ExecoturBirthday = model.Executor.BirthDay,
                ExecutorLastname = model.Executor.User.LastName,
                ExecutorFathername = model.Executor.User.FatherName,
                ExecutorFirstname = model.Executor.User.FirstName,
                ExecutorClosedOrdersCount = model.Executor.ExecutorClosedOrdersCount,
                RegionId = model.InCityId,
                ExecutorGenderId = (int)model.Executor.Gender,
                ExecutorCheck = model.Executor.ExecutorCheck,
                PayWithBounce = model.PayWithBounce,
                ExecutorAvatarFile = System.IO.File.ReadAllBytes(model.Executor.ExecutorPhotoFiles.Where(epf => epf.ExecutorId == model.ExecutorId).Select(epf => epf.PhotoFileUrl).FirstOrDefault()),
                ExecutorRegisterDate = model.Executor.RegistrationDateTime,
                Photos = new List<ClientOrderDetailsView.Photo>
                {
                    new ClientOrderDetailsView.Photo
                    {
                        PhotoUrl1 = System.IO.File.ReadAllBytes(model.Photo1Url),
                        PhotoUrl2 = System.IO.File.ReadAllBytes(model.Photo2Url),
                        PhotoUrl3 = System.IO.File.ReadAllBytes(model.Photo3Url),
                        PhotoUrl4 = System.IO.File.ReadAllBytes(model.Photo4Url)
                    }
                },
                ReceiveOnlyResponses = model.ReceiveOnlyResponses,
                ExecotorOnline = model.Executor.ExecotorOnline
            };
        }

        public ClientOrderItemView GetClientOrderItemView(int id)
        {
            var model = db.CustomerOrders.Find(id);
            return new ClientOrderItemView
            {
                Id = model.Id,
                StartingDatetime = model.StartedDate.ToShortDateString(),
                Category = model.CategoryAndSpecialization.Langs.Where(l => l.Langcode == LanguageController.CurrentCultureCode).FirstOrDefault().Name,
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
                    Category = item.CategoryAndSpecialization.Langs.Where(l=>l.Langcode == langcode).Select(l=>l.Name).FirstOrDefault(),
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

        public int Create(ClientOrderCreate model)
        {
            CustomerOrder customerOrder = new CustomerOrder()
            {
                Title = model.Title,
                Description = model.Description,
                Bonus = model.Bonus,
                OrderState = Enums.OrderState.Open,
                StartDateType = model.StartDateType,
                StartedDate = model.StartedDate,
                CategoryAndSpecializationId = model.CategoryAndSpecializationId,
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
                Photo1Url = model.Photos.Select(p => p.PhotoUrl1).FirstOrDefault(),
                Photo2Url = model.Photos.Select(p => p.PhotoUrl2).FirstOrDefault(),
                Photo3Url = model.Photos.Select(p => p.PhotoUrl3).FirstOrDefault(),
                Photo4Url = model.Photos.Select(p => p.PhotoUrl4).FirstOrDefault(),
                 
            };
            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();
            return customerOrder.Id;
        }
    }
}