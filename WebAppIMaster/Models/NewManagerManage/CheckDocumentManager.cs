using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class CheckDocumentManager
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public CheckDocumentManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public List<CheckDocumentModel.CheckDocumentList> DocumentList()
        {

            var result = (from b in db.ExecutorPassportFiles
                          select new CheckDocumentModel.CheckDocumentList
                          {
                              Id = b.Id,
                              ExecutorName = b.Executor.User.FirstName + " " + b.Executor.User.LastName,
                              ExecutorCity = db.CityLangs.Where(p => p.CityId == b.Executor.CityId).FirstOrDefault().Name
                          }).ToList();
            return result;
        }

        public CheckDocumentModel.DocumentDetail DocumentDetails(int Id)
        {
            var result = (from b in db.ExecutorPassportFiles
                          where b.Id == Id
                          select new CheckDocumentModel.DocumentDetail
                          {
                              DocId = b.Id,
                              UserName = b.Executor.User.FirstName + " " + b.Executor.User.LastName,
                              UserId = b.Executor.User.Id,
                              Gender = b.Executor.Gender,
                              PhoneNumber = b.Executor.PhoneNumber,
                              Rating = b.Executor.Rating,
                              AvatarUrl = b.Executor.AvatarUrl,
                              RegistrationDateTime = b.Executor.RegistrationDateTime,
                              DocumentState = b.Status,
                              City = db.CityLangs.Where(p => p.CityId == b.Executor.CityId).FirstOrDefault().Name,
                              DocUrl = b.ImageUrl
                          }).FirstOrDefault();
            return result;
        }
    }

    public class CheckDocumentModel
    {
        public class CheckDocumentList
        {
            public int Id { get; set; }
            public string ExecutorName { get; set; }
            public string ExecutorCity { get; set; }
            public DateTime? GetTime { get; set; }
            public int? State { get; set; }

        }

        public class DocumentDetail
        {
                public string UserId { get; set; }
                public int DocId { get; set; }
                public string UserName { get; set; }
                public DateTime? BirthDay { get; set; }
                public Gender? Gender { get; set; }
                public string PhoneNumber { get; set; }
                public int? Rating { get; set; }
                public string AvatarUrl { get; set; }
                public DateTime? RegistrationDateTime { get; set; }
                public string SpecialityName { get; set; }

                public Status DocumentState { get; set; }
                public string City { get; set; }
                public string DocUrl { get; set; }
        }
    }
}