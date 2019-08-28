using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class OrderManager
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public OrderManager(ApplicationDbContext _db) => this.db = _db;

        public List<OrderModels.OrderList> GetList()
        {

            return null;
        }
    }
}