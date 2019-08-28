using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class NewsLang
    {
        public int Id { get; set; }
        public string Langcode { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string Description { get; set; }


        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}
