using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.WebApiModel
{
    public class PopulationQuestionServiceMdl
    {
        public class PopulationQuestionList
        {
            public string PopulationCategoryName { get; set; }
            public int PopulationCategoryId { get; set; }

            public List<PopulationList> populationQuestionList { get; set; }
            public class PopulationList
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
            }
        }
        public class PopulationSelect
        {
            public string PopulationCategoryName { get; set; }

            public List<PopulationList> populationQuestionList { get; set; }
            public class PopulationList
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
            }
        }
    }
}