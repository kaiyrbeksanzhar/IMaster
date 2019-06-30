using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class SupportVmMdl
    {
        public string FIO { get; set; }
        public string CityName { get; set; }
        public TypeMessage Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Desciption { get; set; }
        public int Id { get; set; }
    }
}