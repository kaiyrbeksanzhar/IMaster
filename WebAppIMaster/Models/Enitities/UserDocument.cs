using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class UserDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public bool Verified { get; set; }
        public DateTime AddedDateTime { get; set; }
        public DateTime? VerifiedDateTime { get; set; }

        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
