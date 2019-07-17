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
                PhotoFile = model.News.PhotoUrl1 == null ? null : "http://i-master.kz/api/GetNewsPhoto?url=" + model.News.PhotoUrl1,
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
                    PhotoFile = item.News.PhotoUrl1 == null ? null : "http://i-master.kz/api/GetNewsPhoto?url=" + item.News.PhotoUrl1,
                    PhotoFileType = PhotoType,
                    Title = item.Title,
                    ViewCount = item.News.ViewsNumber
                });
            }
            return news;
        }

        public List<PaginationNewsItemMdl> GetListFotOtherNews()
        {
            List<NewsItemMdl> news = new List<NewsItemMdl>();
            string langcode = LanguageController.CurrentCultureCode;
            var query = db.NewsLangs.Where(l => l.Langcode == langcode);

            var sortedQuery = query.Select(u => new
            {
                Id = u.NewsId,
                DateTime = u.News.PublishedDateTime,
                Description = u.Description,
                PhotoFile = u.News.PhotoUrl1 == null ? null : "http://i-master.kz/api/GetNewsPhoto?url=" + u.News.PhotoUrl1,
                Title = u.Title,
                ViewCount = u.News.ViewsNumber
            }).Select(m => new PaginationNewsItemMdl
            {
                Id = m.Id,
                PhotoFile = m.PhotoFile,
                DateTime = m.DateTime,
                Description = m.Description,
                Title = m.Title,
                ViewCount = m.ViewCount,
            }).OrderByDescending(u => u.DateTime).ToList();
            var rand = new Random();
            var randomQuery = sortedQuery.OrderBy(u => rand.Next()).Take(5).ToList();

            return randomQuery;
        }

        public List<PaginationNewsItemMdl> GetPaginationList(int? CurrentPage = null, int? PageSize = null)
        {
            if(PageSize == null)
            {
                PageSize = 5;
            }
            List<NewsItemMdl> news = new List<NewsItemMdl>();
            string langcode = LanguageController.CurrentCultureCode;
            var query = db.NewsLangs.Where(l => l.Langcode == langcode);

            int allPageCount = (int)Math.Ceiling(query.Count() / (double)PageSize);
            if (allPageCount < CurrentPage) CurrentPage = 1;

            var sortedQuery = query.Select(u => new
            {
                Id = u.NewsId,
                DateTime = u.News.PublishedDateTime,
                Description = u.Description,
                PhotoFile = u.News.PhotoUrl1 == null ? null : "http://i-master.kz/api/GetNewsPhoto?url=" + u.News.PhotoUrl1,
                Title = u.Title,
                ViewCount = u.News.ViewsNumber
            }).Select(m=> new PaginationNewsItemMdl
            {
                Id = m.Id,
                PhotoFile = m.PhotoFile,
                DateTime = m.DateTime,
                Description = m.Description,
                Title = m.Title,
                ViewCount =m.ViewCount,
            }).OrderByDescending(u=>u.DateTime).Skip(((int)CurrentPage - 1) * (int)PageSize).Take((int)PageSize).ToList();

            return sortedQuery;
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
                    PhotoFile = item.News.PhotoUrl1 == null ? null : "http://i-master.kz/api/GetNewsPhoto?url=" + item.News.PhotoUrl1,
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