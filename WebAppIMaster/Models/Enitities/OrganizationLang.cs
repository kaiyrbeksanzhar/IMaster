using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class OrganizationLang
    {
        public int Id { get; set; }
        public string Langcode { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string BannerUrl { get; set; }
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
