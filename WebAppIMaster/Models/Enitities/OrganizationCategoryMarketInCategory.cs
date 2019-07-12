using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class OrganizationCategoryMarketInCategory
    {
        [Key]
        [Column(Order = 0)]
        public int CategoryId { get; set; }
        public virtual Category  Category { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CategoryMarketId { get; set; }
        public virtual CategoryMarket  CategoryMarket { get; set; }

        public DateTime CreatedAt_Date { get; set; }
    }
}