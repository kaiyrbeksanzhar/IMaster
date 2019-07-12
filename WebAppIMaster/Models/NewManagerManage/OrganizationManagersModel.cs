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

        public OrganizationManagersModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool OrganizationInsert(OrganizationCreateMdl model)
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
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void OrganizationCategoryInsert(CategoryMarketInsert model)
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

        public void CreatePromotionAndDiscount(PromotionAndDiscountCreate model)
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