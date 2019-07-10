using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models
{
    public class Organization
    {
        public Organization()
        {
            PromotionAndDiscounts = new HashSet<OrganizationPromotionAndDiscount>();
            Langs = new HashSet<OrganizationLang>();
            IPPhotosFiles = new HashSet<IPPhotosFiles>();
            iPOrganizationPrices = new HashSet<IPOrganizationPrice>();
            Markets = new HashSet<CategoryMarket>();
        }

        public int Id { get; set; }
        public string LogotypeUrl { get; set; }
        public decimal Costs { get; set; }
        public string YouTubeVideoUrl { get; set; }
        public string YouTubeVideoUrlkz { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtraPhoneNumber { get; set; }
        public string SiteUrl { get; set; }
        public string Email { get; set; }
        public string Longitude { get; set; }
        public string latitude { get; set; }
        public ICollection<CategoryMarket> Markets { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public ICollection<OrganizationPromotionAndDiscount> PromotionAndDiscounts { get; set; }
        public ICollection<OrganizationLang> Langs { get; set; }

        public ICollection<IPPhotosFiles> IPPhotosFiles { get; set; }
        public ICollection<IPOrganizationPrice>  iPOrganizationPrices { get; set; }

        public IPOrganizationSalesAndDiscount IPOrganizationSalesAndDiscount { get; set; }
    }
}
