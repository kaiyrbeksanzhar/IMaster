using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ClientProfileMdl
    {
        public class ClientProfileView
        {
            public string CustomerId { get; set; }
            public String Firstname;
            public String Lastname;
            public String Fathername;

            public int RegionId;
            public String RegionName;
            public String CityName;
            public int Bonus;
            public int GenderId;
            public String Gender;
            public String Phonenumber;

            public byte[] AvatarFile;
            public String AvatarFileType;
        }
    }
}