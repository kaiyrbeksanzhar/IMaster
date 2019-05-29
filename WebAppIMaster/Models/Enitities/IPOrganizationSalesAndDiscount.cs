using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class IPOrganizationSalesAndDiscount
    {
        [Key]
        [ForeignKey("Organization")]
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int Priroty { get; set; }
        public Status Status { get; set; }
        public Organization Organization { get; set; }
    }
}
