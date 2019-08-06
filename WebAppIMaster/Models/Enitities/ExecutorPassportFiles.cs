using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class ExecutorPassportFiles
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string ImageUrl { get; set; }
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }
    }
}
