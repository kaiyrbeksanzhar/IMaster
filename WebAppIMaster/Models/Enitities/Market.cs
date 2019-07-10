using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models
{
    public class Market
    {
        public Market()
        {
            Langs = new HashSet<MarketLangs>();
            organizationPromotionAndDiscounts = new HashSet<OrganizationPromotionAndDiscount>();
        }
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string LogoFile { get; set; }
        public string LogoFileType { get; set; }
        public Tarif Tarif { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public string SiteUrl { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
        public int CategoryMarketId { get; set; }
        public string VideoUrl { get; set; }
        public string VideoUrl_kz { get; set; }
        public virtual CategoryMarket CategoryMarket { get; set; }
        public ICollection<MarketLangs> Langs { get; set; }
        public ICollection<OrganizationPromotionAndDiscount> organizationPromotionAndDiscounts { get; set; }
    }
}