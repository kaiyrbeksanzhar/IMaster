using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class SendBonus
    {
        public int Id { get; set; }
        public decimal Bonus { get; set; }
        public string Comment { get; set; }
        public int? CustomerId { get; set; }
        public int? ExecutorId { get; set; }
    }
}
