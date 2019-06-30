using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;
using static WebAppIMaster.Models.NewManagerModels.UserAgreementGeneral;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class UserAgreementService 
    {
        public void Create( UserAgreementGeneral model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            List<UserAgreement> userAgreement = new List<UserAgreement>()
            {
                new UserAgreement
                {
                    Description = model.Description_kz,
                    Langcode = lang_kz,
                },
                new UserAgreement
                {
                    Description = model.Description_ru,
                    Langcode = lang_ru,
                }
            };
            db.userAgreements.AddRange(userAgreement);
            db.SaveChanges();
        }

        public List<UserAgreementGeneralVmMdl> SelectList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from u in db.userAgreements

                        where u.Langcode == langcode

                        select new UserAgreementGeneralVmMdl
                        {
                            Id = u.Id,
                            Description = u.Description,
                        }).ToList();
            return item;
        }
    }
}