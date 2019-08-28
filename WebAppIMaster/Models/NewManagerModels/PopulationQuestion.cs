using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public partial class PopulationQuestion
    {
        public class PopulationSelectVmMdl
        {
            public string PopulationCategoryName { get; set; }
            public int PopulationCategoryId { get; set; }

            public List<PopulationQuestionList> populationQuestionList { get; set; }
            public class PopulationQuestionList
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
            }
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

    public class PopulationQuesEditMdl
    {
        public int Id { get; set; }
        public string Name_kz { get; set; }
        public string Name_ru { get; set; }
        public string Description_kz { get; set; }
        public string Description_ru { get; set; }
    }
}