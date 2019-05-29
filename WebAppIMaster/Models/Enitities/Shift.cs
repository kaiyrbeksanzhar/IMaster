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
    public class Shift
    {

        [Key]
        [Column(Order = 0)]
        public int ExecutorProfileId { get; set; }
        [Key]
        [Column(Order = 1)]
        public DateTime StartDateTime { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime EndDateTime { get; set; }
        public decimal Cost { get; set; }
        public Relevance Relevance { get; set; }
    }
}
