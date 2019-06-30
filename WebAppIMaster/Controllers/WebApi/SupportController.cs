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
    ///  Служба поддержки Api (Специализация)
    /// </summary>
    public class SupportController : ApiController
    {
        /// <summary>
        /// передайте supportId(api/Support/5) возращает один элемент
        /// </summary>
        /// <param name="supportId">Принимает параметр (supportId suppoert).(id)</param>
        [System.Web.Http.Route("api/GetSupport")]
        public SupportServiceMdl.Select Get( int supportId )
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
        /// передайте SupportInsertMdl model Save to DataBase
        /// </summary>
        [System.Web.Http.Route("api/SupportInsert")]
        public int SupportInsert( SupportServiceMdl.SupportInsertMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportSerivce repository = new SupportSerivce();
            int createdId = repository.Create(model);
            return createdId;
        }

    }
}
