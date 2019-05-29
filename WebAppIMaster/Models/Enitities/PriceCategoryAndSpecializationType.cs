using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class PriceCategoryAndSpecializationType
    {
        public PriceCategoryAndSpecializationType()
        {
            priceCategoryAndSpecializations = new HashSet<PriceCategoryAndSpecialization>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ExecutorType Type { get; set; } //Bussines,Individual
        public ICollection<PriceCategoryAndSpecialization>  priceCategoryAndSpecializations { get; set; }
    }
}
