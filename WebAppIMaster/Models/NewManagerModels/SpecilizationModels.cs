using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class SpecilizationModels
    {
        public class SpecilizationSelect
        {
            public string CategoryName { get; set; }
            public int Id { get; set; }
            public int Priority { get; set; }
            public string Name { get; set; }
            public string PhotoUrl { get; set; }
        }
        public class SpecilizationCreate
        {
            public int CategoryId { get; set; }
            public string Name_kz { get; set; }
            public string Name_ru { get; set; }
            public string PhotoUrl { get; set; }
            public int Priority { get; set; }
            [ScaffoldColumn(false)]
            [NotMapped]
            public HttpPostedFileBase Photo { get; set; }
            public string Langcode { get; set; }
        }
        public class SpecilizationEdit
        {
            public int Id { get; set; }
            public string Name_kz { get; set; }
            public string Name_ru { get; set; }
            public int Priority { get; set; }

            // Добавлено CategoryId и причины ошибки
         //   public int CategoryId { get; set; }
            public string PhotoUrl { get; set; }
            [ScaffoldColumn(false)]
            [NotMapped]
            public HttpPostedFileBase Photo { get; set; }
        }
    }
}