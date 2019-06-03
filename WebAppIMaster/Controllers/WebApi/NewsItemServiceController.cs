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
    /// News новости 
    /// </summary>
    public class NewsItemServiceController : ApiController
    {
        /// <summary>
        /// (api/NewsItemService) возращает лист элементов
        /// </summary>
        // GET: api/NewsItemService
        public List<NewsItemMdl> Get()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsItemService repository = new NewsItemService(db);
            var model = repository.GetList();
            return model;
        }
        /// <summary>
        /// передайте id(api/NewsItemService/5) возращает один элемент
        /// </summary>
        /// <param name="id">Принимает параметр id</param>
        // GET: api/NewsItemService/5
        public NewsItemMdl Get(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsItemService repository = new NewsItemService(db);
            var model = repository.Get(id);
            return model;
        }

        /// <summary>
        /// передайте id(api/NewsItemService/Казахстан) возращает один элемент
        /// </summary>
        /// <param name="SearchText">принимает string{Казахстан}  возвращает вам один элемент</param>
        // GET: api/NewsItemService/title{Казахстан}
        public List<NewsItemMdl> GetSearch(string SearchText)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsItemService repository = new NewsItemService(db);
            var model = repository.GetSearch(SearchText);
            return model;
        }

    }
}
