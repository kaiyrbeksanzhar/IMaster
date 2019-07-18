using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        /// api/GetListForOtherNews/{} возвращает 5 рандомных новостей(другие новости)
        /// </summary>
        [System.Web.Http.Route("api/GetListForOtherNews")]
        public List<PaginationNewsItemMdl> GetListForOtherNews()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsItemService repository = new NewsItemService(db);
            var model = repository.GetListFotOtherNews();
            return model;
        }

        /// <summary>
        /// api/GetListForPagination/{currentPage,pageSize} возвращает News по pageSize
        /// </summary>
        /// <param name="currentPage">Принимает параметр currentPage(int).</param>
        /// <param name="pageSize">Принимает параметр pageSize(int).</param>
        [System.Web.Http.Route("api/GetListForPagination")]
        public List<PaginationNewsItemMdl> GetListForPagination(int? currentPage = null, int? pageSize = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsItemService repository = new NewsItemService(db);
            var model = repository.GetPaginationList(currentPage,pageSize);
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

        /// <summary>
        /// api/SendPhotoToOrder лист возвращает Request.Form["newsId"] и url
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendPhotoToOrder
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SendPhotoToNews")]
        public async void SendPhotoToNews()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            int newsId = Convert.ToInt32(request.Form["newsId"]);
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Images/News/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + ".png";
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                NewsItemService repository = new NewsItemService(db);
                repository.SendPhotoToNews(url, newsId);
            }
        }

        /// <summary>
        /// api/GetNewsPhoto/{url} возвращает фото
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetNewsPhoto")]
        public HttpResponseMessage GetNewsPhoto( string url )
        {
            if (url == "")
            {
                return null;
            }
            byte[] content = File.ReadAllBytes(HttpContext.Current.Server.MapPath(url));
            MemoryStream ms = new MemoryStream(content);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

            return response;
        }


    }
}
