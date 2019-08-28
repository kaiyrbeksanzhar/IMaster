using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class ResponseManager
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ResponseManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public List<ResponseModels.ResponseList> GetList()
        {
            var result = (from b in db.Responses
                          select new ResponseModels.ResponseList
                          {
                              ExecutorId = b.ExecutorId,
                              ExecutorName = b.Executor.User.FirstName + " " + b.Executor.User.LastName,
                              OrderId = b.OrderId,
                              OrderUserName = b.Order.Customer.FirstName + " " + b.Order.Customer.LastName,
                              CreatedAt_Date = b.CreatedAt_Date,
                              ResponseComment = b.ResponseComment
                          }).ToList();
            return result;
        }

        public ResponseModels.ResponseSelect GetSelect(int OrderId, string ExecutorId)
        {
            Response res = db.Responses.Where(p => p.ExecutorId == ExecutorId).Where(p => p.OrderId == OrderId).FirstOrDefault();

            var result = (from b in db.Responses
                          where b.ExecutorId == ExecutorId
                          where b.OrderId == OrderId
                          select new ResponseModels.ResponseSelect
                          {
                              ExecutorId = b.ExecutorId,
                              OrderId = b.OrderId,
                              CustomerName = b.Order.Customer.FirstName + " " + b.Order.Customer.LastName,
                              AvatarUrl = b.Order.Customer.AvatarUrl,
                              // ------|------
                              OrderName = b.Order.OrderNumber,
                              SpecilizationName = db.SpecializationLangs.Where(p=>p.SpecializationId == b.Order.SpecializationId).FirstOrDefault().Name,
                              

                          }).FirstOrDefault();
            return result;
        }
    }
}