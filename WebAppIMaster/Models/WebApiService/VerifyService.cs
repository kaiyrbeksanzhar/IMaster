﻿using System;
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

        public bool PhoneVerify( string PhoneNumber )
        {
            string sendPhoneNumber = PhoneNumber;
            string sendphoneNumber = sendPhoneNumber.Replace("+", "");
            string Phonenumber = PhoneNumber.Replace(" ", "");
            Phonenumber = PhoneNumber.Replace("+7", "8");

            Phonenumber = System.Text.RegularExpressions.Regex.Replace(Phonenumber, @"\s+", "");
            string phonenumber = PhoneNumber.Substring(Phonenumber.Length - 10, 10);


            var model = db.Users.Where(u => u.PhoneNumber.Contains(Phonenumber)).SingleOrDefault();
            if (model == null)
            {
                return false;
            }

            ManageController manager = new ManageController();
            AddPhoneNumberViewModel addPhone = new AddPhoneNumberViewModel()
            {
                Number = Phonenumber
            };
            //string code = manager.AddPhoneNumber1(addPhone, model.User.Id);
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            //const string accountSid = "AC447e8467b7de404485f857a3495acfbf";
            //const string authToken = "59a8ffe9a848c07bfe598a99c43abfd2";

            //Random random = new Random();
            //int code = random.Next(1000, 9999);
            int code = 4444;
            //TwilioClient.Init(accountSid, authToken);
            //var message = MessageResource.Create(
            //    body: "Ваше код безопасности:" + code,
            //    from: new Twilio.Types.PhoneNumber("+14193860152"),
            //    to: new Twilio.Types.PhoneNumber(Phonenumber)
            //);

            //string code = manager.AddPhoneNumber1(addPhone, model.User.Id);
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            //const string accountSid = "AC447e8467b7de404485f857a3495acfbf";
            //const string authToken = "59a8ffe9a848c07bfe598a99c43abfd2";

            //Random random = new Random();
            //string code = random.Next(1000, 9999).ToString();
            //string client_message = random.Next(100_000, 999_999).ToString();
            //string sender = "i-Master";
            //KcellService.PostKcell(client_message_id: client_message, sender: "I-Master", recipient: sendphoneNumber, message_text: code);

            PhoneCheckingCode phoneCheckingCode = null;
            var phoneCheckingcode = db.phoneCheckingCodes.Where(pcc => pcc.PhoneNumber.Contains(Phonenumber)).FirstOrDefault();
            if (phoneCheckingcode == null)
            {
                phoneCheckingCode = new PhoneCheckingCode()
                {
                    PhoneNumber = Phonenumber,
                    CheckingCode = code.ToString(),
                    DateTime = DateTime.Now
                };
                db.phoneCheckingCodes.Add(phoneCheckingCode);
            }
            else
            {
                phoneCheckingcode.CheckingCode = code.ToString();
                phoneCheckingcode.DateTime = DateTime.Now;
                db.Entry(phoneCheckingcode).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return true;



        }


        public bool PhoneVerify1( string PhoneNumber )
        {
            string dbphonenumber = PhoneNumber;
            string sendPhoneNumber = PhoneNumber;
            string sendphoneNumber = sendPhoneNumber.Replace("+", "");
            string Phonenumber = PhoneNumber.Replace(" ", "");
            string dbPhoneNumber = dbphonenumber.Replace("+7", "8");
            Phonenumber = PhoneNumber.Replace("+7", "");

            Phonenumber = System.Text.RegularExpressions.Regex.Replace(Phonenumber, @"\s+", "");
            string phonenumber = PhoneNumber.Substring(Phonenumber.Length - 10, 10);


            var model = db.Users.Where(u => u.PhoneNumber.Contains(Phonenumber)).SingleOrDefault();

            ManageController manager = new ManageController();

            //string code = manager.AddPhoneNumber1(addPhone, model.User.Id);
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC447e8467b7de404485f857a3495acfbf";
            const string authToken = "59a8ffe9a848c07bfe598a99c43abfd2";

            Random random = new Random();
            //string code = random.Next(1000, 9999).ToString();
            //string client_message = random.Next(100_000, 999_999).ToString();
            //string sender = "i-Master";
            int code = 4444;
            //TwilioClient.Init(accountSid, authToken);
            //var message = MessageResource.Create(
            //    body: "Ваше код безопасности:" + code,
            //    from: new Twilio.Types.PhoneNumber("+14193860152"),
            //    to: new Twilio.Types.PhoneNumber(Phonenumber)
            //);
            //KcellService.PostKcell(client_message_id: client_message, sender: "I-Master", recipient: sendphoneNumber, message_text: code);
            PhoneCheckingCode phoneCheckingCode = null;
            var phoneCheckingcode = db.phoneCheckingCodes.Where(pcc => pcc.PhoneNumber.Contains(Phonenumber)).FirstOrDefault();
            if (phoneCheckingcode == null)
            {
                phoneCheckingCode = new PhoneCheckingCode()
                {
                    PhoneNumber = dbPhoneNumber,
                    CheckingCode = code.ToString(),
                    DateTime = DateTime.Now
                };
                db.phoneCheckingCodes.Add(phoneCheckingCode);
            }
            else
            {
                phoneCheckingcode.CheckingCode = code.ToString();
                phoneCheckingcode.DateTime = DateTime.Now;
                db.Entry(phoneCheckingcode).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return true;

        }
    }
}