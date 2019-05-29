using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class AddressLang
    {
        public int Id { get; set; }
        public string Langcode { get; set; }
        public string Name { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}