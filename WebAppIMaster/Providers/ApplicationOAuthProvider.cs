using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
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
            string checkingCode = context.Password;

            
            string phonenumber = phoneNumber.Substring(phoneNumber.Length - 10, 10);
            ApplicationUser user = null;
            PhoneCheckingCode phoneCheckingCode = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                phoneCheckingCode = db.phoneCheckingCodes.Where(pcc => pcc.PhoneNumber.Contains(phonenumber) 
                || pcc.CheckingCode.Contains(checkingCode) ).FirstOrDefault();

                    
                if (phoneCheckingCode == null)
                {
                    context.SetError("invalid_grant", "Номер пользователя или Code  неправильно.");
                    return;
                }
                int secsInAMin = 60;
                int secsInAnHour = 60 * secsInAMin;
                int secsInADay = 24 * secsInAnHour;
                double secsInAYear = (int)365.25 * secsInADay;
                int totalSeconds = (int)(phoneCheckingCode.DateTime.Year * secsInAYear) +
                         (phoneCheckingCode.DateTime.DayOfYear * secsInADay) +
                         (phoneCheckingCode.DateTime.Hour * secsInAnHour) +
                         (phoneCheckingCode.DateTime.Minute * secsInAMin) +
                         phoneCheckingCode.DateTime.Second;
                totalSeconds = totalSeconds + 240;
                int totalDateTimeNowSeconds = (int)(DateTime.Now.Year * secsInAYear) +
                         (DateTime.Now.DayOfYear * secsInADay) +
                         (DateTime.Now.Hour * secsInAnHour) +
                         (DateTime.Now.Minute * secsInAMin) +
                         DateTime.Now.Second;
                if (totalSeconds > totalDateTimeNowSeconds)
                {
                    user = db.Users.Where(u => u.PhoneNumber == phoneCheckingCode.PhoneNumber).SingleOrDefault();
                }
                else
                {
                    context.SetError("invalid_grant", "Code  устарело.");
                    return;
                }
            }

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);



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