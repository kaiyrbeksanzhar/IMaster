using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models.Enums
{
    public enum Roles
    {
        [Display(Name = "Исполнитель")]
        Executor,
        [Display(Name = "Клиент")]
        Customer
    }
}
