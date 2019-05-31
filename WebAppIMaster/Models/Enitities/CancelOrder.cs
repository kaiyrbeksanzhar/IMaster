using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models.Enitities
{
    public class CancelOrder
    {
        [Key]
        [Column(Order = 0)]
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }

        [Key]
        [Column(Order = 1)]
        public int OrderExecutorId { get; set; }
        public virtual OrderExecutor OrderExecutor { get; set; }

        public OrderCancelType cancelType { get; set; }
    }
}