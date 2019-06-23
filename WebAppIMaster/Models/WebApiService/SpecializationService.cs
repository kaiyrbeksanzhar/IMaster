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
    public class SpecializationService : ISpecializationService
    {
        private ApplicationDbContext db;

        public SpecializationService( ApplicationDbContext db ) => this.db = db;

        public SpecializationServiceMdl Get( int id )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from s in db.Specializations
                        from sl in s.Langs

                        where s.Id == id
                        where sl.Langcode == langcode

                        select new SpecializationServiceMdl
                        {
                            Id = s.Id,
                            CategoryId = s.CategoryId,
                            Name = sl.Name
                        }).SingleOrDefault();
            return item;
        }

        public List<SpecializationServiceMdl> GetList()
        {
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from s in db.Specializations
                        from sl in s.Langs

                        where sl.Langcode == langcode

                        select new SpecializationServiceMdl
                        {
                            Id = s.Id,
                            CategoryId = s.CategoryId,
                            Name = sl.Name
                        }).ToList();
            return item;
        }

        public List<SpecializationServiceMdl> GetList( int categoryId )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from s in db.Specializations
                        from sl in s.Langs

                        where s.CategoryId == categoryId
                        where sl.Langcode == langcode

                        select new SpecializationServiceMdl
                        {
                            Id = s.Id,
                            CategoryId = s.CategoryId,
                            Name = sl.Name,
                            CategoryName = s.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                        }).ToList();
            return item;
        }
    }
}