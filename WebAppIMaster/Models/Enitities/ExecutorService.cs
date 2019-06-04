using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class ExecutorService
    {
        public int Id { get; set; }

        public int CostType { get; set; }

        public string Name { get; set; }

        public int FromCost { get; set; }
        public int ToCost { get; set; }
        public int FixedCost { get; set; }

        public int UnitMeasurementId { get; set; }
        public virtual UnitMeasurement UnitMeasurement { get; set; }


        [Key]
        [Column(Order = 0)]
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }


        [Key]
        [Column(Order = 1)]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
