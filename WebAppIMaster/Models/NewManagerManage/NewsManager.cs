using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class NewsManager
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public NewsManager( ApplicationDbContext db ) => this.db = db;

        public IPagedList<NewsVmMdl> Select( int? pageNumber )
        {
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from n in db.News

                        select new NewsVmMdl
                        {
                           Id = n.Id,
                           Title = n.Langs.Where(l=>l.Langcode == langcode).Select(l=>l.Title).FirstOrDefault(),
                           TypeRoles = n.TypeRoles,
                        }).OrderByDescending(n=>n.Id).ToList().ToPagedList(pageNumber ?? 1, 10);;

            return item;
        }

        public void Create( NewsCreateMdl model,Controller controller)
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            string photo = FileManager.SaveNewsPhoto(controller, model.Photo);
            string photo_kz = FileManager.SaveNewsPhoto(controller,model.Photo_kz);
            News news = new News()
            {
                TypeRoles = model.TypeRoles,
                PhotoUrl1 = photo,
                PhotoUrl2Kazakh = photo_kz,
                PublishedDateTime = DateTime.Now,
                ViewsNumber = 0,
                Langs = new List<NewsLang>
                {
                    new NewsLang
                    {
                        Title = model.Title_kz,
                        ShortText = model.ShortText_kz,
                        Description = model.Description_kz,
                        Langcode = lang_kz,
                    },
                    new NewsLang
                    {
                        Title = model.Title_ru,
                        ShortText = model.ShortText_ru,
                        Description = model.Description_ru,
                        Langcode = lang_ru
                    }
                }
            };
            db.News.Add(news);
            db.SaveChanges();
        }
    }
}