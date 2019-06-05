using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class ExecutorSpecialization
    {
        [Key]
        [Column(Order = 0)]
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }

        [Key]
        [Column(Order = 1)]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
