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

namespace WebAppIMaster.Models.WebApiService
{
    public class ClientProfileService : IClientProfileService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ClientProfileService( ApplicationDbContext db ) => this.db = db;


        public void EditCurrentClientProfile( Controller controller, ClientProfileMdl.ClientProfileEdit item )
        {
            Image img;
            string photourl = " ";
            using (MemoryStream mStream = new MemoryStream(item.AvatarFile))
            {
                img = Image.FromStream(mStream);
                photourl = FileManager.SavePhoto(img);
            }
            Customer customer = new Customer();
            string langkz = LanguageController.GetKzCode();
            string langru = LanguageController.GetRuCode();
            customer = db.Customers.Find(item.Id);
            customer.LastName = item.Lastname;
            customer.FirstName = item.Firstname;
            customer.FatherName = item.Fathername == null ? " " : item.Fathername;
            customer.AvatarUrl = photourl;
            customer.InCityId = item.RegionId;
            customer.ApplicationUser.GenderId = item.GenderId;
            db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }



        public ClientProfileMdl.ClientProfileView GetCurrentClientProfileView( int Id )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.Customers.Find(Id);
            Image img = Image.FromFile(model.AvatarUrl);
            string PhotoType = model.AvatarUrl.Substring(model.AvatarUrl.LastIndexOf(".") + 1);
            byte[] Imagesbyte = FileManager.ImageToByteArray(img);
            return new ClientProfileMdl.ClientProfileView
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                AvatarFile = Imagesbyte,
                Fathername = model.FatherName != null ? model.FatherName : " ",
                RegionId = 1,
                Phonenumber = model.ApplicationUser.PhoneNumber,
                AvatarFileType = PhotoType,
                Bonus = (int)model.Bonus,
                GenderId = model.ApplicationUser.GenderId,
                CityName = model.InCity.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),

            };
        }

        public string Register( ClientProfileMdl.ClientProfileRegister item )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            var user = db.Users.Where(u => u.PhoneNumber == item.PhoneNumber).SingleOrDefault();
            Customer customer = new Customer()
            {
                Id = user.Id,
                LastName = item.LastName,
                FirstName = item.FirstName,
                FatherName = item.FatherName == null ? " " : item.FatherName,
                PhoneNumber = user.PhoneNumber,
                InCityId = item.RegionId,
                HowDidYouAboutUsId = item.MarketingId
            };
            db.Customers.Add(customer);
            db.SaveChanges();

            return customer.Id;
        }


    }
}