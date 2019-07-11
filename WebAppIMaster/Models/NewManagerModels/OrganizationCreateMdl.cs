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
}