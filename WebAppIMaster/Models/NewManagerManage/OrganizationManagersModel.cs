using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class OrganizationManagersModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public OrganizationManagersModel( ApplicationDbContext db )
        {
            this.db = db;
        }

        public bool OrganizationInsert( OrganizationCreateMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            try
            {
                string logoUrl = FileManager.SaveOrganizationLogo(model.LogoType);
                Organization organization = new Organization()
                {
                    Address = model.Address,
                    CityId = model.CityId,
                    PhoneNumber = model.PhoneNumber,
                    ExtraPhoneNumber = model.ExtraPhoneNumber,
                    SiteUrl = model.SiteUrl,
                    Email = model.Email,
                    latitude = model.latitude,
                    Longitude = model.Longitude,
                    YouTubeVideoUrl = model.VideoUrl,
                    YouTubeVideoUrlkz = model.VideoUrl_kz,
                    LogotypeUrl = logoUrl,
                    TarifType = model.TarifType,
                    Langs = new List<OrganizationLang>
                    {
                        new OrganizationLang
                        {
                         Langcode = lang_kz,
                         Name = model.Name_kz,
                         ShortDescription = model.ShortDescription_kz,
                        },
                        new OrganizationLang
                        {
                         Langcode = lang_ru,
                         Name = model.Name_ru,
                         ShortDescription = model.ShortDescription_ru,
                        }
                    }
                };
                db.Organizations.Add(organization);
                db.SaveChanges();

                List<OrganizationCategory> organizationCategories = new List<OrganizationCategory>();
                foreach (var item in model.categoryOrganizations)
                {
                    organizationCategories.Add(new OrganizationCategory
                    {
                        OrganizationId = organization.Id,
                        CategoryMarketId = item.CategoryOrganizationId,
                    });
                }
                db.organizationCategories.AddRange(organizationCategories);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InsertPhotoOrganization( PhotoCreateOrganizationMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            List<IPPhotosFiles> iPPhotosFiles = new List<IPPhotosFiles>()
            {
                new IPPhotosFiles
                {
                    OrganizationId = model.OrganizationId,
                    PhotoUrl = FileManager.SaveOrganizationPhoto(model.PhotoUrl1Type),
                },
                new IPPhotosFiles
                {
                    OrganizationId = model.OrganizationId,
                    PhotoUrl = FileManager.SaveOrganizationPhoto(model.PhotoUrl2Type),
                },
                new IPPhotosFiles
                {
                    OrganizationId = model.OrganizationId,
                    PhotoUrl = FileManager.SaveOrganizationPhoto(model.PhotoUrl3Type),
                },
                new IPPhotosFiles
                {
                    OrganizationId = model.OrganizationId,
                    PhotoUrl = FileManager.SaveOrganizationPhoto(model.PhotoUrl4Type),
                }
            };
            db.IPPhotosFilies.AddRange(iPPhotosFiles);
            db.SaveChanges();
        }

        public List<OrganizationListVMMdl> SelectList()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var item = (from o in db.Organizations
                        from ol in o.Langs

                        where ol.Langcode == langcode

                        select new OrganizationListVMMdl
                        {
                            Id = o.Id,
                            Name = ol.Name,
                            City = o.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            Category = (from cm in o.organizationCategories

                                        select new OrganizationListVMMdl.Categories
                                        {
                                            CategoryNameMarket = cm.CategoryMarket.Langs.Where(l => l.Langcode == langcode).Select(l => l.CategoryName).FirstOrDefault(),
                                        }).ToList(),
                        }).ToList();

            return item;
        }

        public OrganizationVmMdl SelectOrganization( int Id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var item = (from o in db.Organizations
                        from ol in o.Langs

                        where o.Id == Id
                        where ol.Langcode == langcode

                        select new OrganizationVmMdl
                        {
                            Id = o.Id,
                            OrganizationName = ol.Name,
                            PhoneNumber = o.PhoneNumber,
                            ExtraPhoneNumber = o.ExtraPhoneNumber,
                            Email = o.Email,
                            ShortDescription = ol.ShortDescription,
                            SiteUrl = o.SiteUrl,
                            Address = o.Address,
                            PhotoPromotionAndDiscountId = o.PromotionAndDiscounts.Select(p => p.Id).FirstOrDefault(),
                            PhotoPromotionAndDiscountUrl = o.PromotionAndDiscounts.Select(p => p.BannerUrl).FirstOrDefault(),
                            PhotoUrl = o.LogotypeUrl,
                            VidoeUrl = o.YouTubeVideoUrl,
                            VideoUrlkz = o.YouTubeVideoUrlkz,
                            photos = (from p in o.IPPhotosFiles

                                      select new OrganizationVmMdl.Photos
                                      {
                                          PhotoUrl = p.PhotoUrl,
                                      }).ToList(),
                            catgoriesMarkets = (from oc in o.organizationCategories

                                                select new OrganizationVmMdl.CatgoriesMarket
                                                {
                                                    Name = oc.CategoryMarket.Langs.Where(l => l.Langcode == langcode).Select(l => l.CategoryName).FirstOrDefault(),
                                                }).ToList(),
                           iPOrganizationPrices = (from ipo in o.iPOrganizationPrices

                                                   select new OrganizationVmMdl.IPOrganizationPrices
                                                   {
                                                       ProductName = ipo.ProductName,
                                                       From = ipo.FromPrice,
                                                       To = ipo.ToPrice,
                                                       TypeTarif = ipo.TarifType,
                                                   }).ToList(),
                        }).SingleOrDefault();
            return item;
        }

        public List<IndexOrganizationPrice> SelectOrganizationPrice(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var item = (from ip in db.iPOrganizationPrices
                        where ip.OrganizationId == id
                        select new IndexOrganizationPrice
                        {
                            OrganizationName = ip.Organization.Langs.Where(l=>l.Langcode == langcode).Select(l=>l.Name).FirstOrDefault(),
                            OrganizationId = ip.OrganizationId,
                            FromPrice = ip.FromPrice,
                            ProductName = ip.ProductName,
                            TarifType = ip.TarifType,
                            ToPrice = ip.ToPrice,
                        }).ToList();

            return item;
        }

        public void InsertOrganizationPrice( CreateOrganizationPrice model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            IPOrganizationPrice iPOrganizationPrice = new IPOrganizationPrice()
            {
                OrganizationId = model.OrganizationId,
                ProductName = model.ProductName,
                FromPrice = model.FromPrice,
                TarifType = model.TarifType,
                ToPrice = model.ToPrice,
                Type = model.Type
            };
            db.iPOrganizationPrices.Add(iPOrganizationPrice);
            db.SaveChanges();
        }

        public void OrganizationCategoryInsert( CategoryMarketInsert model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            CategoryMarket categoryMarket = new CategoryMarket()
            {
                Langs = new List<CategoryMarketLang>
                {
                    new CategoryMarketLang
                    {
                        Langcode = lang_kz,
                        CategoryName = model.CategoryName_kz,
                    },
                    new CategoryMarketLang
                    {
                        Langcode = lang_ru,
                        CategoryName = model.CategoryName_ru,
                    }
                }
            };
            db.CategoryMarkets.Add(categoryMarket);
            db.SaveChanges();
            foreach (var item in model.categoryMarketInCategories)
            {
                OrganizationCategoryMarketInCategory organizationCategoryMarketInCategory = new OrganizationCategoryMarketInCategory()
                {
                    CategoryId = item.CategoryMarketInCategoryId,
                    CategoryMarketId = categoryMarket.Id,
                    CreatedAt_Date = DateTime.Now,
                };
                db.organizationCategoryMarketInCategories.Add(organizationCategoryMarketInCategory);
                db.SaveChanges();
            }
        }

        public List<CategoryMarketVmMdl> SelectListCategoryMarket()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var item = (from cm in db.CategoryMarkets
                        from cml in cm.Langs
                        where cml.Langcode == langcode

                        select new CategoryMarketVmMdl
                        {
                            CategoryMarketName = cml.CategoryName,
                            Id = cm.Id,
                            CatgoryNames = (from c in cm.organizationCategoryMarketInCategories

                                            select new CategoryMarketVmMdl.CatgoryName
                                            {
                                                Id = c.CategoryId,
                                                Name = c.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                                            }).ToList(),
                        }).ToList();

            return item;
        }

        public void CreatePromotionAndDiscount( PromotionAndDiscountCreate model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            string urlPhoto = FileManager.SavePromotionAndDiscountPhoto(model.UrlPhotoType);
            OrganizationPromotionAndDiscount organizationPromotionAndDiscount = new OrganizationPromotionAndDiscount()
            {
                CallCount = 0,
                ClickCount = 0,
                Priority = model.Priority,
                DateTimeCanceled = model.DateCanceled,
                OrganizationCardId = model.OrganizationId,
                ViewCount = 0,
                BannerUrl = urlPhoto,
                CreatedAt = model.CreatedAt,
            };
            db.OrganizationPromotionAndDiscounts.Add(organizationPromotionAndDiscount);
            db.SaveChanges();
        }

        public List<PromotionAndDiscountVmMdl> SelectListPromotionAndDiscount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            string langcode = LanguageController.CurrentCultureCode;

            var item = (from pd in db.OrganizationPromotionAndDiscounts

                        select new PromotionAndDiscountVmMdl
                        {
                            OrganizationName = pd.OrganizationCard.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            CanceledDateTime = pd.DateTimeCanceled,
                            CreatedAt = pd.CreatedAt,
                        }).ToList();

            return item;
        }
    }
}