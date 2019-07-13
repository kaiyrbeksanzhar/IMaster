using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models
{
    public class Category
    {
        public Category()
        {
            Langs = new HashSet<CategoryLang>();
            executorSpecializations = new HashSet<ExecutorSpecialization>();
            Specializations = new HashSet<Specialization>();
            organizationCategoryMarketInCategories = new HashSet<OrganizationCategoryMarketInCategory>();
        }

        public int Id { get; set; }
        public int Priority { get; set; }
        public string UrlPhoto { get; set; }

        public ICollection<Specialization> Specializations { get; set; }
        public ICollection<CategoryLang> Langs { get; set; }
        public ICollection<OrganizationCategoryMarketInCategory> organizationCategoryMarketInCategories { get; set; }
        public ICollection<ExecutorSpecialization>  executorSpecializations { get; set; }

    }
}
