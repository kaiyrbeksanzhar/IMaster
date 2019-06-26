using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIMaster.Models
{
    public class Response
    {
        [Key]
        [Column(Order = 0)]
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }

        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }
        public virtual CustomerOrder Order { get; set; }

        public DateTime CreatedAt_Date { get; set; }
        public string ResponseComment { get; set; }
    }
}