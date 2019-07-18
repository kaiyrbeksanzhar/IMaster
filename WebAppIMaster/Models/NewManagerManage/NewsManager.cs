using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Controllers.AppController;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Threading.Tasks;
using FireSharp;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class NewsManager
    {
        ApplicationDbContext db = new ApplicationDbContext();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "rKTja1mSTCgwIQ0gix83P6v2ocaT67v5Jz4qx7uQ",
            BasePath = "https://plasma-climber-231012.firebaseio.com/",
        };

        IFirebaseClient client;

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

        public int Create( NewsCreateMdl model,Controller controller)
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
            return news.Id;
        }

        public  Data SelectNotification()
        {

            FirebaseResponse response = client.Get("News/"+16);
           
            Data obj = response.ResultAs<Data>();
            return  new Data
            {
                Id = obj.Id,
                Status = obj.Status,
                Text = obj.Text,
            };
        }
    }
}