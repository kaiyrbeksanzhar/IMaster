using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using static WebAppIMaster.Models.WebApiModel.UserAgreement;

namespace WebAppIMaster.Models.WebApiService
{
    public class UserAgreementService
    {
        public List<UserAgreementList> SelectList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from u in db.userAgreements
                        where u.Langcode == langcode
                        select new UserAgreementList
                        {
                            Id = u.Id,
                            Description = u.Description,
                        }).ToList();
            return item;
        }
    }
}