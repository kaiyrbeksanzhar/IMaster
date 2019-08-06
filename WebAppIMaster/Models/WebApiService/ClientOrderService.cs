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
using WebAppIMaster.Models.Enums;
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
            string langcode = LanguageController.CurrentCultureCode;

            var item = db.CustomerOrders.Where(u => u.Id == id).Select(u => new
            {
                Id = u.Id,
                Title = u.Title,
                OrderNumber = u.OrderNumber,
                OrderCostType = u.CostType,
                StartedDate = u.StartedDate,
                OrderStartDateType = u.StartDateType,
                SpecializationName = u.Specialization.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                SpecializationId = u.SpecializationId,
                Address = u.Address.Langs.Where(a => a.Langcode == langcode).Select(a => a.Name).FirstOrDefault(),
                Cost = (int)u.Cost,
                Description = u.Description,
                Region = u.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                RegionId = u.InCityId,
                PhotoUris = new List<string>
                {
                    u.Photo1Url == null ? null : "http://i-master.kz/api/GetClientOrderPhoto?url=" + u.Photo1Url,
                    u.Photo2Url == null ? null : "http://i-master.kz/api/GetClientOrderPhoto?url=" + u.Photo2Url,
                    u.Photo3Url == null ? null : "http://i-master.kz/api/GetClientOrderPhoto?url=" + u.Photo3Url,
                    u.Photo4Url == null ? null : "http://i-master.kz/api/GetClientOrderPhoto?url=" + u.Photo4Url,
                },
                PayWithBounce = u.PayWithBounce,
                ReceiveOnlyResponses = u.ReceiveOnlyResponses,
                CreatedAt = u.CreatedDateTime,
                OrderType = u.OrderType,

                Executor = u.ExecutorId == null ? null : new
                {
                    ExecutorId = u.ExecutorId,
                    ExecutorLastname = u.Executor.User.LastName,
                    ExecutorFirstname = u.Executor.User.FirstName,
                    ExecutorUri = u.Executor.AvatarUrl == null ? null : "http://i-master.kz/api/GetOrderPhoto?url=" + u.Executor.AvatarUrl,
                    ExecotorOnline = (bool)u.Executor.ExecotorOnline,
                    ExecotorRating = u.Executor.Rating == null ? "0" : u.Executor.Rating.ToString(),
                    ExecoturBirthday = (DateTime)u.Executor.BirthDay,
                    ExecutorCheck = (bool)u.Executor.ExecutorCheck,
                    ExecutorClosedOrdersCount = u.Executor.Orders.Where(o => o.OrderState == OrderState.Finished).Count(),
                    ExecutorGenderName = u.Executor.Gender.Value.ToString(),
                    ExecutorRegionId = u.Executor.CityId,
                    ExecutorRegionName = u.Executor.City.Langs.Where(cl => cl.Langcode == langcode).Select(cl => cl.Name).FirstOrDefault(),
                    ExecutorRegisterDate = (DateTime)u.Executor.RegistrationDateTime,
                },
            }).ToList().Select(u => new ClientOrderDetailsView
            {
                Id = u.Id,
                Title = u.Title,
                OrderNumber = u.OrderNumber,
                OrderCostType = u.OrderCostType,
                StartedDate = u.StartedDate,
                OrderStartDateType = u.OrderStartDateType,
                SpecializationName = u.SpecializationName,
                SpecializationId = u.SpecializationId,
                Address = u.Address,
                Cost = u.Cost,
                Description = u.Description,
                Region = u.Region,
                RegionId = u.RegionId,
                PhotoUris = u.PhotoUris,
                PayWithBounce = u.PayWithBounce,
                ReceiveOnlyResponses = u.ReceiveOnlyResponses,
                CreatedAt = u.CreatedAt,
                OrderType = u.OrderType,

                ExecutorId = u.Executor?.ExecutorId,
                ExecutorLastname = u.Executor?.ExecutorLastname,
                ExecutorFirstname = u.Executor?.ExecutorFirstname,
                ExecotorAvatarUri = u.Executor?.ExecutorUri,
                ExecotorOnline = u.Executor?.ExecotorOnline ?? false,
                ExecotorRating = u.Executor?.ExecotorRating,
                ExecoturBirthday = u.Executor?.ExecoturBirthday ?? DateTime.Now,
                ExecutorCheck = u.Executor?.ExecutorCheck ?? false,
                ExecutorClosedOrdersCount = u.Executor?.ExecutorClosedOrdersCount ?? 0,
                ExecutorGenderName = u.Executor?.ExecutorGenderName,
                ExecutorRegionId = u.Executor?.ExecutorRegionId ?? 0,
                ExecutorRegionName = u.Executor?.ExecutorRegionName,
                ExecutorRegisterDate = u.Executor?.ExecutorRegisterDate ?? DateTime.Now,
            }).SingleOrDefault();
            item.PhotoUris = item.PhotoUris.Where(u => u != null);

            return item;
        }

        public ClientOrderItemView GetClientOrderItemView( int id )
        {
            CustomerOrder model = db.CustomerOrders.Find(id);
            return new ClientOrderItemView
            {
                Id = model.Id,
                StartingDatetime = model.StartedDate.ToShortDateString(),
                SpecializationName = model.Specialization.Langs.Where(l => l.Langcode == LanguageController.CurrentCultureCode).FirstOrDefault().Name,
                Cost = Convert.ToInt32(model.Cost),
                Title = model.Title,
                Type = model.OrderType
            };
        }

        public List<ClientOrderItemView> GetList()
        {
            List<ClientOrderItemView> clientOrderItemViews = new List<ClientOrderItemView>();
            var header = (from co in db.CustomerOrders
                          where co.OrderState == OrderState.Open
                          where co.OrderType == Enitities.Enums.OrderStatus.Published
                          orderby co.CreatedDateTime descending
                          select co).ToList();
            //var model = db.CustomerOrders.ToList();
            string langcode = LanguageController.CurrentCultureCode;
            foreach (var item in header)
            {
                clientOrderItemViews.Add(new ClientOrderItemView
                {
                    SpecializationName = item.Specialization.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                    Cost = (int)item.Cost,
                    StartingDatetime = item.StartedDate.ToShortDateString(),
                    Id = item.Id,
                    NewNotifications = item.NewNotifications,
                    Title = item.Title,
                    Type = item.OrderType
                });
            }
            return clientOrderItemViews;
        }


        public bool ClientOrderEdit( ClientOrderEdit model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            string langcode = LanguageController.CurrentCultureCode;
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            try
            {
                var clientOrder = db.CustomerOrders.Where(co => co.Id == model.Id).SingleOrDefault();
                clientOrder.Title = model.Title;
                clientOrder.Description = model.Description;
                clientOrder.StartDateType = model.OrderStartDateType;
                clientOrder.Cost = model.Cost;
                clientOrder.CostType = model.OrderCostType;
                clientOrder.InCityId = model.RegionId;
                clientOrder.ReceiveOnlyResponses = model.ReceiveOnlyResponses;
                clientOrder.SpecializationId = model.CategoryAndSpecializationId;
                clientOrder.PayWithBounce = model.PayWithBounce;
                clientOrder.Address = db.CustomerOrders.Select(co => co.Address).FirstOrDefault();
                clientOrder.Address.Langs = db.CustomerOrders.SelectMany(co => co.Address.Langs).ToList();
                clientOrder.Address.Langs.Where(l => l.Langcode == lang_kz).FirstOrDefault().Name = model.Address;
                clientOrder.Address.Langs.Where(l => l.Langcode == lang_ru).FirstOrDefault().Name = model.Address;
                db.Entry(clientOrder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


        public int Create( ClientOrderCreate model )
        {
            Dictionary<int, string> photos = new Dictionary<int, string>();
            int i = 1;
            if (model.Photos != null)
            {
                foreach (var item in model.Photos)
                {
                    MemoryStream ms = new MemoryStream(item.Key);
                    Image img = Image.FromStream(ms);
                    string url = FileManager.SavePhoto(img);
                    photos.Add(i, url);
                    i++;
                }
            }
            CustomerOrder customerOrder = new CustomerOrder()
            {
                Title = model.Title,
                Description = model.Description,
                OrderState = Enums.OrderState.Open,
                StartDateType = model.StartDateType,
                StartedDate = (DateTime)model.StartedDate == DateTime.MinValue ? DateTime.Now : (DateTime)model.StartedDate,
                SpecializationId = model.SpecializationId == 0 ? 1 : model.SpecializationId,
                CostType = model.CostType,
                Cost = model.Cost,
                CreatedDateTime = DateTime.Now,
                InCityId = model.RegionId == -1 || model.RegionId == 0 ? 1 : model.RegionId,
                ReceiveOnlyResponses = model.ReceiveOnlyResponses,
                CustomerId = model.CustomerId,
                EndedDateTime = DateTime.Now,
                CustomerReviewedDateTime = DateTime.Now,
                ExecutorCommentedDateTime = DateTime.Now,
                PayWithBounce = model.PayWithBounce,
                OrderType = Enitities.Enums.OrderStatus.Published,
                Bonus = 0,
                OrderNumber = "N1",
                Address = new Address
                {
                    Langs = new List<AddressLang>
                         {

                             new AddressLang
                             {
                                 Name = model.Address,
                                 Langcode = LanguageController.GetRuCode()
                             },
                             new AddressLang
                             {

                                 Name = model.Address,
                                 Langcode = LanguageController.GetKzCode()
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
            var item = (from co in db.CustomerOrders

                        where co.CustomerId == clientId
                        orderby co.CreatedDateTime descending
                        select co).ToList();
            //var model = db.CustomerOrders.Where(co => co.CustomerId == clientId).ToList();
            foreach (var clientorder in item)
            {
                clientOrders.Add(new ClientOrderItemView
                {
                    Id = clientorder.Id,
                    Cost = (int)clientorder.Cost,
                    SpecializationName = clientorder.Specialization.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                    StartingDatetime = clientorder.StartedDate.ToShortDateString() + " " + clientorder.StartedDate.ToShortTimeString(),
                    NewNotifications = clientorder.NewNotifications,
                    Title = clientorder.Title,
                    Type = clientorder.OrderType
                });
            }
            return clientOrders;
        }

        public List<ClientOrderItemView> GetListForExecutor( string executorId )
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ClientOrderItemView> clientOrders = new List<ClientOrderItemView>();
            var model = db.CustomerOrders.Where(co => co.ExecutorId == executorId).OrderByDescending(co => co.CreatedDateTime).ToList();
            foreach (var clientorder in model)
            {
                clientOrders.Add(new ClientOrderItemView
                {
                    Id = clientorder.Id,
                    Cost = (int)clientorder.Cost,
                    SpecializationName = clientorder.Specialization.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                    StartingDatetime = clientorder.StartedDate.ToShortDateString() + " " + clientorder.StartedDate.ToShortTimeString(),
                    NewNotifications = clientorder.NewNotifications,
                    Title = clientorder.Title,
                    Type = clientorder.OrderType
                });
            }
            return clientOrders;
        }

        public ClientCommonOrderDetailsView GetClientCommonOrderDetailsView( int orderId )
        {
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from cd in db.CustomerOrders
                        where cd.Id == orderId
                        let specialization = cd.Specialization.Langs.Where(l => l.Langcode == langcode).Select(l => new { Specialization = l.Name, l.SpecializationId, l.Specialization.CategoryId, CategoryName = l.Specialization.Category.Langs.Where(cl => cl.Langcode == langcode).Select(cl => cl.Name).FirstOrDefault() }).FirstOrDefault()
                        select new
                        {
                            Id = cd.Id,
                            Description = cd.Description,
                            StartedDate = cd.StartedDate,
                            SpecializationId = cd.SpecializationId,
                            Address = cd.Address.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            OrderNumber = cd.OrderNumber,
                            Cost = (int)cd.Cost,
                            OrderCostType = cd.CostType,
                            OrderType = cd.OrderType,
                            OrderStartDateType = cd.StartDateType,
                            Region = cd.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            RegionId = cd.InCityId,
                            Title = cd.Title,
                            ViewCount = cd.ViewCount,
                            specialization,
                            CreateAt = cd.CreatedDateTime,
                            Photourl1 = cd.Photo1Url,
                            Photourl2 = cd.Photo2Url,
                            Photourl3 = cd.Photo3Url,
                            Photourl4 = cd.Photo4Url,
                        }).SingleOrDefault();

            List<ClientCommonOrderDetailsView.Photo> photos = new List<ClientCommonOrderDetailsView.Photo>();
            if (string.IsNullOrWhiteSpace(item.Photourl1) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    Uri = "http://i-master.kz/api/GetClientOrderPhoto?url=" + item.Photourl1,
                });
            }
            if (string.IsNullOrWhiteSpace(item.Photourl2) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    Uri = "http://i-master.kz/api/GetClientOrderPhoto?url=" + item.Photourl2,
                });
            }
            if (string.IsNullOrWhiteSpace(item.Photourl3) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    Uri = "http://i-master.kz/api/GetClientOrderPhoto?url=" + item.Photourl3,
                });
            }
            if (string.IsNullOrWhiteSpace(item.Photourl4) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    Uri = "http://i-master.kz/api/GetClientOrderPhoto?url=" + item.Photourl4,
                });
            }


            return new ClientCommonOrderDetailsView
            {
                Id = item.Id,
                Description = item.Description,
                StartedDate = item.StartedDate,
                SpecializationId = item.SpecializationId,
                Address = item.Address,
                OrderNumber = item.OrderNumber,
                Cost = item.Cost,
                OrderCostType = item.OrderCostType,
                OrderType = item.OrderType,
                OrderStartDateType = item.OrderStartDateType,
                Region = item.Region,
                RegionId = item.RegionId,
                Title = item.Title,
                ViewCount = item.ViewCount,
                CategoryId = item.specialization.CategoryId,
                CategoryName = item.specialization.CategoryName,
                SpecializationName = item.specialization.Specialization,
                CreateAt = item.CreateAt,
                Photos = photos
            };
        }

        public void SendPhotoToOrder( string url, int orderId )
        {
            var CustomerOrder = db.CustomerOrders.Where(co => co.Id == orderId).FirstOrDefault();
            if (CustomerOrder != null)
            {
                if (CustomerOrder.Photo1Url == null)
                {
                    CustomerOrder.Photo1Url = url;
                }
                else if (CustomerOrder.Photo2Url == null)
                {
                    CustomerOrder.Photo2Url = url;
                }
                else if (CustomerOrder.Photo3Url == null)
                {
                    CustomerOrder.Photo3Url = url;
                }
                else if (CustomerOrder.Photo4Url == null)
                {
                    CustomerOrder.Photo4Url = url;
                }
            }
            db.Entry(CustomerOrder).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public bool ClientMyOrderFinish( ClientMyOrderFinish item )
        {
            try
            {
                CustomerOrder order = db.CustomerOrders.Find(item.OrderId);
                order.OrderState = OrderState.Finished;
                order.OrderType = Enitities.Enums.OrderStatus.Closed;
                order.CustomerReviewedDateTime = item.ReviewedAt;
                order.CustomerReview = item.ReviewText;
                order.EndedDateTime = DateTime.Now;

                // TODO:FinishReason
                order.EvaluationPerformerWork = (EvaluationPerformerWork)item.StarsCountOf5;
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelClientMyOrder( int orderId, CancelReason cancelReason )
        {
            try
            {
                CustomerOrder order = db.CustomerOrders.Find(orderId);
                order.OrderState = OrderState.Canceled;
                order.OrderType = Enitities.Enums.OrderStatus.Closed;
                order.EndedDateTime = DateTime.Now;
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePhotoUrlAddress( string url, int orderId )
        {
            try
            {
                var item = db.CustomerOrders.Find(orderId);
                if (item.Photo1Url == url)
                {
                    item.Photo1Url = null;
                }
                if (item.Photo2Url == url)
                {
                    item.Photo2Url = null;
                }
                if (item.Photo3Url == url)
                {
                    item.Photo3Url = null;
                }
                if (item.Photo4Url == url)
                {
                    item.Photo4Url = null;
                }
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}