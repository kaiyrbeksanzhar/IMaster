using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;
using WebAppIMaster.Models.NewManagerModels;
using static WebAppIMaster.Models.WebApiModel.UserAgreement;

namespace WebAppIMaster.Controllers.WebApi
{
    /// <summary>
    ///  Пользовательское соглашение
    /// </summary>
    public class UserAgreementServiceController : ApiController
    {
        /// <summary>
        /// (api/GetListUserAgreement) возращает лист элементов
        /// </summary>
        [System.Web.Http.Route("api/GetListUserAgreement")]
        public List<UserAgreementGeneralVmMdl> GetListUserAgreement()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserAgreementService repository = new UserAgreementService();
            var result = repository.SelectList();
            return result;
        }
    }
}
