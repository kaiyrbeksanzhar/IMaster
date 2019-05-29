using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class News
    {
        public News()
        {
            Langs = new HashSet<NewsLang>();
        }

        public int Id { get; set; }
        public int ViewsNumber { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string PhotoUrl1 { get; set; }
        public string PhotoUrl2Kazakh { get; set; }
        public Roles TypeRoles { get; set; }

        public ICollection<NewsLang> Langs { get; set; }
    }
}
