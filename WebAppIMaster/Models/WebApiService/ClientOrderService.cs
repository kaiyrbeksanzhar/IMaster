using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public HttpResponseMessage Create( ClientOrderCreate model )
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
                Photo4Url = model.Photos.Select(p => p.PhotoUrl4).FirstOrDefault()
            };
            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public int Edit( ClientOrderEdit model )
        {
            try
            {
                var result = db.CustomerOrders.Find(model.Id);
                CustomerOrder customerOrder = new CustomerOrder()
                {
                    Id = model.Id,
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
                    Photo4Url = model.Photos.Select(p => p.PhotoUrl4).FirstOrDefault()
                };
                db.Entry(customerOrder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return result.Id;
            }
            catch
            {
                return -1;
            }

        }

        public ClientOrderItemView Get( int id )
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
        
    }
}