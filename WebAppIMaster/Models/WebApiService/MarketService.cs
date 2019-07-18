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

    public class MarketService : IMarketService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public MarketService(ApplicationDbContext db) => this.db = db;

        public void ClientPhone(string clientId, int marketId)
        {
            Models.Enitities.ClientPhone clientPhone = new Models.Enitities.ClientPhone()
            {
                CreatedAt_Date = DateTime.Now,
                CustomerId = clientId,
                MarketId = marketId
            };
            db.ClientPhones.Add(clientPhone);
            db.SaveChanges();
        }

        public void ExecutorPhone(string executorId, int marketId)
        {
            Models.ExecutorPhone executorPhone = new Models.ExecutorPhone()
            {
                CreatedAt_Date = DateTime.Now,
                ExecutorId = executorId,
                MarketId = marketId
            };
            db.ExecutorPhonies.Add(executorPhone);
            db.SaveChanges();
        }

        public MarketServiceMdl.MarketDetails GetDetails(int id)
        {
            MarketServiceMdl.MarketDetails marketDetails = new MarketServiceMdl.MarketDetails();
            string langcode = LanguageController.CurrentCultureCode;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var item = (from o in db.Organizations
                        from ol in o.Langs

                        where ol.Langcode == langcode
                        where o.Id == id
                        select new
                        {
                            Id = o.Id,
                            CategoryName = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryName).FirstOrDefault(),
                            CategoryId = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryMarketId).FirstOrDefault(),
                            RegionName = o.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            RegionId = o.CityId,
                            Name = ol.Name,
                            SiteUrl = o.SiteUrl,
                            Email = o.Email,
                            AboutCompany = ol.ShortDescription,
                            Address = o.Address,
                            SpecialtyName = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.organizationCategoryMarketInCategories).Select(occ => occ.Category).SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            SpecialtyId = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.organizationCategoryMarketInCategories).Select(occ => occ.Category).SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryId).FirstOrDefault(),
                            lattitude = o.latitude,
                            Longitude = o.Longitude,
                            PhoneNumber = o.PhoneNumber,
                            ExtraPhoneNumber = o.ExtraPhoneNumber,
                            PhotosMarket = (from ip in o.IPPhotosFiles
                                            select new
                                            {
                                                PhotoUrl = ip.PhotoUrl,
                                            }).ToList(),
                            PromotionAndDiscountPhoto = o.PromotionAndDiscounts.Select(pad=>pad.BannerUrl).FirstOrDefault(),
                            LogoTypeUrl = o.LogotypeUrl,
                        }).SingleOrDefault();
            var model = db.Organizations.Where(o => o.Id == id).SelectMany(o => o.iPOrganizationPrices).ToList();
            Dictionary<string, string> service = new Dictionary<string, string>();
            foreach (var price in model)
            {
                service.Add(price.ProductName, price.FromPrice + "-" + price.ToPrice == null ? " " : price.ToPrice + "." + price.TarifType.ToString());
            }
            List<string> phoneNumbers = new List<string>();
            phoneNumbers.Add(item.PhoneNumber);
            phoneNumbers.Add(item.ExtraPhoneNumber);
            List<string> photosMarket = new List<string>();
            foreach (var photoMarket in item.PhotosMarket)
            {
                photosMarket.Add(photoMarket.PhotoUrl == null ? null : "http://i-master.kz/api/GetPhoto?url=" + photoMarket.PhotoUrl);
            }
            return new MarketServiceMdl.MarketDetails
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Name = item.Name,
                RegionId = item.RegionId,
                RegionName = item.RegionName,
                SpecialtyId = item.SpecialtyId,
                SpecialtyName = item.SpecialtyName,
                SiteUrl = item.SiteUrl,
                Email = item.Email,
                Coordinates = item.lattitude + " " + item.Longitude,
                Address = item.Address,
                AboutCompany = item.AboutCompany,
                ContactPhoneNumbers = phoneNumbers,
                Serivces = service,
                PhotoFiles = photosMarket,
                LogoFileType = item.LogoTypeUrl == null ? null : "http://i-master.kz/api/GetPhoto?url=" + item.LogoTypeUrl,
                PromotionAndDiscountsUrl = item.PromotionAndDiscountPhoto == null ? null : "http://i-master.kz/api/GetPhoto?url=" + item.PromotionAndDiscountPhoto
            };
        }

        public List<MarketServiceMdl.MarketItem> GetList()
        {
            string langcode = LanguageController.CurrentCultureCode;
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;


            var item = (from o in db.Organizations
                        from ol in o.Langs

                        where ol.Langcode == langcode

                        select new MarketServiceMdl.MarketItem
                        {
                            Id = o.Id,
                            CategoryName = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryName).FirstOrDefault(),
                            CategoryId = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryMarketId).FirstOrDefault(),
                            RegionName = o.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            RegionId = o.CityId,
                            Address = o.Address,
                            SpecialtyName = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.organizationCategoryMarketInCategories).Select(occ => occ.Category).SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            SpecialtyId = o.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.organizationCategoryMarketInCategories).Select(occ => occ.Category).SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryId).FirstOrDefault(),
                            Title = ol.Name,
                        }).ToList();
            return item;
        }


        public List<MarketServiceMdl.MarketItem> GetListForPagination(int? CurrentPage = null, int? PageSize = null)
        {
            if (PageSize == null)
            {
                PageSize = 5;
            }
            string langcode = LanguageController.CurrentCultureCode;

            var query = db.OrganizationLangs.Where(hl => hl.Langcode == langcode);

            int allPageCount = (int)Math.Ceiling(query.Count() / (double)PageSize);
            if (allPageCount < CurrentPage) CurrentPage = 1;

            var sortedQuery = query.Select(u => new
            {
                Id = u.Id,
                CategoryName = u.Organization.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryName).FirstOrDefault(),
                CategoryId = u.Organization.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryMarketId).FirstOrDefault(),
                RegionName = u.Organization.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                RegionId = u.Organization.CityId,
                Address = u.Organization.Address,
                SpecialtyName = u.Organization.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.organizationCategoryMarketInCategories).Select(occ => occ.Category).SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                SpecialtyId = u.Organization.organizationCategories.Select(o => o.CategoryMarket).SelectMany(cm => cm.organizationCategoryMarketInCategories).Select(occ => occ.Category).SelectMany(c => c.Langs).Where(l => l.Langcode == langcode).Select(l => l.CategoryId).FirstOrDefault(),
                Title = u.Name,
                PhotoFile = u.Organization.LogotypeUrl,
            }).Select(m => new MarketServiceMdl.MarketItem
            {
                Id = m.Id,
                CategoryName = m.CategoryName,
                CategoryId = m.CategoryId,
                RegionName = m.RegionName,
                RegionId = m.RegionId,
                Address = m.Address,
                SpecialtyName = m.SpecialtyName,
                SpecialtyId = m.SpecialtyId,
                Title = m.Title,
                LogotypeFile = m.PhotoFile == null ? null : "http://i-master.kz/api/GetPhoto?url=" + m.PhotoFile,
            }).OrderBy(u => u.Title).Skip(((int)CurrentPage - 1) * (int)PageSize).Take((int)PageSize).ToList();

            return sortedQuery;
        }

        public List<string> GetPhotosMarket(int marketId)
        {
            List<string> photos = new List<string>();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            string langcode = LanguageController.CurrentCultureCode;

            var model = db.Organizations.Where(o => o.Id == marketId).SelectMany(o => o.IPPhotosFiles).ToList();
            foreach (var item in model)
            {
                photos.Add(item.PhotoUrl == null ? null : "http://i-master.kz/api/GetPhoto?url=" + item.PhotoUrl);
            }

            return photos;
        }

        public string GetPromotionAndDiscountPhotoForMarket(int marketId)
        {
            string PhotoUrl = " ";
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var model = db.Organizations.Where(o => o.Id == marketId).SelectMany(o => o.PromotionAndDiscounts).ToList();
            foreach (var item in model)
            {
                PhotoUrl = item.BannerUrl == null ? null : "http://i-master.kz/api/GetPhoto?url=" + item.BannerUrl;
            }
            return PhotoUrl;
        }

        public string GetLogoTypeMarket(int marketId)
        {
            List<string> photos = new List<string>();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            string langcode = LanguageController.CurrentCultureCode;

            string LogotypeUrl = db.Organizations.Where(o => o.Id == marketId).Select(o => o.LogotypeUrl).FirstOrDefault();

            return LogotypeUrl;
        }
    }
}