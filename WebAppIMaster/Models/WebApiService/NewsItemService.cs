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
    public class NewsItemService : INewsItemService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public NewsItemService(ApplicationDbContext db) => this.db = db;
        public NewsItemMdl Get(int id)
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.NewsLangs.Where(nl => nl.NewsId == id).Where(nl=>nl.Langcode == langcode).FirstOrDefault();
            byte[] byteData = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~") + model.News.PhotoUrl1);
            return new NewsItemMdl
            {
                Id = model.Id,
                DateTime = model.News.PublishedDateTime,
                Description = model.Description,
                PhotoFile = byteData,
                Title = model.Title,
                ViewCount = model.News.ViewsNumber,
                PhotoFileType = model.News.PhotoUrl1.Substring(model.News.PhotoUrl1.LastIndexOf(".") + 1),
        };
        }

        public List<NewsItemMdl> GetList()
        {
            List<NewsItemMdl> news = new List<NewsItemMdl>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.NewsLangs.Where(l => l.Langcode == langcode).ToList();
            foreach(var item in model)
            {
                string PhotoType = item.News.PhotoUrl1.Substring(item.News.PhotoUrl1.LastIndexOf(".") + 1);
                byte[] byteData = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(item.News.PhotoUrl1));
                news.Add(new NewsItemMdl
                {
                    Id = item.NewsId,
                    DateTime = item.News.PublishedDateTime,
                    Description = item.Description,
                    PhotoFile = byteData,
                    PhotoFileType = PhotoType,
                    Title = item.Title,
                    ViewCount = item.News.ViewsNumber
                });
            }
            return news;
        }

        public List<NewsItemMdl> GetSearch(string searchText)
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<NewsItemMdl> news = new List<NewsItemMdl>();
            var model = db.NewsLangs.Where(l => l.Langcode == langcode).Where(l=>l.Title.Contains(searchText) && l.Description.Contains(searchText)).ToList();
            foreach (var item in model)
            {
                byte[] byteData = System.IO.File.ReadAllBytes(item.News.PhotoUrl1);
                news.Add(new NewsItemMdl
                {
                    Id = item.Id,
                    DateTime = item.News.PublishedDateTime,
                    Description = item.Description,
                    PhotoFile = byteData,
                    Title = item.Title,
                    ViewCount = item.News.ViewsNumber
                });
            }
            return news;
        }

        public void SendPhotoToNews( string url, int newsId )
        {
            var News = db.News.Where(n => n.Id == newsId).FirstOrDefault();
            if (News != null)
            {
                if (News.PhotoUrl1 == null)
                {
                    News.PhotoUrl1 = url;
                }
                else if (News.PhotoUrl2Kazakh == null)
                {
                    News.PhotoUrl2Kazakh = url;
                }
            }
            db.Entry(News).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}