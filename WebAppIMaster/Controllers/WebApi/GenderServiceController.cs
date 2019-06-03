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
    ///  GenderService Api
    /// </summary>
    public class GenderServiceController : ApiController
    {
        /// <summary>
        ///  возращает лист элементов
        /// </summary>
        // GET: api/ClientProfile/5
        public List<GenderServiceMdl.GenderNameView> Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            GenderService repository = new GenderService();
            var model = repository.GetList();
            return model;
        }
        /// <summary>
        /// передайте id(api/GenderService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: api/GenderService/5
        public GenderServiceMdl.GenderNameView Get(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            GenderService repository = new GenderService();
            var model = repository.Get(id);
            return model;
        }


    }
}
