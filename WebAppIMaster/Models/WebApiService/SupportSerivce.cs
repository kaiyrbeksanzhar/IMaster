using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class SupportSerivce : ISupportService
    {
        public int Create( SupportServiceMdl.SupportInsertMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Support support = new Support()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                CityId = model.CityId,
                Description = model.Description,
                PhoneNumber = model.PhoneNumber,
                TypeMessage = model.Type,
            };
            db.Supports.Add(support);
            db.SaveChanges();
            return support.Id;
        }

        public SupportServiceMdl.Select Select( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var model = (from s in db.Supports

                         where s.Id == id
                         select new SupportServiceMdl.Select
                         {
                             LastName = s.LastName,
                             FirstName = s.FirstName,
                             CityName = s.City.Langs.Where(l=>l.Langcode == langcode).Select(l=>l.Name).FirstOrDefault(),
                             Description = s.Description,
                             PhoneNumber = s.PhoneNumber,
                             Type = s.TypeMessage,
                         }).SingleOrDefault();
            return model;
        }

        public List<SupportServiceMdl.SupportSelectList> SelectList()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var model = (from s in db.Supports
                         select new SupportServiceMdl.SupportSelectList
                         {
                             Id = s.Id,
                             LastName = s.LastName,
                             FirstName = s.FirstName,
                             CityName = s.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                             Description = s.Description,
                             PhoneNumber = s.PhoneNumber,
                             Type = s.TypeMessage,
                         }).ToList();

            return model;
        }

        public void SendPhotoToSupport(string url, int supportId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Support = db.Supports.Where(s => s.Id == supportId).FirstOrDefault();
            if (Support != null)
            {
                if (Support.FileUrl == null)
                {
                    Support.FileUrl = url;
                }
            }
            db.Entry(Support).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}