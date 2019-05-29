using WebAppIMaster.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebAppIMaster.Models.NewManagerModels.CategoryModels;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class CategoryManagerModels
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public CategoryManagerModels( ApplicationDbContext db ) => this.db = db;

        public List<CategorySelect> Select()
        {
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from cl in db.CategoryLangs
                        where cl.Langcode == langcode

                        select new CategorySelect
                        {
                            Id = cl.CategoryId,
                            Name = cl.Name,
                            Priority = cl.Categories.Priority,
                            SpecializationCount = cl.Categories.Specializations.Count(),
                            UrlPhoto = cl.Categories.UrlPhoto
                        }).OrderBy(cl => cl.Priority).ToList();

            return item;
        }

        public bool Insert( Controller controller, CategoryCreate model )
        {
            try
            {
                string lang_kz = LanguageController.GetKzCode();
                string lang_ru = LanguageController.GetRuCode();

                Category category = new Category()
                {
                    Priority = model.Priority,
                    Langs = new List<CategoryLang>()
                {
                    new CategoryLang
                    {
                        Langcode = lang_kz,
                        Name = model.Name_kz
                    },
                    new CategoryLang
                    {
                        Langcode = lang_ru,
                        Name = model.Name_ru
                    }
                }
                };
                category.UrlPhoto = FileManager.SavePhoto(controller, model.Photo);
                db.Categories.Add(category);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Edit( Controller controller, CategoryEdit model )
        {
            try
            {
                string lang_kz = LanguageController.GetKzCode();
                string lang_ru = LanguageController.GetRuCode();
                Category category = db.Categories.Find(model.Id);
                category.Priority = model.Priority;
                category.UrlPhoto = FileManager.SavePhoto(controller, model.Photo);
                category.Langs = db.CategoryLangs.Where(cl => cl.CategoryId == category.Id).ToList();
                category.Langs.Where(l => l.Langcode == lang_kz).FirstOrDefault().Name = model.Name_kz;
                category.Langs.Where(l => l.Langcode == lang_ru).FirstOrDefault().Name = model.Name_ru;
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return category.Id;
            }
            catch
            {
                return -1;
            }
        }
        public CategoryEdit SelectEdit( int id )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            var item = (from c in db.Categories
                        where c.Id == id
                        select new CategoryEdit
                        {
                            Id = id,
                            Name_kz = c.Langs.Where(l => l.Langcode == lang_kz).Select(l => l.Name).FirstOrDefault(),
                            Name_ru = c.Langs.Where(l => l.Langcode == lang_ru).Select(l => l.Name).FirstOrDefault(),
                            Priority = c.Priority
                        }).SingleOrDefault();
            return item;
        }
        public void Delete( int id )
        {
            var models = db.Categories.Find(id);
            db.Categories.Remove(models);
            db.SaveChanges();

        }

    }
}