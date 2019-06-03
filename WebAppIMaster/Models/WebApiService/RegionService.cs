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
    public class RegionService : IRegionService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public RegionService( ApplicationDbContext db ) => this.db = db;

        public RegionNameView Get( int id )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.CityLangs.Where(cl => cl.CityId == id);
            return new RegionNameView
            {
                id = id,
                LastStage = true,
                Name = model.Where(m => m.Langcode == langcode).Select(m => m.Name).FirstOrDefault(),
                ParentId = -1
            };
        }

        public List<RegionNameView> GetList()
        {
            List<RegionNameView> result = new List<RegionNameView>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.CityLangs.Where(cl => cl.Langcode == langcode).ToList();
            foreach (var city in model)
            {
                result.Add(new RegionNameView
                {
                    id = city.CityId,
                    LastStage = true,
                    Name = city.Name,
                    ParentId = -1
                });
            }
            return result;
        }

    }
}