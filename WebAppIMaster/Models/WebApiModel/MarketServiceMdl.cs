using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.WebApiModel
{
    public class MarketServiceMdl
    {
        public class MarketItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public byte[] LogotypeFile { get; set; }
            public string LogotypeFileType { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int SpecialtyId { get; set; }
            public string SpecialtyName { get; set; }
            public int RegionId { get; set; }
            public string RegionName { get; set; }
            public string Address { get; set; }
        }

        public class MarketDetails
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public byte[] LogoFile { get; set; }
            public string LogoFileType { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int SpecialtyId { get; set; }
            public string SpecialtyName { get; set; }
            public Dictionary<byte[], string> PhotoFiles { get; set; }
            public string AboutCompany { get; set; }
            public Dictionary<byte[], string> PromotionAndDiscounts { get; set; }
            public Dictionary<string, string> Serivces { get; set; }
            public List<string> ContactPhoneNumbers { get; set; }
            public string Email { get; set; }
            public string SiteUrl { get; set; }
            public int RegionId { get; set; }
            public string RegionName { get; set; }
            public string Address { get; set; }
            public string Coordinates { get; set; }
        }

    }
}