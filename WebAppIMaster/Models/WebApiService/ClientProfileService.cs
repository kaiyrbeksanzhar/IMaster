using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enums;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class ClientProfileService : IClientProfileService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ClientProfileService(ApplicationDbContext db) => this.db = db;
        public ClientProfileMdl.ClientProfileView Get(int Id)
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.Customers.Find(Id);
            return new ClientProfileMdl.ClientProfileView
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                AvatarFile = null,
                Fathername = model.FatherName != null ? model.FatherName : " ",
                RegionId = 1,
                Phonenumber = model.ApplicationUser.PhoneNumber,
                AvatarFileType = "png",
                Bonus = 500,
                Gender = "мужской",
                RegionName = "Шымкент"
            };
        }
    }
}