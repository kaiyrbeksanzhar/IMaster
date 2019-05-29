using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class CategoryModels
    {
        public class CategorySelect
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Priority { get; set; }
            public int SpecializationCount { get; set; }
            public string UrlPhoto { get; set; }
        }
        public class CategoryCreate
        {
            public string Langcode { get; set; }
            public string Name_kz { get; set; }
            public string Name_ru { get; set; }
            public int Priority { get; set; }
            public string UrlPhoto { get; set; }
            [ScaffoldColumn(false)]
            [NotMapped]
            public HttpPostedFileBase Photo { get; set; }
        }
        public class CategoryEdit
        {
            public int Id { get; set; }
            public string Name_kz { get; set; }
            public string Name_ru { get; set; }
            public int Priority { get; set; }
            public string UrlPhoto { get; set; }
            [ScaffoldColumn(false)]
            [NotMapped]
            public HttpPostedFileBase Photo { get; set; }
        }
    }
}