using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class PopulationQuestion
    {
        public PopulationQuestion()
        {
            Langs = new HashSet<PopulationQuestionLangs>();
        }
        public int Id { get; set; }

        public int PopulationCategoryId { get; set; }
        public virtual PopulationCategory PopulationCategory { get; set; }

        public ICollection<PopulationQuestionLangs> Langs { get; set; }
    }
}