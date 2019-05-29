using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebAppIMaster.Models;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// CategoryService Json два метода есть
    /// </summary>
    public class CategoryServiceController : ApiController
    {

        /// <summary>
        /// Возвращает полностью Category
        /// </summary>
        // GET: api/CategoryService
        [System.Web.Http.Route("api/CategoryService")]
        public List<CategoryServiceMdl> Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryService repository = new CategoryService(db);
            var model = repository.GetList();
            return model;
        }
        /// <summary>
        /// передайте id(api/CategoryService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id.</param>
        // GET: 
        public CategoryServiceMdl Get( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryService repository = new CategoryService(db);
            var model = repository.Get(id);
            return model;
        }


    }
}
