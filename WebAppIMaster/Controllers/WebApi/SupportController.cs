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
    ///  Служба поддержки Api (Специализация)
    /// </summary>
    public class SupportController : ApiController
    {
        /// <summary>
        /// передайте supportId(api/Support/5) возращает один элемент
        /// </summary>
        /// <param name="supportId">Принимает параметр (supportId suppoert).(id)</param>
        [System.Web.Http.Route("api/GetSupport")]
        public SupportServiceMdl.Select Get(int supportId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportSerivce repository = new SupportSerivce();
            var model = repository.Select(supportId);
            return model;
        }
        /// <summary>
        /// передайте (api/GetSupportList) возращает лист элементов
        /// </summary>
        [System.Web.Http.Route("api/GetSupportList")]
        public List<SupportServiceMdl.SupportSelectList> GetSupportList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportSerivce repository = new SupportSerivce();
            var model = repository.SelectList();
            return model;
        }

        /// <summary>
        /// api/GetSupportListForPagination/{currentPage,pageSize} возвращает лист Службка поддержку по pageSize
        /// </summary>
        /// <param name="currentPage">Принимает параметр currentPage(int).</param>
        /// <param name="pageSize">Принимает параметр pageSize(int).</param>
        [System.Web.Http.Route("api/GetSupportListForPagination")]
        public List<SupportServiceMdl.SupportSelectList> GetSupportListForPagination(int? currentPage = null, int? pageSize = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportSerivce repository = new SupportSerivce();
            var model = repository.SelectSupportListForPagination(currentPage, pageSize);
            return model;
        }


        /// <summary>
        /// передайте SupportInsertMdl model Save to DataBase
        /// </summary>
        [System.Web.Http.Route("api/SupportInsert")]
        public int SupportInsert(SupportServiceMdl.SupportInsertMdl model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportSerivce repository = new SupportSerivce();
            int createdId = repository.Create(model);
            return createdId;
        }


        /// <summary>
        /// api/SendPhotoToSupport  возвращает Request.Form["supportId"] и url
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        // GET: api/SendPhotoToOrder
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SendFileToSupport")]
        public async void SendPhotoToSupport()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var request = HttpContext.Current.Request;
            var postedFile = request.Files;

            int newsId = Convert.ToInt32(request.Form["supportId"]);
            string type = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            string typeFile = Path.GetFileName(request.Files[0].FileName);
            string url = "~/Files/SupportFiles/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") + "." + typeFile;
            if (postedFile != null)
            {
                postedFile[0].SaveAs(HttpContext.Current.Server.MapPath(url));
                NewsItemService repository = new NewsItemService(db);
                repository.SendPhotoToNews(url, newsId);
            }
        }

        /// <summary>
        /// api/GetSupportPhoto/{url} возвращает file
        /// </summary>
        /// <param name="files">Принимает параметр files.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSupportFile")]
        public HttpResponseMessage GetSupportPhoto(string url)
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
