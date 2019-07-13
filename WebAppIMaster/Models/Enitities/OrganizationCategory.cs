using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class OrganizationCategory
    {
        [Key]
        [Column(Order = 0)]
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CategoryMarketId { get; set; }
        public virtual CategoryMarket  CategoryMarket { get; set; }
    }
}