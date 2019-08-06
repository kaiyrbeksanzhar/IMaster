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
using static WebAppIMaster.Models.WebApiModel.ExecutorServiceMdl;
using static WebAppWebAppIMaster.SmsService;

namespace WebAppIMaster.Models.WebApiService
{
    public class ExecutorService : IExecutorService
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ExecutorService(ApplicationDbContext db) => this.db = db;

        public ExecutorServiceMdl.ExecutorProfile GetById(string id)
        {
            string langcode = LanguageController.CurrentCultureCode;
            //var model = db.Executors.Find(id);
            var model = (from e in db.Executors
                         where e.Id == id

                         select new
                         {
                             Id = e.Id,
                             LastName = e.User.LastName,
                             FirstName = e.User.FirstName,
                             FatherName = e.User.FatherName == null ? " " : e.User.FatherName,
                             Rating = e.Rating,
                             PhoneNumber = e.PhoneNumber,
                             GenderId = e.Gender == Gender.Male ? 1 : 2,
                             YouTubeUrl = e.YouTubeVideoUrl,
                             RegionId = e.CityId,
                             Region = e.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                             RegisteredAt = e.RegistrationDateTime == null ? DateTime.MinValue : e.RegistrationDateTime,
                             ClosedOrdersCount = e.ExecutorClosedOrdersCount,
                             Bonus = (int)e.Orders.Select(o => o.Bonus).FirstOrDefault(),
                             ExecutorType = e.ExecutorType,
                             Check = e.ExecutorCheck,
                             BirthDay = e.BirthDay,
                         }).SingleOrDefault();
            if (model == null) return null;
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
            List<string> PhotoUrls = new List<string>();
            Dictionary<byte[], string> photos = new Dictionary<byte[], string>();
            var photosfileurl = db.ExecutorPhotoFiles.Where(epf => epf.ExecutorId == id).ToList();
            Image img;
            if (photosfileurl.Count > 0)
            {
                foreach (var pfu in photosfileurl)
                {
                    img = Image.FromFile(pfu.PhotoFileUrl);
                    string PhotoType = pfu.PhotoFileUrl.Substring(pfu.PhotoFileUrl.LastIndexOf(".") + 1);
                    byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                    string ImagesUrl = "http://i-master.kz/api/GetExecutorPhoto?url=" + pfu.PhotoFileUrl;
                    PhotoUrls.Add(pfu.PhotoFileUrl == null ? null : "http://i-master.kz/api/GetExecutorPhoto?url=" + pfu.PhotoFileUrl);
                    //photos.Add(Imagesbyte, PhotoType);
                    photos.Add(Imagesbyte, ImagesUrl);
                }
            }
            else
            {
                photos = null;
            }
            var specialization = (from es in db.ExecutorSpecializations
                                  where es.ExecutorId == id
                                  select new ExecutorServiceMdl.ExecutorProfile.Specialization
                                  {
                                      CategoryName = es.Specialization.Category.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                                      CategoryId = es.Specialization.Category.Id,
                                      SpecializationId = es.SpecializationId,
                                      SpecializationName = es.Specialization.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
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
                LastName = model.LastName,
                FirstName = model.FirstName,
                FatherName = model.FatherName,
                Rating = model.Rating,
                PhoneNumber = model.PhoneNumber,
                GenderId = model.GenderId,
                YouTubeUrl = model.YouTubeUrl,
                RegionId = model.RegionId,
                Region = model.Region,
                RegisteredAt = model.RegisteredAt == null ? DateTime.MinValue : model.RegisteredAt,
                ClosedOrdersCount = model.ClosedOrdersCount,
                Bonus = model.Bonus,
                BirthDay = model.BirthDay,
                ExecutorType = model.ExecutorType,
                Check = model.Check,
                Services = executiveServices,
                Specializations = specialization,
                Photos = photos,
                Reviews = reviews,
                PhotosUrls = PhotoUrls
            };
        }

        public ExecutorServiceMdl.ExecutorProfile GetByPhoneNumber(string phoneNumber)
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
            List<string> PhotoUris = new List<string>();
            var photosfileurl = db.ExecutorPhotoFiles.Where(epf => epf.ExecutorId == model.User.Id).ToList();
            Image img;
            foreach (var pfu in photosfileurl)
            {
                img = Image.FromFile(pfu.PhotoFileUrl);
                string PhotoType = pfu.PhotoFileUrl.Substring(pfu.PhotoFileUrl.LastIndexOf(".") + 1);
                byte[] Imagesbyte = FileManager.ImageToByteArray(img);
                photos.Add(Imagesbyte, PhotoType);
                PhotoUris.Add(pfu.PhotoFileUrl == null ? null : "http://i-master.kz/api/GetExecutorPhoto?url=" + pfu.PhotoFileUrl);
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
                Reviews = reviews,
                PhotosUrls = PhotoUris,
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
                    ExecutorType = (ExecutorType)item.ExecutorType,
                    AvatarUri = item.AvatarUrl == null ? null : "http://i-master.kz/api/GetExecutorPhoto?url=" + item.AvatarUrl,
                });
            }

            return executorItems;
        }

        public List<ExecutorServiceMdl.ExecutorItem> GetItemListForSpecialization(int specializationId)
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
                    AvatarUri = item.Executor.AvatarUrl == null ? null : "http://i-master.kz/api/GetExecutorPhoto?url=" + item.Executor.AvatarUrl,
                    ExecutorType = (ExecutorType)item.Executor.ExecutorType,
                    Check = (bool)item.Executor.ExecutorCheck,
                    ClosedOrdersCount = (int)item.Executor.ExecutorClosedOrdersCount,
                    Rating = (int)item.Executor.Rating,
                    RegisteredAt = (DateTime)item.Executor.RegistrationDateTime,
                });
            }
            return executorItems;
        }

        public List<ExecutorServiceMdl.ExecutorItem> GetItemListSuitableForOrder(int orderId)
        {
            string langcode = LanguageController.CurrentCultureCode;

            List<ExecutorServiceMdl.ExecutorItem> executorItems = new List<ExecutorServiceMdl.ExecutorItem>();

            var executors = (from o in db.CustomerOrders
                             where o.Id == orderId
                             from e in db.Executors
                             where e.specializations.Any(c => c.Id == o.SpecializationId)
                             orderby e.Rating descending
                             select e).ToList();
            if (executors != null)
            {
                foreach (var item in executors)
                {
                    executorItems.Add(new ExecutorServiceMdl.ExecutorItem
                    {
                        Id = item.Id,
                        FirstName = item.User.FirstName,
                        FatherName = item.User.FatherName,
                        AvatarUri = item.AvatarUrl == null ? null : "http://i-master.kz/api/GetExecutorPhoto?url=" + item.AvatarUrl,
                        ExecutorType = (ExecutorType)item.ExecutorType,
                        Check = (bool)item.ExecutorCheck,
                        ClosedOrdersCount = (int)item.ExecutorClosedOrdersCount,
                        Rating = (int)item.Rating,
                        RegisteredAt = (DateTime)item.RegistrationDateTime,
                    });
                }
            }
            else
            {
                executorItems = null;
            }

            return executorItems;
        }

        public string Register(ExecutorServiceMdl.ExecutorRegister item)
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();
            List<ExecutorSpecialization> executorSpecializations = new List<ExecutorSpecialization>();

            string Phonenumber = item.PhoneNumber.Replace(" ", "");
            string PhonenumberDate = Phonenumber.Replace("+7", "8");
            Phonenumber = PhonenumberDate.Substring(PhonenumberDate.Length - 10, 10);
            var user = db.Users.Where(u => u.PhoneNumber.Contains(Phonenumber)).SingleOrDefault();
            for (int i = 0; i < item.SpecializationIds.Count(); i++)
            {
                executorSpecializations.Add(new ExecutorSpecialization
                {
                    ExecutorId = user.Id,
                    SpecializationId = item.SpecializationIds[i]
                });
            }
            var closedOrder = db.CancelOrders.Where(co => co.ExecutorId == user.Id).Count();
            Executor executor = new Executor()
            {
                Id = user.Id,
                BirthDay = (DateTime?)item.BirthDay,
                PhoneNumber = PhonenumberDate,
                ExecutorSpecializations = executorSpecializations,
                Gender = (Gender?)item.GenderId,
                ExecutorType = (ExecutorType?)item.ExecutorType,
                RegistrationDateTime = DateTime.Now,
                ExecutorCheck = true,
                ExecotorOnline = true,
                Rating = 50,
                ExecutorClosedOrdersCount = (int?)closedOrder,
                ExecutorStatus = (ExecutorStatus?)ExecutorStatus.Newbie,
                CityId = (int?)user.RegionId == 0 ? 1 : user.RegionId,
                //BannedDateTime = (DateTime?)DateTime.MinValue,
                Banned = false,
            };
            db.Executors.Add(executor);
            db.SaveChanges();

            return executor.Id;
        }

        public void SendCheckingCodeForUpdatePhoneNumber(string newPhoneNumber)
        {
            string sendPhoneNumber = newPhoneNumber;
            string sendphoneNumber = sendPhoneNumber.Replace("+", "");
            string newPhonenumber = newPhoneNumber.Replace(" ", "");
            newPhonenumber = newPhonenumber.Replace("+7", "8");

            newPhonenumber = System.Text.RegularExpressions.Regex.Replace(newPhonenumber, @"\s+", "");
            string phonenumber = newPhoneNumber.Substring(newPhonenumber.Length - 10, 10);

            var model = db.Executors.Where(e => e.User.PhoneNumber == newPhonenumber).FirstOrDefault();
            ManageController manager = new ManageController();
            AddPhoneNumberViewModel addPhone = new AddPhoneNumberViewModel()
            {
                Number = model.User.PhoneNumber
            };
            //string code = manager.AddPhoneNumber1(addPhone, model.User.Id);
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            //const string accountSid = "AC447e8467b7de404485f857a3495acfbf";
            //const string authToken = "59a8ffe9a848c07bfe598a99c43abfd2";

            //Random random = new Random();
            //int code = random.Next(100000, 999999);
            //TwilioClient.Init(accountSid, authToken);
            //var message = MessageResource.Create(
            //    body: "Ваше код безопасности:" + code,
            //    from: new Twilio.Types.PhoneNumber("+14193860152"),
            //    to: new Twilio.Types.PhoneNumber(model.User.PhoneNumber)
            //);
            int code = 4444;
            var cheking = db.phoneCheckingCodes.Where(p => p.PhoneNumber == newPhonenumber).FirstOrDefault();
            PhoneCheckingCode phoneCheckingCode = null;
            if (cheking != null)
            {
                phoneCheckingCode = new PhoneCheckingCode()
                {
                    PhoneNumber = newPhonenumber,
                    CheckingCode = code.ToString(),
                    DateTime = DateTime.Now
                };
                db.phoneCheckingCodes.Add(phoneCheckingCode);
            }
            else
            {
                cheking.CheckingCode = code.ToString();
                cheking.DateTime = DateTime.Now;
                db.Entry(cheking).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
        }

        public bool UpdatePhoneNumber(string executorId, string newPhoneNumber, string checkingCode)
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

        public void UpdatePhotoFiles(string executorId, Dictionary<byte[], string> actualPhotoFiles)
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



        public void UpdateProfile(ExecutorServiceMdl.ExecutorProfileEdit item)
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

        public void UpdateServices(List<ExecutorServiceMdl.ExecutiveService> actualServices, string executorId)
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

        public void UpdateType(ExecutorServiceMdl.ExecutorTypeEdit item, string executorId)
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();


            var executor = db.Executors.Where(e => e.Id == executorId).SingleOrDefault();
            executor.ExecutorType = item.ExecutorType;
            List<ExecutorSpecialization> model = db.ExecutorSpecializations.Where(e => e.ExecutorId == executorId).ToList();
            int itemSpecialtyLenght = item.SpecializationIds.Count();
            for (int i = 0; i < itemSpecialtyLenght; i++)
            {
                model[i].SpecializationId = item.SpecializationIds[i];
            }
            db.Entry(executor).State = System.Data.Entity.EntityState.Modified;
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void SendExecutorAvatar(string url, string executorId)
        {
            var Executor = db.Executors.Where(n => n.Id == executorId).FirstOrDefault();
            if (Executor != null)
            {
                if (Executor.AvatarUrl == null)
                {
                    Executor.AvatarUrl = url;
                }
            }
            db.Entry(Executor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void SendExecutorPhotos(string url, string executorId)
        {
            var Executor = db.Executors.Where(n => n.Id == executorId).FirstOrDefault();
            if (Executor != null)
            {
                if (Executor.AvatarUrl == null)
                {
                    Executor.AvatarUrl = url;
                }
            }
            db.Entry(Executor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void SendExecutroPassportFile(string url, string executorId)
        {
            ExecutorPassportFiles files = new ExecutorPassportFiles()
            {
                ExecutorId = executorId,
                ImageUrl = url,
                Status = Status.Active,
            };
            db.ExecutorPassportFiles.Add(files);
            db.SaveChanges();
        }

        public ExecutorPassportFile GetExecutorPassportFile(string executorId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var model = db.ExecutorPassportFiles.Where(epf => epf.ExecutorId == executorId).FirstOrDefault();
            return new ExecutorPassportFile
            {
                Id = model.Id,
                ExecutorId = executorId,
                Url = model.ImageUrl == null ? null : "http://i-master.kz/api/GetExecutorPhoto?url=" + model.ImageUrl,
                Status = model.Status,
            };
        }

        public void deletePassportFile(string url)
        {
            var model = db.ExecutorPassportFiles.Where(epf => epf.ImageUrl == url).FirstOrDefault();
            db.ExecutorPassportFiles.Remove(model);
            db.SaveChanges();
        }
    }
}