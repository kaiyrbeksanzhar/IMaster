using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models.Enitities
{
    public class RateHowItWorks
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PopulationQuestionId { get; set; }
        public virtual PopulationQuestion PopulationQuestion { get; set; }


        public Estimate Estimate { get; set; }

        public DateTime CreatedAt_Date { get; set; }
    }
}