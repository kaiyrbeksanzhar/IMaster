﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class CategoryMarket
    {
        public CategoryMarket()
        {
            Langs = new HashSet<CategoryMarketLang>();
            organizationCategoryMarketInCategories = new HashSet<OrganizationCategoryMarketInCategory>(); organizationCategories = new HashSet<OrganizationCategory>();
        }
        public int Id { get; set; }

        public ICollection<CategoryMarketLang> Langs { get; set; }

        public ICollection<OrganizationCategoryMarketInCategory> organizationCategoryMarketInCategories { get; set; }

        public ICollection<OrganizationCategory> organizationCategories { get; set; }
    }
}