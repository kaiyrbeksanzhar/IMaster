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

        public UserAgreementEdit UserAgreementEditSelect()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            var listUA = db.userAgreements.ToList();

            UserAgreementEdit uae = new UserAgreementEdit
            {
                Description_kz = listUA.Where(p => p.Langcode == lang_kz).FirstOrDefault().Description,
                Description_ru = listUA.Where(p => p.Langcode == lang_ru).FirstOrDefault().Description
            };

            return uae;
        }

        public void UserAgreementEditManager(UserAgreementEdit model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            UserAgreement uakz = db.userAgreements.Where(p => p.Langcode == lang_kz).FirstOrDefault();
            UserAgreement uaru = db.userAgreements.Where(p => p.Langcode == lang_ru).FirstOrDefault();
            uakz.Description = model.Description_kz;
            uaru.Description = model.Description_ru;

            db.Entry(uakz).State = System.Data.Entity.EntityState.Modified;
            db.Entry(uaru).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}