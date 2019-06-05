using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class PhoneCheckingCode
    {
        [Key]
        [Column(Order = 0)]
        public string PhoneNumber { get; set; }
        [Key]
        [Column(Order = 1)]
        public string CheckingCode { get; set; }

        public DateTime DateTime { get; set; }
    }
}