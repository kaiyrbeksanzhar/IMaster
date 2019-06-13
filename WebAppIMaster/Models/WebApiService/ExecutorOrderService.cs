using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                OrderExecutorId = orderId,
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

        public ExecutorOrderMdl.ExecutorOrderDetails GetDetails( int orderId )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.orderExecutors.Where(oe => oe.OrderId == orderId).FirstOrDefault();
            var customer = db.Customers.Where(c => c.Id == model.CustomerId).FirstOrDefault();
            return new ExecutorOrderMdl.ExecutorOrderDetails
            {
                OrderId = model.OrderId,
                ClientLastName = customer.LastName,
                ClientFatherName = customer.FatherName,
                ClientFirstName = customer.FirstName,
                OrderStatus = Enums.OrderState.Open
            };
        }

        public List<ExecutorOrderMdl.ExecutorOrderItem> GetItemList( List<int> CategoryIds, List<int> SpecializationIds )
        {
            foreach (var category in CategoryIds)
            {

            }
            return null;
        }

        public void Response( string executorId, int orderId, string responseComment )
        {
            WebAppIMaster.Models.Enitities.Response responsies = new Response()
            {
                CreatedAt_Date = DateTime.Now,
                ExecutorId = executorId,
                OrderExecutorId = orderId,
                responseComment =responseComment
            };
            db.Responses.Add(responsies);
            db.SaveChanges();
        }


        public void RemoveBookmark( int orderId )
        {
            throw new NotImplementedException();
        }

        public void AddBookmark( int orderId )
        {
            throw new NotImplementedException();
        }
    }
}