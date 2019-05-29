using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class PriceCategoryAndSpecialization
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public virtual PriceCategoryAndSpecializationType PriceCategoryAndSpecializationType { get; set; }
        public int PriceCategoryAndSpecializationTypeId { get; set; }

        public int? CategoryId { get; set; }
        public int? SpecializationId { get; set; }
    }
}
