using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class PopulationCategory
    {
        public PopulationCategory()
        {
            Langs = new HashSet<PopulationCategoryLangs>();
            populationCategories = new HashSet<PopulationCategory>();
        }
        public int Id { get; set; }
        public int Property { get; set; }

        public ICollection<PopulationCategoryLangs> Langs { get; set; }
        public ICollection<PopulationCategory>   populationCategories { get; set; }
    }
}