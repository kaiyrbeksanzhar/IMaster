using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class ServiceLang
    {
        public int Id { get; set; }
        public string Langcode { get; set; }
        public string Name { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
