using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class Service
    {
        public Service()
        {
            Langs = new HashSet<ServiceLang>();
            Executors = new HashSet<ExecutorService>();
        }
        public int Id { get; set; }
        public ICollection<ServiceLang> Langs { get; set; }
        public ICollection<ExecutorService> Executors { get; set; }
    }
}
