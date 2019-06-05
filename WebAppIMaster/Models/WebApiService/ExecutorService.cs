﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WebAppIMaster.Controllers;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enums;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;
using static WebAppWebAppIMaster.SmsService;

namespace WebAppIMaster.Models.WebApiService
{
    public class ExecutorService : IExecutorService
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ExecutorService( ApplicationDbContext db ) => this.db = db;

        public ExecutorServiceMdl.ExecutorProfile GetById( string id )
        {
            string langcode = LanguageController.CurrentCultureCode;
            var model = db.Executors.Find(id);
            List<ExecutorServiceMdl.ExecutorProfile.Review> reviews = new List<ExecutorServiceMdl.ExecutorProfile.Review>();
            var reviewies = db.CustomerOrders.Where(co => co.ExecutorId == id).ToList();
            foreach (var reviws in reviewies)
            {
                reviews.Add(new ExecutorServiceMdl.ExecutorProfile.Review
                {
                    ClientId = reviws.CustomerId,
                    ClientLastName = reviws.Customer.LastName,
                    ClientFirstName = reviws.Customer.FirstName,
                    ClientFatherName = reviws.Customer.FatherName == null ? " " : reviws.Customer.FatherName,
                    ReviewedAt = reviws.CustomerReviewedDateTime,
                    ReviewText = reviws.CustomerReview,
                    StarCountsOf5 = (int)reviws.EvaluationPerformerWork
                });
            }
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            var photosfileurl = db.ExecutorPhotoFiles.Where(epf => epf.ExecutorId == id).ToList();
            Image img;
            foreach (var pfu in photosfileurl)
            {
                img = Image.FromFile(pfu.PhotoFileUrl);
                string PhotoType = pfu.PhotoFileUrl.Substring(pfu.PhotoFileUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                photos.Add(Imagesbyte, PhotoType);
            }
            var specialization = (from e in db.Executors
                                  where e.Id == id
                                  from c in e.Categories
                                  from s in c.Specializations
                                  from sl in s.Langs
                                  where sl.Langcode == langcode
                                  select new ExecutorServiceMdl.ExecutorProfile.Specialization
                                  {
                                      CategoryName = c.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                                      CategoryId = c.Id,
                                      SpecializationId = s.Id,
                                      SpecializationName = sl.Name
                                  }).ToList();
            List<ExecutorServiceMdl.ExecutiveService> executiveServices = new List<ExecutorServiceMdl.ExecutiveService>();
            var executorservices = db.ExecutorServices.Where(es => es.ExecutorId == id).ToList();
            foreach (var executorservice in executorservices)
            {
                executiveServices.Add(new ExecutorServiceMdl.ExecutiveService
                {
                    Name = executorservice.Name,
                    CostType = (OrderCostType)executorservice.CostType,
                    FixedCost = executorservice.FixedCost,
                    FromCost = executorservice.FromCost,
                    ToCost = executorservice.ToCost
                });
            }

            return new ExecutorServiceMdl.ExecutorProfile
            {
                Id = model.Id,
                LastName = model.User.LastName,
                FirstName = model.User.FirstName,
                FatherName = model.User.FatherName == null ? " " : model.User.FatherName,
                Rating = (int)model.Rating,
                PhoneNumber = model.PhoneNumber,
                GenderId = (int)model.Gender,
                YouTubeUrl = model.YouTubeVideoUrl,
                RegionId = (int)model.CityId,
                Region = model.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                RegisteredAt = (DateTime)model.RegistrationDateTime,
                ClosedOrdersCount = (int)model.ExecutorClosedOrdersCount,
                Bonus = (int)model.Orders.Select(o => o.Bonus).FirstOrDefault(),
                BirthDay = (DateTime)model.BirthDay,
                ExecutorType = (ExecutorType)model.ExecutorType,
                Check = (bool)model.ExecutorCheck,
                Services = executiveServices,
                Specializations = specialization,
                Photos = photos,
                Reviews = reviews
            };
        }

        public ExecutorServiceMdl.ExecutorProfile GetByPhoneNumber( string phoneNumber )
        {
            string langcode = LanguageController.CurrentCultureCode;

            var model = db.Executors.Where(e => e.User.PhoneNumber == phoneNumber).FirstOrDefault();
            List<ExecutorServiceMdl.ExecutorProfile.Review> reviews = new List<ExecutorServiceMdl.ExecutorProfile.Review>();
            var reviewies = db.CustomerOrders.Where(co => co.ExecutorId == model.User.Id).ToList();
            foreach (var reviws in reviewies)
            {
                reviews.Add(new ExecutorServiceMdl.ExecutorProfile.Review
                {
                    ClientId = reviws.CustomerId,
                    ClientLastName = reviws.Customer.LastName,
                    ClientFirstName = reviws.Customer.FirstName,
                    ClientFatherName = reviws.Customer.FatherName == null ? " " : reviws.Customer.FatherName,
                    ReviewedAt = reviws.CustomerReviewedDateTime,
                    ReviewText = reviws.CustomerReview,
                    StarCountsOf5 = (int)reviws.EvaluationPerformerWork
                });
            }
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            var photosfileurl = db.ExecutorPhotoFiles.Where(epf => epf.ExecutorId == model.User.Id).ToList();
            Image img;
            foreach (var pfu in photosfileurl)
            {
                img = Image.FromFile(pfu.PhotoFileUrl);
                string PhotoType = pfu.PhotoFileUrl.Substring(pfu.PhotoFileUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                photos.Add(Imagesbyte, PhotoType);
            }
            var specialization = (from e in db.Executors
                                  where e.Id == model.User.Id
                                  from c in e.Categories
                                  from s in c.Specializations
                                  from sl in s.Langs
                                  where sl.Langcode == langcode
                                  select new ExecutorServiceMdl.ExecutorProfile.Specialization
                                  {
                                      CategoryName = c.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                                      CategoryId = c.Id,
                                      SpecializationId = s.Id,
                                      SpecializationName = sl.Name
                                  }).ToList();
            List<ExecutorServiceMdl.ExecutiveService> executiveServices = new List<ExecutorServiceMdl.ExecutiveService>();
            var executorservices = db.ExecutorServices.Where(es => es.ExecutorId == model.User.Id).ToList();
            foreach (var executorservice in executorservices)
            {
                executiveServices.Add(new ExecutorServiceMdl.ExecutiveService
                {
                    Name = executorservice.Name,
                    CostType = (OrderCostType)executorservice.CostType,
                    FixedCost = executorservice.FixedCost,
                    FromCost = executorservice.FromCost,
                    ToCost = executorservice.ToCost
                });
            }
            return new ExecutorServiceMdl.ExecutorProfile()
            {
                Id = model.Id,
                LastName = model.User.LastName,
                FirstName = model.User.FirstName,
                FatherName = model.User.FatherName == null ? " " : model.User.FatherName,
                Rating = (int)model.Rating,
                PhoneNumber = model.PhoneNumber,
                GenderId = (int)model.Gender,
                YouTubeUrl = model.YouTubeVideoUrl,
                RegionId = (int)model.CityId,
                Region = model.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                RegisteredAt = (DateTime)model.RegistrationDateTime,
                ClosedOrdersCount = (int)model.ExecutorClosedOrdersCount,
                Bonus = (int)model.Orders.Select(o => o.Bonus).FirstOrDefault(),
                BirthDay = (DateTime)model.BirthDay,
                ExecutorType = (ExecutorType)model.ExecutorType,
                Check = (bool)model.ExecutorCheck,
                Services = executiveServices,
                Specializations = specialization,
                Photos = photos,
                Reviews = reviews
            };
        }

        public string Register( ExecutorServiceMdl.ExecutorRegister item )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            List<ExecutorSpecialization> executorSpecializations = new List<ExecutorSpecialization>();
            var user = db.Users.Where(u => u.PhoneNumber == item.PhoneNumber).SingleOrDefault();
            for (int i = 0; i < item.SpecializationIds.Count(); i++)
            {
                executorSpecializations.Add(new ExecutorSpecialization
                {
                    ExecutorId = user.Id,
                    SpecializationId = item.SpecializationIds[i]
                });
            }
            Executor executor = new Executor()
            {
                Id = user.Id,
                BirthDay = item.BirthDay,
                PhoneNumber = item.PhoneNumber,
                ExecutorSpecializations = executorSpecializations,
                Gender = (Gender?)item.GenderId,
                ExecutorType = item.ExecutorType
            };
            db.Executors.Add(executor);
            db.SaveChanges();

            return executor.Id;
        }

        public void SendCheckingCodeForUpdatePhoneNumber( string newPhoneNumber )
        {
            string phonenumber = "+" + newPhoneNumber;
            var model = db.Executors.Where(e => e.User.PhoneNumber == phonenumber).FirstOrDefault();
            ManageController manager = new ManageController();
            AddPhoneNumberViewModel addPhone = new AddPhoneNumberViewModel()
            {
                Number = model.User.PhoneNumber
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
                to: new Twilio.Types.PhoneNumber(model.User.PhoneNumber)
            );
            PhoneCheckingCode phoneCheckingCode = new PhoneCheckingCode()
            {
                PhoneNumber = addPhone.Number,
                CheckingCode = code.ToString(),
                DateTime = DateTime.Now
            };
            db.phoneCheckingCodes.Add(phoneCheckingCode);
            db.SaveChanges();
        }

        public bool UpdatePhoneNumber( string executorId, string newPhoneNumber, string checkingCode )
        {
            throw new NotImplementedException();
        }

        public void UpdatePhotoFiles( Dictionary<byte[], string> actualPhotoFiles )
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile( ExecutorServiceMdl.ExecutorProfileEdit item )
        {
            throw new NotImplementedException();
        }

        public void UpdateServices( List<ExecutorServiceMdl.ExecutiveService> actualServices )
        {
            throw new NotImplementedException();
        }

        public void UpdateType( ExecutorServiceMdl.ExecutorTypeEdit item )
        {
            throw new NotImplementedException();
        }
    }
}