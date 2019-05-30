using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ExecutorMdl
    {
        public class ExecutorRegister
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public DateTime BirthDay { get; set; }
            public int GenderId { get; set; }
            public ExecutorType ExecutorType { get; set; }
            public List<int> SpecializationIds { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class ExecutorProfileEdit
        {
            public string Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public DateTime BirthDay { get; set; }
            public int RegionId { get; set; }
        }


        public class ExecutorProfile
        {
            public string Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public DateTime BirthDay { get; set; }
            public int GenderId { get; set; }
            public string GenderName { get; set; }
            public int Rating { get; set; }
            public bool Check { get; set; }
            public DateTime RegisteredAt { get; set; }
            public int ClosedOrdersCount { get; set; }
            public int RegionId { get; set; }
            public string Region { get; set; }
            public int Bonus { get; set; }
            public string PhoneNumber { get; set; }

            public List<Specialization> Specializations { get; set; }
            public ExecutorType ExecutorType { get; set; }
            public Dictionary<byte[], String> Photos { get; set; }
            public List<ExecutiveService> Services { get; set; }
            public string YouTubeUrl { get; set; }
            public List<Review> Reviews { get; set; }

            public class Specialization
            {
                public int SpecializationId { get; set; }
                public string SpecializationName { get; set; }
                public int CategoryId { get; set; }
                public string CategoryName { get; set; }
            }

            public class Review
            {
                public string ClientId { get; set; }
                public string ClientLastName { get; set; }
                public string ClientFirstName { get; set; }
                public string ClientFatherName { get; set; }
                public int StarCountsOf5 { get; set; }
                public DateTime ReviewedAt { get; set; }
                public string ReviewText { get; set; }
            }
        }
        public class ExecutiveService
        {
            public string Name { get; set; }
            public OrderCostType CostType { get; set; }
            public int FromCost { get; set; }
            public int ToCost { get; set; }
            public int FixedCost { get; set; }
        }

        public class ExecutorTypeEdit
        {
            public ExecutorType ExecutorType { get; set; }
            public List<int> SpecializationIds { get; set; }
        }
    }
}