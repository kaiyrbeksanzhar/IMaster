using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// MarketService
    /// </summary>
    public class MarketSerivecController : ApiController
    {
        // GET: api/MarketSerivece
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// передайте clientId,marketId(api/MarketSerivece/) сохранять в базу
        /// </summary>
        /// <param name="clientId">Принимает параметр clientId.</param>
        /// <param name="marketId">Принимает параметр marketId.</param>
        // GET: api/MarketSerivece/5
        public void PutClient( string clientId, int marketId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            repository.ClientPhone(clientId, marketId);
        }

        /// <summary>
        /// передайте clientId,marketId(api/MarketSerivece/) сохранять в базу
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        /// <param name="marketId">Принимает параметр marketId.</param>
        // GET: api/MarketSerivece/5
        public void PutExecutor( string executorId, int marketId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            repository.ClientPhone(executorId, marketId);
        }
        // POST: api/MarketSerivece
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MarketSerivece/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MarketSerivece/5
        public void Delete(int id)
        {
        }
    }
}
