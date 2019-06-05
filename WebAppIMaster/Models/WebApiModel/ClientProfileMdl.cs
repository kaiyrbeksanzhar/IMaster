using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ClientProfileMdl
    {
        public class ClientProfileView
        {
            public string CustomerId { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Fathername { get; set; }

            public int RegionId { get; set; }
            public string CityName { get; set; }
            public int Bonus { get; set; }
            public int GenderId { get; set; }
            public string Gender { get; set; }
            public string Phonenumber { get; set; }

            public byte[] AvatarFile { get; set; }
            public string AvatarFileType { get; set; }
        }

        public class ClientProfileEdit
        {
            public string Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Fathername { get; set; }

            public byte[] AvatarFile { get; set; }
            public string AvatarFileType { get; set; }

            public int RegionId { get; set; }

            public int GenderId { get; set; }
        }

        public class ClientProfileRegister
        {
            public string PhoneNumber { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; } 
            public string FatherName { get; set; }
            public int GenderId { get; set; }
            public int RegionId { get; set; }
            public int MarketingId { get; set; }

        }
    }
}