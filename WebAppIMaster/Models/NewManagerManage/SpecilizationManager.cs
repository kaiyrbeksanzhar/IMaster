using WebAppIMaster.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebAppIMaster.Models.NewManagerModels.SpecilizationModels;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class SpecilizationManager
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public SpecilizationManager( ApplicationDbContext db ) => this.db = db;

        public List<SpecilizationSelect> Select(int categoryId)
        {
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from sl in db.SpecializationLangs
                        where sl.Langcode == langcode
                        where sl.Specialization.CategoryId == categoryId
                        select new SpecilizationSelect
                        {
                            Id = sl.Specialization.Id,
                            Name = sl.Name,
                            PhotoUrl = sl.Specialization.PhotoUrl,
                            Priority = sl.Specialization.Priority,
                            CategoryName = sl.Specialization.Category.Langs.Where(l=>l.Langcode == langcode).Where(l=>l.CategoryId == categoryId).Select(l=>l.Name).FirstOrDefault()
                        }).OrderBy(sl=>sl.Priority).ToList();
            return item;
        }

        public bool Insert( Controller controller, SpecilizationCreate models )
        {
            try
            {
                string lang_kz = LanguageController.GetKzCode();
                string lang_ru = LanguageController.GetRuCode();
                Specialization specialization = new Specialization()
                {
                    CategoryId = models.CategoryId,
                    Priority = models.Priority,
                    PhotoUrl = FileManager.SavePhoto1(controller, models.Photo),
                    Langs = new List<SpecializationLang>()
                    {
                        new SpecializationLang
                        {
                            Langcode = lang_kz,
                            Name = models.Name_kz
                        },
                        new SpecializationLang
                        {
                            Langcode = lang_ru,
                            Name = models.Name_ru
                        }
                    }
                };
                db.Specializations.Add(specialization);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Edit( Controller controller ,SpecilizationEdit model )
        {
            try
            {
                string lang_kz = LanguageController.GetKzCode();
                string lang_ru = LanguageController.GetRuCode();
                Specialization specialization = db.Specializations.Find(model.Id);
                specialization.PhotoUrl = FileManager.SavePhoto1(controller, model.Photo);
                specialization.Priority = model.Priority;
                specialization.Langs = db.SpecializationLangs.Where(sl => sl.SpecializationId == specialization.Id).ToList();
                specialization.Langs.Where(l => l.Langcode == lang_kz).FirstOrDefault().Name = model.Name_kz;
                specialization.Langs.Where(l => l.Langcode == lang_ru).FirstOrDefault().Name = model.Name_ru;
                db.Entry(specialization).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return specialization.Id;
            }
            catch
            {
                return -1;
            }
        }
        public SpecilizationEdit SelectEdit(int id )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            var item = (from s in db.Specializations
                        where s.Id == id

                        select new SpecilizationEdit
                        {
                            Id = id,
                            Name_kz = s.Langs.Where(l => l.Langcode == lang_kz).Select(l => l.Name).FirstOrDefault(),
                            Name_ru = s.Langs.Where(l => l.Langcode == lang_ru).Select(l => l.Name).FirstOrDefault(),
                            PhotoUrl = s.PhotoUrl,
                            Priority = s.Priority
                        }).SingleOrDefault();
            return item;
        }

        public void Delete(int id )
        {
            Specialization specialization = db.Specializations.Find(id);
            db.Specializations.Remove(specialization);
            db.SaveChanges();
        }
    }
}