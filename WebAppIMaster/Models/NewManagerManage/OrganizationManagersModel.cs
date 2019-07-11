using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class OrganizationManagersModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public OrganizationManagersModel( ApplicationDbContext db )
        {
            this.db = db;
        }

        public bool OrganizationInsert( OrganizationCreateMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            try
            {
                Organization organization = new Organization()
                {
                    Address = model.Address,
                    CityId = model.CityId,
                    PhoneNumber = model.PhoneNumber,
                    ExtraPhoneNumber = model.ExtraPhoneNumber,
                    SiteUrl = model.SiteUrl,
                    Email = model.Email,
                    latitude = model.latitude,
                    Longitude = model.Longitude,
                    YouTubeVideoUrl = model.VideoUrl,
                    YouTubeVideoUrlkz = model.VideoUrl_kz,
                    LogotypeUrl = model.LogoTypeUrl,
                    TarifType = model.TarifType,
                    Langs = new List<OrganizationLang>
                {
                    new OrganizationLang
                    {
                     Langcode = lang_kz,
                     Name = model.Name_kz,
                     ShortDescription = model.ShortDescription_kz,
                    },
                    new OrganizationLang
                    {
                     Langcode = lang_ru,
                     Name = model.Name_ru,
                     ShortDescription = model.ShortDescription_ru,
                    }
                }
                };
                db.Organizations.Add(organization);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}