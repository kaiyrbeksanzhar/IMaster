using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// Клиент RegionService Api
    /// </summary>
    public class RegionServiceController : ApiController
    {
        /// <summary>
        /// (api/RegionService) возращает лист элемент
        /// </summary>
        // GET: api/RegionService
        public List<RegionNameView> Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RegionService repository = new RegionService(db);
            var model = repository.GetList();
            return model;
        }
        /// <summary>
        /// передайте id(api/RegionService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: api/RegionService/5
        public RegionNameView Get(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RegionService repository = new RegionService(db);
            var model = repository.Get(id);
            return model;
        }

    }
}
