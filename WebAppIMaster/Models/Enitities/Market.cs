using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoFile { get; set; }
        public string LogoFileType { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string AboutCompany { get; set; }
        //public Map<byte[], string> PromotionAndDiscounts { get; set; }
        //public Map<string, string> Serivces { get; set; }
        public List<string> ContactPhoneNumbers { get; set; }
        public string Email { get; set; }
        public string SiteUrl { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
    }
}