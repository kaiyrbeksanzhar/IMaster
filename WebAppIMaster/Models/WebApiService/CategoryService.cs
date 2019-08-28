using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models.WebApiService
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CategoryService( ApplicationDbContext db ) => this.db = db;

        public CategoryServiceMdl  Get( int id )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.CategoryLangs.Where(cl => cl.CategoryId == id).Where(cl => cl.Langcode == langcode);
            return new WebApiModel.CategoryServiceMdl
            {
                Id = model.FirstOrDefault().CategoryId,
                Name = model.FirstOrDefault().Name
            };
        }
         
        public List<CategoryServiceMdl> GetList()
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<CategoryServiceMdl> categories = new List<CategoryServiceMdl>();
            var model = db.CategoryLangs.Where(cl=>cl.Langcode == langcode).ToList();
            foreach (var categoryl in model)
            {
                categories.Add(new CategoryServiceMdl
                {
                    Id = categoryl.CategoryId,
                    Name = categoryl.Name,
                    PhotoUrl = categoryl.Categories.UrlPhoto
                });
            }
            return categories;
        }
    }
}