using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class Specialization
    {
        public Specialization()
        {
            Langs = new HashSet<SpecializationLang>();
        }
        public int Id { get; set; }
        public int Priority { get; set; }
        public string PhotoUrl { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<SpecializationLang> Langs { get; set; }
    }
}
