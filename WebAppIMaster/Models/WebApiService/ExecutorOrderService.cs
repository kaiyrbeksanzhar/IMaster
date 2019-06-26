using System;
using System.Collections.Generic;
using System.Linq;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enitities.Enums;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class ExecutorOrderService : IExecutorOrderService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ExecutorOrderService( ApplicationDbContext db ) => this.db = db;



        public void CallToClient( string executorId, int orderId )
        {
            CallToClient callToClient = new CallToClient()
            {
                ExecutorId = executorId,
                OrderId = orderId,
                CreatedAt_Date = DateTime.Now
            };
            db.CallToClients.Add(callToClient);
            db.SaveChanges();
        }

        public void CancelOrder( string executorId, int orderId, OrderCancelType cancelType )
        {

            CancelOrder cancelOrder = new CancelOrder()
            {
                ExecutorId = executorId,
                cancelType = cancelType,
                OrderExecutorId = orderId,
                CanceledDateTime = DateTime.Now
            };
            db.CancelOrders.Add(cancelOrder);
            db.SaveChanges();
        }

        public ExecutorOrderMdl.ExecutorOrderDetails GetDetails( int orderId, string userId )
        {
            string langcode = LanguageController.CurrentCultureCode;

            var item = db.CustomerOrders.Where(u => u.Id == orderId).Select(u => new 
            {
                OrderId = u.Id,
                OrderTitle = u.Title,
                ClientId = u.CustomerId,
                ClientFirstName = u.Customer.FirstName,
                ClientPhoneNumber = u.Customer.ApplicationUser.PhoneNumber,
                Bookmark = u.BookmarkOrders.Any(b => b.UserId == userId),
                Responded = u.Responses.Any(r => r.ExecutorId == userId),
                StartDateType = u.StartDateType,
                StartDate = u.StartDateType == Enums.OrderStartDateType.SelectDate ? u.StartedDate : DateTime.Now,
                CostType = u.CostType,
                Cost = u.CostType != Enums.OrderCostType.ByAgreement ? (int)u.Cost : 0,
                OrderStatus = u.OrderState,
                CreateAt = u.CreatedDateTime,

                Description = u.Description,
                RegionId = u.InCityId,
                RegionName = u.InCity.Langs.Where(cl => cl.Langcode == langcode).Select(cl => cl.Name).FirstOrDefault(),
                Address = u.Address.Langs.Where(al => al.Langcode == langcode).Select(al => al.Name).FirstOrDefault(),
                u.Photo1Url,
                u.Photo2Url,
                u.Photo3Url,
                u.Photo4Url,
            }).ToList().Select(u => new ExecutorOrderMdl.ExecutorOrderDetails
            {
                OrderId = u.OrderId,
                OrderTitle = u.OrderTitle,
                ClientId = u.ClientId,
                ClientFirstName = u.ClientFirstName,
                ClientPhoneNumber = u.ClientPhoneNumber,
                Bookmark = u.Bookmark,
                StartDateType = u.StartDateType,
                StartDate = u.StartDate,
                CostType = u.CostType,
                Cost = u.Cost,
                OrderStatus = u.OrderStatus,
                Responded = u.Responded,
                CreateAt = u.CreateAt,

                Description = u.Description,
                RegionId = u.RegionId,
                RegionName = u.RegionName,
                Address = u.Address,
                PhotoUris = new List<string>
                {
                    u.Photo1Url == null ? null : "http://i-master.kz/api/GetOrderPhoto?url=" + u.Photo1Url,
                    u.Photo2Url == null ? null : "http://i-master.kz/api/GetOrderPhoto?url=" + u.Photo2Url,
                    u.Photo3Url == null ? null : "http://i-master.kz/api/GetOrderPhoto?url=" + u.Photo3Url,
                    u.Photo4Url == null ? null : "http://i-master.kz/api/GetOrderPhoto?url=" + u.Photo4Url,
                },
            }).SingleOrDefault();
            item.PhotoUris = item.PhotoUris.Where(u => u != null);
            return item;
        }

        public List<ExecutorOrderMdl.ExecutorOrderItem> GetItemList( List<int> CategoryIds, List<int> SpecializationIds, string userId)
        {
            IQueryable<CustomerOrder> query = db.CustomerOrders;
            if (CategoryIds != null && 0 < CategoryIds.Count)
            {
                int[] ids = CategoryIds.ToArray();
                query = query.Where(u => ids.Any(id => id == u.Specialization.CategoryId));
            }
            if (SpecializationIds != null && 0 < SpecializationIds.Count)
            {
                int[] ids = SpecializationIds.ToArray();
                query = query.Where(u => ids.Any(id => id == u.SpecializationId));
            }

            List<ExecutorOrderMdl.ExecutorOrderItem> list = query.OrderByDescending(u => u.CreatedDateTime).Select(u => new ExecutorOrderMdl.ExecutorOrderItem
            {
                OrderId = u.Id,
                OrderTitle = u.Title,
                ClientId = u.CustomerId,
                ClientFirstName = u.Customer.ApplicationUser.FirstName,
                Bookmark = db.BookmarkOrders.Any(b => b.OrderId == u.Id && b.UserId == userId),
                Responded = u.Responses.Any(r => r.ExecutorId == userId), 
                StartDateType = u.StartDateType,
                StartDate = u.StartDateType == Enums.OrderStartDateType.SelectDate ? u.StartedDate : DateTime.Now,
                CostType = u.CostType,
                Cost = u.CostType == Enums.OrderCostType.ByAgreement ? 0 : (int)u.Cost,
                OrderStatus = u.OrderState,
            }).ToList();

            return list;
        }

        public void Response( string executorId, int orderId, string responseComment )
        {
            Response responsies = new Response()
            {
                OrderId = orderId,
                ExecutorId = executorId,
                ResponseComment = responseComment,
                CreatedAt_Date = DateTime.Now,
            };
            db.Responses.Add(responsies);
            db.SaveChanges();
        }

        public void RemoveBookmark( string userId,int orderId )
        {
            var item = (from b in db.BookmarkOrders
                        where b.UserId == userId
                        where b.OrderId == orderId
                        select b).SingleOrDefault(); ;
            db.BookmarkOrders.Remove(item);
            db.SaveChanges();
        }
        public void AddBookmark(string userId, int orderId )
        {
            BookmarkOrder bookmark = new BookmarkOrder()
            {
                UserId = userId,
                OrderId = orderId,
            };
            db.BookmarkOrders.Add(bookmark);
            db.SaveChanges();
        }
    }
}