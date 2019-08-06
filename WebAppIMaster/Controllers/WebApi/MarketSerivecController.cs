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
using static WebAppIMaster.Models.WebApiModel.MarketServiceMdl;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    /// MarketService
    /// </summary>
    public class MarketSerivecController : ApiController
    {
        /// <summary>
        /// api/GetDetailsMarket/{marketId} возвращает Market
        /// </summary>
        /// <param name="Id">Принимает параметр Id(marketId).</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetDetailsMarket")]
        public MarketDetails GetDetailsMarket(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var model = repository.GetDetails(Id);
            return model;
        }

        /// <summary>
        /// api/GetListMarket возвращает Market List
        /// </summary>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetListMarket")]
        public List<MarketServiceMdl.MarketItem> GetListMarket()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var model = repository.GetList();
            return model;
        }

        /// <summary>
        /// api/GetListMarketForPagination/{currentPage,pageSize} возвращает Market List по pageSize
        /// </summary>
        /// <param name="currentPage">Принимает параметр currentPage(int).</param>
        /// <param name="pageSize">Принимает параметр pageSize(int).</param>
        [System.Web.Http.Route("api/GetListMarketForPagination")]
        public List<MarketServiceMdl.MarketItem> GetListMarketForPagination(int? currentPage = null, int? pageSize = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var model = repository.GetListForPagination(currentPage,pageSize);
            return model;
        }

        /// <summary>
        /// передайте clientId,marketId(api/MarketSerivece/) сохранять в базу
        /// </summary>
        /// <param name="clientId">Принимает параметр clientId.</param>
        /// <param name="marketId">Принимает параметр marketId.</param>
        [System.Web.Http.Route("api/Client")]
        public void PostClient( string clientId, int marketId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            repository.ClientPhone(clientId, marketId);
        }

        /// <summary>
        /// передайте clientId,marketId(api/MarketSerivece/) сохранять в базу
        /// </summary>
        /// <param name="executorId">Принимает параметр executorId.</param>
        /// <param name="marketId">Принимает параметр marketId.</param>
        // GET: api/MarketSerivece/5
        public void PostExecutor( string executorId, int marketId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            repository.ClientPhone(executorId, marketId);
        }

        /// <summary>
        /// api/MarketPhotosUrls/{marketId} возвращает List urls photo
        /// </summary>
        /// <param name="marketId">Принимает параметр marketId.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MarketPhotosUrls")]
        public List<string> MarketPhotosUrls(int marketId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var result = repository.GetPhotosMarket(marketId);
            return result;
        }


        /// <summary>
        /// api/GetPromotionAndDiscountPhotoForMarket/{marketId} возвращает Акция и скидки  url photo
        /// </summary>
        /// <param name="marketId">Принимает параметр marketId.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPromotionAndDiscountPhotoForMarket")]
        public string GetPromotionAndDiscountPhotoForMarket(int marketId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var result = repository.GetPromotionAndDiscountPhotoForMarket(marketId);
            return result;
        }

        /// <summary>
        /// api/GetLogoTypeMarket/{marketId} возвращает Логотип маркет  url photo
        /// </summary>
        /// <param name="marketId">Принимает параметр marketId.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetLogoTypeMarket")]
        public string GetLogoTypeMarket(int marketId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var result = repository.GetLogoTypeMarket(marketId);
            return result;
        }

        /// <summary>
        /// api/GetListPromotionAndDiscountForPagination/{currentPage,pageSize} возвращает Акция и скидка маркет Лист
        /// </summary>
        /// <param name="currentPage">Принимает параметр currentPage.</param>
        /// <param name="pageSize">Принимает параметр pageSize.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetListPromotionAndDiscountForPagination")]
        public List<MarketServiceMdl.PromotionAndDiscount> GetListPromotionAndDiscountForPagination( int? currentPage = null, int? pageSize = null )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MarketService repository = new MarketService(db);
            var result = repository.GetListPaginationPromotionAndDiscount(currentPage, pageSize);
            return result;
        }

        /// <summary>
        /// api/GetPhoto/{url} возвращает фото
        /// </summary>
        /// <param name="url">Принимает параметр url.</param>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPhoto")]
        public HttpResponseMessage GetPhoto(string url)
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
