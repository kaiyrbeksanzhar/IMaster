using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class PopulationQuestion
    {
        public class PopulationSelectVmMdl
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PopulationCategoryName { get; set; }
            public string Description { get; set; }
        }

        public class PopulationCreateMdl
        {
            public string Name_kz { get; set; }
            public string Name_ru { get; set; }
            public string Description_kz { get; set; }
            public string Description_ru { get; set; }
        }
        public class PopulationEditMdl
        {
            public int Id { get; set; }
            public string Name_kz { get; set; }
            public string Name_ru { get; set; }
            public string Description_kz { get; set; }
            public string Description_ru { get; set; }
        }
    }
}