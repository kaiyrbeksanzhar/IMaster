using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class PopulationCategoryLangs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }

        public int PopulationCategoryId { get; set; }
        public virtual PopulationCategory  PopulationCategory { get; set; }
    }
}