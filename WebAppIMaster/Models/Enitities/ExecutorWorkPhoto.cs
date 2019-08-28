using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class ExecutorWorkPhoto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }
    }
}