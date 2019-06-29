using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.NewManagerModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class HowDidYouAboutUsModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public HowDidYouAboutUsModel( ApplicationDbContext db ) => this.db = db;




        public IPagedList<HowDidYouAboutUsSelect> Select( string sourceName, int? pageNumber )
        {
            string langcode = LanguageController.CurrentCultureCode;
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            var query = db.HowDidYouAboutUsLangs.Where(l => l.Langcode == langcode);
            if (sourceName != null && sourceName != "")
            {
                query = query.Where(l => l.SourceName.ToLower().Contains(sourceName.ToLower()));
            }

            var sortedQuery = query.Select(q => new HowDidYouAboutUsSelect
            {
                DateTime = q.HowDidYouAboutUs.DateTime,
                SourceName = q.SourceName,
                Id = q.HowDidYouAboutUs.Id,
                Order = (int)q.HowDidYouAboutUs.Order,
            }).OrderByDescending(hl => hl.DateTime).ToList().ToPagedList(pageNumber ?? 1, 10);

            return sortedQuery;
        }
        public IPagedList<HowDidYouAboutUsSelectPopulation> SelectPopulation( string sourceName, int? pageNumber )
        {
            string langcode = LanguageController.CurrentCultureCode;
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            var query = db.HowDidYouAboutUsLangs.Where(l => l.Langcode == langcode);
            if (sourceName != null && sourceName != "")
            {
                query = query.Where(l => l.SourceName.ToLower().Contains(sourceName.ToLower()));
            }

            var sortedQuery = query.Select(q => new HowDidYouAboutUsSelectPopulation
            {
                DateTime = q.HowDidYouAboutUs.DateTime,
                SourceName = q.SourceName,
                Id = q.HowDidYouAboutUs.Id,
                Order = (int)q.HowDidYouAboutUs.Order,
            }).ToList().ToPagedList(pageNumber ?? 1, 10);
            return sortedQuery;
        }

        public bool Insert( HowDidYouAboutUsCreate model )
        {
            try
            {
                string lang_kz = LanguageController.GetKzCode();
                string lang_ru = LanguageController.GetRuCode();
                HowDidYouAboutUs howDidYouAbout = new HowDidYouAboutUs()
                {
                    DateTime = model.DateTime,
                    Order = model.Order,
                    Langs = new List<HowDidYouAboutUsLang>()
                    {
                        new HowDidYouAboutUsLang
                        {
                            Langcode = lang_kz,
                            SourceName = model.SourceName_kz
                        },
                        new HowDidYouAboutUsLang
                        {
                            Langcode = lang_ru,
                            SourceName = model.SourceName_ru
                        }
                    }
                };
                db.HowDidYouAboutUes.Add(howDidYouAbout);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Edit( HowDidYouAboutUsEdit model )
        {
            try
            {
                HowDidYouAboutUs hdyouAbout = db.HowDidYouAboutUes.Find(model.Id);
                hdyouAbout.Order = model.Order;
                hdyouAbout.DateTime = model.DateTime;
                hdyouAbout.Langs = db.HowDidYouAboutUsLangs.Where(l => l.HowDidYouAboutUsId == model.Id).ToList();
                hdyouAbout.Langs.Where(l => l.Langcode == LanguageController.GetKzCode()).FirstOrDefault().SourceName = model.SourceName_kz;
                hdyouAbout.Langs.Where(l => l.Langcode == LanguageController.GetRuCode()).FirstOrDefault().SourceName = model.SourceName_ru;
                db.Entry(hdyouAbout).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return hdyouAbout.Id;
            }
            catch
            {
                return -1;
            }
        }
        public HowDidYouAboutUsEdit SelectEdit( int id )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            var item = (from h in db.HowDidYouAboutUes
                        where h.Id == id

                        select new HowDidYouAboutUsEdit
                        {
                            Id = id,
                            SourceName_kz = h.Langs.Where(l => l.Langcode == lang_kz).Select(l => l.SourceName).FirstOrDefault(),
                            SourceName_ru = h.Langs.Where(l => l.Langcode == lang_ru).Select(l => l.SourceName).FirstOrDefault(),
                            Order = (int)h.Order,
                            DateTime = h.DateTime
                        }).SingleOrDefault();
            return item;
        }
        public bool Delete( int id )
        {
            var model = db.HowDidYouAboutUes.Find(id);
            if (model != null)
            {
                db.HowDidYouAboutUes.Remove(model);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}