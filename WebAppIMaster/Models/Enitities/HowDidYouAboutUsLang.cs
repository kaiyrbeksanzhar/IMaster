using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class HowDidYouAboutUsLang
    {
        public int Id { get; set; }
        public string SourceName { get; set; }
        public string Langcode { get; set; }

        public int HowDidYouAboutUsId { get; set; }
        public virtual HowDidYouAboutUs HowDidYouAboutUs { get; set; }
    }
}
