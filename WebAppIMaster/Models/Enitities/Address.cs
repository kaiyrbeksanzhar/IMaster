using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class Address
    {
        public Address()
        {
            Langs = new HashSet<AddressLang>();
        }

        public int Id { get; set; }

        public ICollection<AddressLang> Langs { get; set; }
    }
}