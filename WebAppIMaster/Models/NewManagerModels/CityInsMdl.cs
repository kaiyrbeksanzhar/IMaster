using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class CityInsMdl
    {
        public string Name_kz { get; set; }
        public string Name_ru { get; set; }
        public string langcode { get; set; }
        public string Latitude { get; set; }
        public string Longitudey { get; set; }
        public int Preority { get; set; }
    }
}