using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models.Enitities
{
    public class Support
    {
        public int Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public TypeMessage  TypeMessage { get; set; }

        public string PhoneNumber { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }

        public string Description { get; set; }
    }
}