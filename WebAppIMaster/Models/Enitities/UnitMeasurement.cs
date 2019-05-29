using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class UnitMeasurement
    {
        public UnitMeasurement()
        {
            Langs = new HashSet<UnitMeasurementLang>();
            ExecutorServices = new HashSet<ExecutorService>();
        }

        public int Id { get; set; }

        public ICollection<UnitMeasurementLang> Langs { get; set; }
        public ICollection<ExecutorService> ExecutorServices { get; set; }
    }
}
