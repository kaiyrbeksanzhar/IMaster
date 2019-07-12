using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class OrganizationCreateMdl
    {
        public string Name_kz { get; set; }
        public string Name_ru { get; set; }
        public string ShortDescription_kz { get; set; }
        public string ShortDescription_ru { get; set; }
        public Tarif TarifType { get; set; }
        [NotMapped]
        public string LogoTypeUrl { get; set; }
        public HttpPostedFileBase LogoType { get; set; }

        public List<CategoryOrganization> categoryOrganizations { get; set; }
        public class CategoryOrganization
        {
            public int CategoryOrganizationId { get; set; }
        }
        
        public string VideoUrl { get; set; }
        public string VideoUrl_kz { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtraPhoneNumber { get; set; }
        public string SiteUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string latitude { get; set; }
        public int CityId { get; set; }

    }

    public class CategoryMarketInsert
    {
        public string CategoryName_kz { get; set; }
        public string CategoryName_ru { get; set; }
        public List<CategoryMarketInCategory>  categoryMarketInCategories { get; set; }
        public class CategoryMarketInCategory
        {
            public int CategoryMarketInCategoryId { get; set; }
        }
    }

    public class CategoryMarketVmMdl
    {
        public int Id { get; set; }
        public string CategoryMarketName { get; set; }
        public List<CatgoryName> CatgoryNames { get; set; }

        public class CatgoryName
        {
            public int Id { get; set; }
            public string  Name { get; set; }
        }
    }

    public class PromotionAndDiscountCreate
    {
        public int OrganizationId { get; set; }
        [NotMapped]
        public int UrlPhoto { get; set; }
        public HttpPostedFileBase UrlPhotoType { get; set; }
        public DateTime DateCanceled { get; set; }
        public int Priority { get; set; }
        public int ViewCount { get; set; }
        public int ClickCount { get; set; }
        public int CallCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PromotionAndDiscountVmMdl
    {
        public string OrganizationName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CanceledDateTime { get; set; }  
    }
}