using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class SupportServiceMdl
    {
        public class SupportInsertMdl
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public int CityId { get; set; }
            public string Description { get; set; }
            public string PhoneNumber { get; set; }
            public TypeMessage Type { get; set; }
        }

        public class SupportSelectList
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string CityName { get; set; }
            public string Description { get; set; }
            public string PhoneNumber { get; set; }
            public TypeMessage Type { get; set; }
            public string FileUrl { get; set; }
        }

        public class Select
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string CityName { get; set; }
            public string Description { get; set; }
            public string PhoneNumber { get; set; }
            public TypeMessage Type { get; set; }
            public string FileUrl { get; set; }
        }
    }
}