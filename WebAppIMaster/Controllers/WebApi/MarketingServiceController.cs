using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiService;
using static WebAppIMaster.Models.WebApiModel.MarketingService;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// MarketingService
    /// </summary>
    public class MarketingServiceController : ApiController
    {
        /// <summary>
        /// (api/GetClientOrderDetailsView) возращает лист элемент
        /// </summary>
        // GET: api/MarketingService
        public List<MarketingNameView> Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketingService repository = new MarketingService(db);
            var model = repository.GetList();
            if (model == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// api/GetMarketinListForPagination/{currentPage,pageSize} возвращает News по pageSize
        /// </summary>
        /// <param name="currentPage">Принимает параметр currentPage(int).</param>
        /// <param name="pageSize">Принимает параметр pageSize(int).</param>
        [System.Web.Http.Route("api/GetMarketinListForPagination")]
        public List<MarketingNameView> GetListForPagination(int? currentPage = null, int? pageSize = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketingService repository = new MarketingService(db);
            var model = repository.GetListForPagination(currentPage, pageSize);
            return model;
        }

        /// <summary>
        /// передайте id(api/MarketingService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: api/MarketingService/5
        public MarketingNameView Get( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketingService repository = new MarketingService(db);
            var model = repository.Get(id);
            return model;
        }

    }
}
