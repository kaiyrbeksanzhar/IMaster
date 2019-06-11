using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebAppIMaster.Models;
using WebAppIMaster.Models.Enitities;
using static WebAppWebAppIMaster.SmsService;

namespace WebAppIMaster.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider( string publicClientId )
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials( OAuthGrantResourceOwnerCredentialsContext context )
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            string phoneNumber = System.Text.RegularExpressions.Regex.Replace(context.UserName, @"\s+", "");
            string phonenumber = phoneNumber.Substring(phoneNumber.Length - 10, 10);
            string checkingCode = context.Password;



            ApplicationUser user = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                DateTime now = DateTime.Now;
                bool any = (from phcc in db.phoneCheckingCodes
                            where phcc.PhoneNumber.Contains(phonenumber)
                            where phcc.CheckingCode == checkingCode
                            where DbFunctions.DiffMinutes(phcc.DateTime, now) <= 5
                            select phcc).Any();
                
                if (any == false)
                {
                    context.SetError("invalid_grant", "Code  устарело.");
                    return;
                }
                user = await db.Users.Where(u => u.PhoneNumber.Contains(phonenumber)).SingleOrDefaultAsync();
            }
            
            if (user == null)
            {
                return;
            }
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint( OAuthTokenEndpointContext context )
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication( OAuthValidateClientAuthenticationContext context )
        {
            // Учетные данные владельца ресурса не содержат идентификатор клиента.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri( OAuthValidateClientRedirectUriContext context )
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties( string userName )
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}