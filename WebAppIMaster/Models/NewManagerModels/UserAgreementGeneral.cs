using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class UserAgreementGeneral
    {
        public int Id { get; set; }
        public string Description_kz { get; set; }
        public string Description_ru { get; set; }
    }
    public class UserAgreementGeneralVmMdl
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}