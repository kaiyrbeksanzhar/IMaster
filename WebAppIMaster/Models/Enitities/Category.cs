using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class Category
    {
        public Category()
        {
            Langs = new HashSet<CategoryLang>();
            Executors = new HashSet<ExecutorCategoryAndSpecialization>();
            CustomerOrders = new HashSet<CustomerOrder>();
            Specializations = new HashSet<Specialization>();
        }

        public int Id { get; set; }
        public int Priority { get; set; }
        public string UrlPhoto { get; set; }

        public ICollection<Specialization> Specializations { get; set; }
        public ICollection<CategoryLang> Langs { get; set; }
        public ICollection<ExecutorCategoryAndSpecialization> Executors { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; }


    }
}
