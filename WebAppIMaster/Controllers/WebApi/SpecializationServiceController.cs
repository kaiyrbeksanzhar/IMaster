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
    ///  SpecializationService Api (Специализация)
    /// </summary>
    public class SpecializationServiceController : ApiController
    {

        /// <summary>
        /// передайте id(api/SpecializationService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр (Id специализаций).(id)</param>
        // GET: api/SpecializationService
        public SpecializationServiceMdl Get( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SpecializationService repository = new SpecializationService(db);
            var model = repository.Get(id);
            return model;
        }
        /// <summary>
        ///  (api/GetSpecializationList/) возвращает лист элементов 
        /// </summary>
        [System.Web.Http.Route("api/GetSpecializationList")]
        // GET: api/GetSpecializationList
        public List<SpecializationServiceMdl> GetList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SpecializationService repository = new SpecializationService(db);
            var model = repository.GetList();
            return model;
        }
        /// <summary>
        ///  (api/GetSpecializationForCategoryList/) возвращает специализацию лист элементов(фильтр категория)
        /// <param name="categoryId">Принимает параметр categoryId.</param>
        /// </summary>
        [System.Web.Http.Route("api/GetSpecializationForCategoryList")]
        // GET: api/GetSpecializationList
        public List<SpecializationServiceMdl> GetSpecializationForCategoryList(int categoryId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SpecializationService repository = new SpecializationService(db);
            var model = repository.GetList(categoryId);
            return model;
        }
    }
}
