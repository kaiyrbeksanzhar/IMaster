using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models
{
    public class PhoneCheckingCode
    {
        [Key]
        public string PhoneNumber { get; set; }
        public string CheckingCode { get; set; }

        public DateTime DateTime { get; set; }
    }
}