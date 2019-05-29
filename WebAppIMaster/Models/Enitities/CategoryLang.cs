using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class CategoryLang
    {
        public int Id { get; set; }
        public string Langcode { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }
    }
}
