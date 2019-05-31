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
        /// возвращает List<NewsItemMdl> лист новости
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
        /// возвращает List<NewsItemMdl> лист новости
        /// </summary>
        /// <param name="id">id должны передать метод возвращает вам одну новость</param>
        // GET: api/NewsItemService/5
        public NewsItemMdl Get(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsItemService repository = new NewsItemService(db);
            var model = repository.Get(id);
            return model;
        }

        /// <summary>
        /// возвращает List<NewsItemMdl> лист новости
        /// </summary>
        /// <param name="SearchText">id должны передать метод возвращает вам одну новость</param>
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
