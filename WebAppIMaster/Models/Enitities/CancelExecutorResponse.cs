using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models
{
    public class CancelExecutorResponse
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int OrderId { get; set; }
        public string ExecutorId { get; set; }
    }
}