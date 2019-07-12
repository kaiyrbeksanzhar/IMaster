using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class SelectListManager
    {
    }

    public class CitySelectList
    {
        public static SelectList SelectListForSelectList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;
            var model = (from c in db.Cities
                         from cl in c.Langs
                         where cl.Langcode == langcode

                         select new
                         {
                             Id = c.Id,
                             Name = cl.Name
                         });
            return new SelectList(model, "Id", "Name");
        }
    }

    public class CategoryMarketSelectList
    {
        public static SelectList SelectListForSelectList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var model = (from cm in db.CategoryMarkets
                         from cml in cm.Langs
                         where cml.Langcode == langcode

                         select new
                         {
                             Id = cm.Id,
                             Name = cml.CategoryName,
                         });
            return new SelectList(model, "Id", "Name");
        }
    }

    public class CategorySelectList
    {
        public static SelectList SelectListForSelectList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var model = (from c in db.Categories
                         from cl in c.Langs

                         where cl.Langcode == langcode

                         select new
                         {
                             Id = c.Id,
                             Name = cl.Name,
                         });
            return new SelectList(model, "Id", "Name");
        }
    }
}