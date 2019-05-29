using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Fathername { get; set; }
        public string AvatarUrl { get; set; }

        public int AccomodationCityId { get; set; }
        public virtual City AccomodationCity { get; set; }


    }
}
