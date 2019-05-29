using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class OrganizationPromotionAndDiscount
    {
        public int Id { get; set; }
        public string BannerUrl { get; set; }
        public int ViewCount { get; set; }
        public int ClickCount { get; set; }
        public int CallCount { get; set; }

        public int OrganizationCardId { get; set; }
        public virtual Organization OrganizationCard { get; set; }
    }
}
