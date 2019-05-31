using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync( UserManager<ApplicationUser> manager )
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }



        public ApplicationUser()
        {
            Documents = new HashSet<UserDocument>();
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public virtual UserProfile Profile { get; set; }
        public virtual Executor Executor { get; set; }
        public virtual Customer Customer { get; set; }

        public ICollection<UserDocument> Documents { get; set; }
    }
}
