using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
  
    public class MarketService : IMarketService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public MarketService( ApplicationDbContext db ) => this.db = db;

        public void ClientPhone( string clientId, int marketId )
        {
            Models.Enitities.ClientPhone clientPhone = new Models.Enitities.ClientPhone()
            {
                CreatedAt_Date = DateTime.Now,
                CustomerId = clientId,
                MarketId = marketId
            };
            db.ClientPhones.Add(clientPhone);
            db.SaveChanges();
        }

        public void ExecutorPhone( string executorId, int marketId )
        {
            Models.Enitities.ExecutorPhone executorPhone = new Models.Enitities.ExecutorPhone()
            {
                CreatedAt_Date = DateTime.Now,
                ExecutorId = executorId,
                MarketId = marketId
            };
            db.ExecutorPhonies.Add(executorPhone);
            db.SaveChanges();
        }

        public MarketServiceMdl.MarketDetails GetDetails( int id )
        {
            throw new NotImplementedException();
        }

        public List<MarketServiceMdl.MarketItem> GetList()
        {
            throw new NotImplementedException();
        }
    }
}