using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enums;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;
using WebAppWebAppIMaster;
using Microsoft.Owin;
using static WebAppWebAppIMaster.SmsService;
using WebAppIMaster.Controllers;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;

namespace WebAppIMaster.Models.WebApiService
{
    public class ClientProfileService : Controller, IClientProfileService
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ClientProfileService( ApplicationDbContext db ) => this.db = db;


        public void EditCurrentClientProfile( Controller controller, ClientProfileMdl.ClientProfileEdit item )
        {
            //Image img;
            string photourl = " ";
            byte[] bytes = null;
            //using (MemoryStream mStream = new MemoryStream(item.AvatarFile))
            //{
            //    img = Image.FromStream(mStream);
            //    photourl = FileManager.SavePhoto(img);
            //}
            if (item.AvatarFile == null)
            {
                photourl = null;
            }
            else
            {
                bytes = Convert.FromBase64String(item.AvatarFile);
            }
            Customer customer = new Customer();
            string langkz = LanguageController.GetKzCode();
            string langru = LanguageController.GetRuCode();
            customer = db.Customers.Find(item.Id);
            customer.LastName = item.Lastname;
            customer.FirstName = item.Firstname;
            customer.FatherName = item.Fathername == null ? " " : item.Fathername;
            customer.Photo = bytes;
            customer.AvatarType = item.AvatarFileType;
            customer.InCityId = item.RegionId;
            customer.ApplicationUser.GenderId = item.GenderId;
            db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }



        public ClientProfileMdl.ClientProfileView GetCurrentClientProfileView( string phoneNumber )
        {
            Image img = null;
            byte[] Imagesbyte = null;
            string PhotoType = " ";
            string langcode = LanguageController.CurrentCultureCode;
            string PhoneNumber = System.Text.RegularExpressions.Regex.Replace(phoneNumber, @"\s+", "");
            string phonenumber = PhoneNumber.Substring(PhoneNumber.Length - 10, 10);

            var item = db.Customers.Where(u => u.PhoneNumber.Contains(phonenumber)).Select(model => new
            {
                Id = model.Id,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                AvatarFile = model.Photo,
                Fathername = model.FatherName,
                RegionId = 1,
                Phonenumber = model.ApplicationUser.PhoneNumber,
                AvatarFileType = model.AvatarType,
                Bonus = (int?)model.Bonus,
                GenderName = model.ApplicationUser.GenderId == 1 ? "Male" : "Female",
                GenderId = model.ApplicationUser.GenderId,
                CityName = model.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),

            }).SingleOrDefault();

            if (item.AvatarFile != null)
            {
                Imagesbyte = item.AvatarFile;
                PhotoType = item.AvatarFileType;
            }
            return new ClientProfileMdl.ClientProfileView()
            {
                CustomerId = item.Id,
                Firstname = item.Firstname,
                Lastname = item.Lastname,
                AvatarFile = Imagesbyte,
                Fathername = item.Fathername,
                RegionId = 1,
                Phonenumber = item.Phonenumber,
                AvatarFileType = PhotoType,
                Bonus = (int?)item.Bonus,
                GenderId = item.GenderId,
                CityName = item.CityName,
                Gender = item.GenderName,
            };
        }

        public async Task<string> Register( ClientProfileMdl.ClientProfileRegister item )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            ApplicationUser user = null;
            RegisterViewModel model = new RegisterViewModel()
            {
                LastName = item.LastName,
                FirstName = item.FirstName,
                FatherName = item.FatherName,
                GenderId = item.GenderId,
                PhoneNumber = item.PhoneNumber,
                UserName = item.PhoneNumber,
            };
            if (ModelState.IsValid)
            {
                user = new ApplicationUser
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    FatherName = model.FatherName,
                    GenderId = model.GenderId,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.PhoneNumber,
                };
                try
                {
                    var result = await UserManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        using (ApplicationDbContext db = new ApplicationDbContext())
                        {
                            Customer customer = new Customer()
                            {
                                LastName = user.LastName,
                                FirstName = user.FirstName,
                                FatherName = user.FatherName,
                                PhoneNumber = user.PhoneNumber,
                                InCityId = item.RegionId
                            };
                            db.Customers.Add(customer);
                            db.SaveChanges();
                        };
                        // Дополнительные сведения о включении подтверждения учетной записи и сброса пароля см. на странице https://go.microsoft.com/fwlink/?LinkID=320771.
                        // Отправка сообщения электронной почты с этой ссылкой
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");

                    }
                }
                catch (Exception e)
                {
                    int ijo = 5;
                }
            }
            return user.Id;
        }

        public void SendPhotoToClientProfule(string url, string clientProfileId)
        {
            var Customer = db.Customers.Where(co => co.Id == clientProfileId).FirstOrDefault();
            if (Customer != null)
            {
                if (Customer.AvatarUrl == null)
                {
                    Customer.AvatarUrl = url;
                }
            }
            db.Entry(Customer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }



        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ClientProfileService( ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        protected override void Dispose( bool disposing )
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors( IdentityResult result )
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal( string returnUrl )
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ClientProfileMdl.ClientProfileView GetCurrentCustomerClientProfileView( string customerId )
       {
            Image img = null;
            byte[] Imagesbyte = null;
            string PhotoType = null;
            string langcode = LanguageController.CurrentCultureCode;
            var item = db.Customers.Where(u => u.Id == customerId).Select(model => new
            {
                Id = model.Id,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                AvatarFile = model.Photo,
                Fathername = model.FatherName,
                RegionId = 1,
                Phonenumber = model.ApplicationUser.PhoneNumber,
                AvatarFileType = model.AvatarType,
                Bonus = (int?)model.Bonus,
                GenderName = model.ApplicationUser.GenderId == 1 ? "Male" : "Female",
                GenderId = model.ApplicationUser.GenderId,
                CityName = model.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),

            }).SingleOrDefault();
            if (item == null) throw new KeyNotFoundException();
            if (item.AvatarFile != null)
            {
                Imagesbyte = item.AvatarFile;
                PhotoType = item.AvatarFileType;
            }
            return new ClientProfileMdl.ClientProfileView()
            {
                CustomerId = item.Id,
                Firstname = item.Firstname,
                Lastname = item.Lastname,
                AvatarFile = Imagesbyte,
                Fathername = item.Fathername,
                RegionId = 1,
                Phonenumber = item.Phonenumber,
                AvatarFileType = PhotoType,
                Bonus = (int?)item.Bonus,
                GenderId = item.GenderId,
                CityName = item.CityName,
                Gender = item.GenderName
            };
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult( string provider, string redirectUri )
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult( string provider, string redirectUri, string userId )
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult( ControllerContext context )
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}