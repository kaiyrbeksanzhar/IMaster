using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public List<ExecutorServiceMdl.ExecutorItem> GetItemList()
        {
            string langcode = LanguageController.CurrentCultureCode;
            List<ExecutorServiceMdl.ExecutorItem> executorItems = new List<ExecutorServiceMdl.ExecutorItem>();

            var executors = db.Executors.ToList();
            foreach (var item in executors)
            {
                executorItems.Add(new ExecutorServiceMdl.ExecutorItem
                {
                    Id = item.Id,
                    LastName = item.User.LastName,
                    FirstName = item.User.FirstName,
                    FatherName = item.User.FatherName,
                    Rating = (int)item.Rating,
                    Check = (bool)item.ExecutorCheck,
                    RegisteredAt = (DateTime)item.RegistrationDateTime,
                    ClosedOrdersCount = (int)item.ExecutorClosedOrdersCount,
                    AvatarFile = System.IO.File.ReadAllBytes(item.AvatarUrl),
                    ExecutorType = (ExecutorType)item.ExecutorType,
                    AvatarFileType = item.AvatarUrl.Substring(item.AvatarUrl.LastIndexOf(".") + 1),
                });
            }

            return executorItems;
        }

        public List<ExecutorServiceMdl.ExecutorItem> GetItemListForSpecialization( int specializationId )
        {
            string langcode = LanguageController.CurrentCultureCode;

            List<ExecutorServiceMdl.ExecutorItem> executorItems = new List<ExecutorServiceMdl.ExecutorItem>();

            var executors = db.ExecutorSpecializations.Where(es => es.SpecializationId == specializationId).ToList();
            foreach (var item in executors)
            {
                executorItems.Add(new ExecutorServiceMdl.ExecutorItem
                {
                    Id = item.ExecutorId,
                    LastName = item.Executor.User.LastName,
                    FirstName = item.Executor.User.FirstName,
                    FatherName = item.Executor.User.FatherName,
                    AvatarFile = System.IO.File.ReadAllBytes(item.Executor.AvatarUrl),
                    AvatarFileType = item.Executor.AvatarUrl.Substring(item.Executor.AvatarUrl.LastIndexOf(".") + 1),
                    ExecutorType = (ExecutorType)item.Executor.ExecutorType,
                    Check = (bool)item.Executor.ExecutorCheck,
                    ClosedOrdersCount = (int)item.Executor.ExecutorClosedOrdersCount,
                    Rating = (int)item.Executor.Rating,
                    RegisteredAt = (DateTime)item.Executor.RegistrationDateTime,
                });
            }
            return executorItems;
        }

        public List<ExecutorServiceMdl.ExecutorItem> GetItemListSuitableForOrder( int orderId )
        {
            string langcode = LanguageController.CurrentCultureCode;

            List<ExecutorServiceMdl.ExecutorItem> executorItems = new List<ExecutorServiceMdl.ExecutorItem>();

            var executors = db.CustomerOrders.Where(o => o.Id == orderId).ToList();

            foreach (var item in executors)
            {
                executorItems.Add(new ExecutorServiceMdl.ExecutorItem
                {
                    Id = item.ExecutorId,
                    LastName = item.Executor.User.LastName,
                    FirstName = item.Executor.User.FirstName,
                    FatherName = item.Executor.User.FatherName,
                    AvatarFile = System.IO.File.ReadAllBytes(item.Executor.AvatarUrl),
                    AvatarFileType = item.Executor.AvatarUrl.Substring(item.Executor.AvatarUrl.LastIndexOf(".") + 1),
                    ExecutorType = (ExecutorType)item.Executor.ExecutorType,
                    Check = (bool)item.Executor.ExecutorCheck,
                    ClosedOrdersCount = (int)item.Executor.ExecutorClosedOrdersCount,
                    Rating = (int)item.Executor.Rating,
                    RegisteredAt = (DateTime)item.Executor.RegistrationDateTime,
                });
            }
            return executorItems;
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
            try
            {
                string langcode = LanguageController.CurrentCultureCode;

                var user = (from e in db.Users.Select(u => u.Executor)
                            where e.Id == executorId

                            select e).SingleOrDefault();

                string phoneNumber = System.Text.RegularExpressions.Regex.Replace(user.PhoneNumber, @"\s+", "");
                string phonenumber = user.PhoneNumber.Substring(user.PhoneNumber.Length - 10, 10);
                var Phonecheking = db.phoneCheckingCodes.Where(pcc => pcc.PhoneNumber.Contains(phonenumber)).FirstOrDefault();

                newPhoneNumber = newPhoneNumber.Replace(" ", "");
                newPhoneNumber = newPhoneNumber.Replace("+7", "8");

                var executor = db.Executors.Find(executorId);
                executor.PhoneNumber = newPhoneNumber;
                db.Entry(executor).State = System.Data.Entity.EntityState.Modified;

                user.PhoneNumber = newPhoneNumber;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                Phonecheking.PhoneNumber = newPhoneNumber;
                Phonecheking.CheckingCode = checkingCode;
                db.Entry(Phonecheking).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void UpdatePhotoFiles( string executorId, Dictionary<byte[], string> actualPhotoFiles )
        {

            List<string> photos = new List<string>();
            var item = (from epf in db.ExecutorPhotoFiles
                        where epf.ExecutorId == executorId

                        select epf).ToList();
            foreach (var aPhotoFile in actualPhotoFiles)
            {
                MemoryStream ms = new MemoryStream(aPhotoFile.Key);
                Image img = Image.FromStream(ms);
                string url = FileManager.SavePhoto(img);
                photos.Add(url);
            }
            int i = 0;
            foreach (var executorphoto in item)
            {
                executorphoto.PhotoFileUrl = photos[i];
                db.Entry(executorphoto).State = System.Data.Entity.EntityState.Modified;
                i++;
            }
            db.SaveChanges();
        }

        public void UpdateProfile( ExecutorServiceMdl.ExecutorProfileEdit item )
        {
            string langcode = LanguageController.CurrentCultureCode;

            var hidder = (from e in db.Executors
                          where e.Id == item.Id
                          select e).SingleOrDefault();

            hidder.User.LastName = item.LastName;
            hidder.User.FirstName = item.FirstName;
            hidder.User.FatherName = item.FatherName;
            hidder.BirthDay = item.BirthDay;
            hidder.CityId = item.RegionId;

            db.Entry(hidder).State = System.Data.Entity.EntityState.Modified;

            var user = (from u in db.Users
                        where u.Id == item.Id
                        select u).SingleOrDefault();
            user.LastName = item.LastName;
            user.FatherName = item.FatherName;
            user.FirstName = item.FirstName;

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateServices( List<ExecutorServiceMdl.ExecutiveService> actualServices, string executorId )
        {
            List<Models.ExecutorService> executiveServices = new List<Models.ExecutorService>();
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            var item = (from es in db.ExecutorServices
                        where es.ExecutorId == executorId
                        select new ExecutorServiceMdl.ExecutiveService
                        {
                            Name = es.Name,
                            CostType = (OrderCostType)es.CostType,
                            FixedCost = es.FixedCost,
                            FromCost = es.FromCost,
                            ToCost = es.ToCost
                        }).ToList();

            foreach (var executorservice in item)
            {
                executiveServices.Add(new Models.ExecutorService
                {
                    CostType = (int)executorservice.CostType,
                    FixedCost = executorservice.FixedCost,
                    FromCost = executorservice.FromCost,
                    Name = executorservice.Name,
                    ToCost = executorservice.ToCost
                });
            }
            db.Entry(executiveServices).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateType( ExecutorServiceMdl.ExecutorTypeEdit item, string executorId )
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();


            var executor = db.Executors.Where(e => e.Id == executorId).SingleOrDefault();
            executor.ExecutorType = item.ExecutorType;
            foreach (var executorSpecialty in item.SpecializationIds)
            {
                executor.specializations.Add(new Specialization
                {
                    Id = executorSpecialty,
                });
            }
            db.Entry(executor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}