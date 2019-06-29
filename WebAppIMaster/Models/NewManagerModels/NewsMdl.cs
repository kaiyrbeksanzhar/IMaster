using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class NewsCreateMdl
    {
        public int Id { get; set; }
        public string Title_kz { get; set; }
        public string Title_ru { get; set; }
        [NotMapped]
        public string Url { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        [NotMapped]
        public string Url_kz { get; set; }
        public HttpPostedFileBase Photo_kz { get; set; }
        public Roles TypeRoles { get; set; }
        public string ShortText_kz { get; set; }
        public string Description_kz { get; set; }
        public string ShortText_ru { get; set; }
        public string Description_ru { get; set; }
    }

    public class NewsVmMdl
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public  Roles TypeRoles { get; set; }
    }

}