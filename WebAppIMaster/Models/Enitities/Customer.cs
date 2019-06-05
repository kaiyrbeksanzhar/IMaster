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
    public class Customer
    {
        public Customer()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
        }

        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public decimal? Bonus { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public StatusCustomer? Status { get; set; }

        public int? InCityId { get; set; }
        public virtual City InCity { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
