using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class HowDidYouAboutUs
    {
        public HowDidYouAboutUs()
        {
            Langs = new HashSet<HowDidYouAboutUsLang>();
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int? Order { get; set; }
        public ICollection<Customer> Customers { get; set; }


        public ICollection<HowDidYouAboutUsLang> Langs { get; set; }

    }
}
