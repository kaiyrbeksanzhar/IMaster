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
            CustomerOrder model = db.CustomerOrders.Where(co => co.Id == id).SingleOrDefault();
            //var al = model.Address.Langs.Where(l => l.Langcode == langcode & l.AddressId == model.AddressId).FirstOrDefault();

            var adreslang = (from als in db.AddressLangs

                             where als.Langcode == langcode
                             where als.AddressId == model.AddressId

                             select als).SingleOrDefault();

            ClientOrderDetailsView clientOrderDetailsView = new ClientOrderDetailsView();
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            if (string.IsNullOrWhiteSpace(model.Photo1Url) == false)
            {
                string PhotoType = model.Photo1Url.Substring(model.Photo1Url.LastIndexOf(".") + 1);
                photos.Add(System.IO.File.ReadAllBytes(model.Photo1Url), PhotoType);
            }
            if (string.IsNullOrWhiteSpace(model.Photo2Url) == false)
            {
                string Photo1Type = model.Photo2Url.Substring(model.Photo2Url.LastIndexOf(".") + 1);
                photos.Add(System.IO.File.ReadAllBytes(model.Photo2Url), Photo1Type);
            }
            if (string.IsNullOrWhiteSpace(model.Photo3Url) == false)
            {
                string Photo2Type = model.Photo3Url.Substring(model.Photo3Url.LastIndexOf(".") + 1);
                photos.Add(System.IO.File.ReadAllBytes(model.Photo3Url), Photo2Type);
            }

            if (string.IsNullOrWhiteSpace(model.Photo4Url) == false)
            {
                string Photo3Type = model.Photo4Url.Substring(model.Photo4Url.LastIndexOf(".") + 1);
                photos.Add(System.IO.File.ReadAllBytes(model.Photo4Url), Photo3Type);
            }
            var item = new ClientOrderDetailsView
            {
                Id = model.Id,
                Title = model.Title,
                OrderNumber = model.OrderNumber,
                OrderCostType = model.CostType,
                StartedDate = model.StartedDate,
                OrderStartDateType = model.StartDateType,
                CategoryName = model.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                CategoryId = model.CategoryId,
                Address = adreslang.Name,
                Cost = (int)model.Cost,
                Description = model.Description,
                Region = model.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                RegionId = model.InCityId,
                Photos = photos,
                PayWithBounce = model.PayWithBounce,
                ReceiveOnlyResponses = model.ReceiveOnlyResponses,
                CreatedAt = model.CreatedDateTime,
                OrderType = model.OrderType,
            };
            if (model.ExecutorId != null)
            {
                item.ExecutorLastname = model.Executor.User.LastName;
                item.ExecutorFirstname = model.Executor.User.FirstName;
                item.ExecutorFathername = model.Executor.User.FatherName;
                if (model.Executor.AvatarUrl == null)
                {
                    item.ExecutorAvatarFile = null;
                    item.ExecotorAvatarFileType = null;
                }
                else
                {
                    item.ExecutorAvatarFile = System.IO.File.ReadAllBytes(model.Executor.AvatarUrl);
                    item.ExecotorAvatarFileType = model.Photo1Url.Substring(model.Executor.AvatarUrl.LastIndexOf(".") + 1);
                }
                item.ExecotorOnline = (bool)model.Executor.ExecotorOnline;
                item.ExecotorRating = model.Executor.Rating.ToString();
                item.ExecoturBirthday = (DateTime)model.Executor.BirthDay;
                item.ExecutorCheck = (bool)model.Executor.ExecutorCheck;
                item.ExecutorClosedOrdersCount = (int)model.Executor.ExecutorClosedOrdersCount;
                item.ExecutorGenderName = model.Executor.Gender.Value.ToString();
                item.ExecutorRegionId = model.Executor.User.RegionId;
                item.ExecutorRegisterDate = (DateTime)model.Executor.RegistrationDateTime;
            }

            return item;
        }

        public ClientOrderItemView GetClientOrderItemView( int id )
        {
            CustomerOrder model = db.CustomerOrders.Find(id);
            return new ClientOrderItemView
            {
                Id = model.Id,
                StartingDatetime = model.StartedDate.ToShortDateString(),
                CategoryName = model.Category.Langs.Where(l => l.Langcode == LanguageController.CurrentCultureCode).FirstOrDefault().Name,
                Cost = Convert.ToInt32(model.Cost),
                Title = model.Title,
                Type = model.OrderType
            };
        }

        public List<ClientOrderItemView> GetList()
        {
            List<ClientOrderItemView> clientOrderItemViews = new List<ClientOrderItemView>();
            var header = (from co in db.CustomerOrders

                          select co).ToList();
            //var model = db.CustomerOrders.ToList();
            string langcode = LanguageController.CurrentCultureCode;
            foreach (var item in header)
            {
                clientOrderItemViews.Add(new ClientOrderItemView
                {
                    CategoryName = item.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
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

        public int Create( ClientOrderCreate model )
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
                StartedDate = (DateTime)model.StartedDate,
                CategoryId = model.CategoryId == 0 ? 1 : model.CategoryId,
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

                        select co).ToList();
            //var model = db.CustomerOrders.Where(co => co.CustomerId == clientId).ToList();
            foreach (var clientorder in item)
            {
                clientOrders.Add(new ClientOrderItemView
                {
                    Id = clientorder.Id,
                    Cost = (int)clientorder.Cost,
                    CategoryName = clientorder.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
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
                        select new
                        {
                            Id = cd.Id,
                            Description = cd.Description,
                            StartedDate = cd.StartedDate,
                            CateogoryId = cd.CategoryId,
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
                            Category = cd.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
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
                    File = System.IO.File.ReadAllBytes(item.Photourl1),
                    Type = item.Photourl1.Substring(item.Photourl1.LastIndexOf(".") + 1),
                });
            }
            if (string.IsNullOrWhiteSpace(item.Photourl2) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    File = System.IO.File.ReadAllBytes(item.Photourl2),
                    Type = item.Photourl2.Substring(item.Photourl2.LastIndexOf(".") + 1),
                });
            }
            if (string.IsNullOrWhiteSpace(item.Photourl3) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    File = System.IO.File.ReadAllBytes(item.Photourl3),
                    Type = item.Photourl3.Substring(item.Photourl3.LastIndexOf(".") + 1),
                });
            }
            if (string.IsNullOrWhiteSpace(item.Photourl4) == false)
            {
                photos.Add(new ClientCommonOrderDetailsView.Photo
                {
                    File = System.IO.File.ReadAllBytes(item.Photourl4),
                    Type = item.Photourl4.Substring(item.Photourl4.LastIndexOf(".") + 1),
                });
            }
            

            return new ClientCommonOrderDetailsView
            {
                Id = item.Id,
                Description = item.Description,
                StartedDate = item.StartedDate,
                CategoryId = item.CateogoryId,
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
                Category = item.Category,
                CreateAt = item.CreateAt,
                Photos = photos
            };
        }
    }
}