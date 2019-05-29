using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class SpecializationLang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Langcode { get; set; }

        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
