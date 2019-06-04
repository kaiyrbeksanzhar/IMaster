﻿using System;
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
            byte[] byteData = System.IO.File.ReadAllBytes(model.News.PhotoUrl1);
            //Convert byte arry to base64string   
            //string imreBase64Data = Convert.ToBase64String(byteData);
            //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            return new NewsItemMdl
            {
                Id = model.Id,
                DateTime = model.News.PublishedDateTime,
                Description = model.Description,
                PhotoFile = byteData,
                Title = model.Title,
                ViewCount = model.News.ViewsNumber
            };
        }

        public List<NewsItemMdl> GetList()
        {
            List<NewsItemMdl> news = new List<NewsItemMdl>();
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.NewsLangs.Where(l => l.Langcode == langcode).ToList();
            foreach(var item in model)
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
    }
}