using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models
{
    public class Address
    {
        public Address()
        {
            Langs = new HashSet<AddressLang>();
            CustomerOrders = new HashSet<CustomerOrder>();
        }

        public int Id { get; set; }

        public ICollection<AddressLang> Langs { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}