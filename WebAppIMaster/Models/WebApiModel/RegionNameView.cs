using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.WebApiModel
{
    public class RegionNameView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool LastStage { get; set; }
        public int ParentId { get; set; }
    }
}