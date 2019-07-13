using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class OrganizationCreateMdl
    {
        public string Name_kz { get; set; }
        public string Name_ru { get; set; }
        public string ShortDescription_kz { get; set; }
        public string ShortDescription_ru { get; set; }
        public TarifOrganizationMarket TarifType { get; set; }
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

    public class OrganizationVmMdl
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string OrganizationName { get; set; }
        public string CategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string VidoeUrl { get; set; }
        public string VideoUrlkz { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtraPhoneNumber { get; set; }
        public string SiteUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Photos> photos { get; set; }

        public class Photos
        {
            public string PhotoUrl { get; set; }
        }

        public string PhotoPromotionAndDiscountUrl { get; set; }
        public int PhotoPromotionAndDiscountId { get; set; }
    }

    public class OrganizationListVMMdl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public List<Categories> Category { get; set; }
        public class Categories
        {
            public string CategoryNameMarket { get; set; }
        }

    }
    public class PhotoCreateOrganizationMdl
    {
        public int OrganizationId { get; set; }
        [NotMapped]
        public string PhotoUrl1 { get; set; }
        public HttpPostedFileBase PhotoUrl1Type { get; set; }
        [NotMapped]
        public string PhotoUrl2 { get; set; }
        public HttpPostedFileBase PhotoUrl2Type { get; set; }
        [NotMapped]
        public string PhotoUrl3 { get; set; }
        public HttpPostedFileBase PhotoUrl3Type { get; set; }
        [NotMapped]
        public string PhotoUrl4 { get; set; }
        public HttpPostedFileBase PhotoUrl4Type { get; set; }

    }
}

