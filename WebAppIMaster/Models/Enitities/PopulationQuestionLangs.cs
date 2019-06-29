using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class PopulationQuestionLangs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }

        public int PopulationQuestionId { get; set; }
        public virtual PopulationQuestion  PopulationQuestion { get; set; }
    }
}