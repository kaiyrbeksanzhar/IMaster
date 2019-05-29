using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebAppIMaster.Models;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers
{
    ///<Summary>
    /// Gets the answer
    ///</Summary>
    [System.Web.Mvc.Route("api/category")]
    public class CategoryController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Category
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        [System.Web.Mvc.Route("api/category/get")]   
        public JsonResult Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryService repository = new CategoryService(db);
            var model = repository.GetList();
            return new JsonResult()
            {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        // GET: api/Category/5
        ///<Summary>
        /// Get нужен id возвращает Category
        ///</Summary>
        [ResponseType(typeof(CategoryServiceMdl))]
        public JsonResult Get(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryService repository = new CategoryService(db);
            var model = repository.Get(id);
            return new JsonResult()
            {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        

   
        
    }
}