using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WebAppIMaster.Controllers;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models.WebApiService
{
    public class VerifyService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public VerifyService( ApplicationDbContext db ) => this.db = db;

        public bool  PhoneVerify(string PhoneNumber)
        {
            var model = db.Users.Where(u => u.PhoneNumber == PhoneNumber).SingleOrDefault();
            if(model == null)
            {
                return false;
            }

            string phonenumber = "+" + model.PhoneNumber;
            ManageController manager = new ManageController();
            AddPhoneNumberViewModel addPhone = new AddPhoneNumberViewModel()
            {
                Number = model.PhoneNumber
            };
            //string code = manager.AddPhoneNumber1(addPhone, model.User.Id);
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC447e8467b7de404485f857a3495acfbf";
            const string authToken = "59a8ffe9a848c07bfe598a99c43abfd2";

            Random random = new Random();
            int code = random.Next(100000, 999999);
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: "Ваше код безопасности:" + code,
                from: new Twilio.Types.PhoneNumber("+14193860152"),
                to: new Twilio.Types.PhoneNumber(phonenumber)
            );
            PhoneCheckingCode phoneCheckingCode = new PhoneCheckingCode()
            {
                PhoneNumber = phonenumber,
                CheckingCode = code.ToString(),
                DateTime = DateTime.Now
            };
            db.phoneCheckingCodes.Add(phoneCheckingCode);
            db.SaveChanges();
            return true;
        }
    }
}