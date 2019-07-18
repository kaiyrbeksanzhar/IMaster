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
    public class MarketingService : IMarketingService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public MarketingService(ApplicationDbContext db) => this.db = db;

        public WebApiModel.MarketingService.MarketingNameView Get(int id)
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.HowDidYouAboutUsLangs.Where(hl => hl.HowDidYouAboutUsId == id).Where(hl => hl.Langcode == langcode).FirstOrDefault(); ;
            return new WebApiModel.MarketingService.MarketingNameView
            {
                Id = model.HowDidYouAboutUsId,
                Name = model.SourceName
            };
        }

        public List<WebApiModel.MarketingService.MarketingNameView> GetList()
        {
            List<WebApiModel.MarketingService.MarketingNameView> result = new List<WebApiModel.MarketingService.MarketingNameView>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.HowDidYouAboutUsLangs.Where(hl => hl.Langcode == langcode).ToList();
            foreach (var marketing in model)
            {
                result.Add(new WebApiModel.MarketingService.MarketingNameView
                {
                    Id = marketing.HowDidYouAboutUsId,
                    Name = marketing.SourceName
                });
            }
            return result;
        }

        public List<WebApiModel.MarketingService.MarketingNameView> GetListForPagination(int? CurrentPage = null, int? PageSize = null)
        {
            if (PageSize == null)
            {
                PageSize = 5;
            }
            string langcode = LanguageController.CurrentCultureCode;

            var query = db.HowDidYouAboutUsLangs.Where(hl => hl.Langcode == langcode);

            int allPageCount = (int)Math.Ceiling(query.Count() / (double)PageSize);
            if (allPageCount < CurrentPage) CurrentPage = 1;

            var sortedQuery = query.Select(u => new
            {
                Id = u.HowDidYouAboutUsId,
                Name = u.SourceName
            }).Select(m => new WebApiModel.MarketingService.MarketingNameView
            {
                Id = m.Id,
                Name = m.Name,
            }).OrderBy(u => u.Name).Skip(((int)CurrentPage - 1) * (int)PageSize).Take((int)PageSize).ToList();

            return sortedQuery;
        }
    }
}