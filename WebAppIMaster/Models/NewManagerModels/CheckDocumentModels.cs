using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class CheckDocumentModels
    {
    }

    public class DocumentDetail
    {
        public string UserId { get; set; }
        public int DocId { get; set; }
        public string UserName { get; set; }
        public DateTime? BirthDay { get; set; }
        public Gender? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int? Rating { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? RegistrationDateTime { get; set; }
        public string SpecialityName { get; set; }

        public Status DocumentState { get; set; }
        public string City { get; set; }
        public string DocUrl { get; set; }
    }
}