using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ClientExecutorServiceMdl
    {
        public class ExecutorItem
        {
            public string Id { get; set; }
            public string Lastname { get; set; }
            public string Firstname { get; set; }
            public string Fathername { get; set; }
            public string PhoneNumber { get; set; }
            public bool Online { get; set; }
            public string Rating { get; set; }
            public string AvatarUri { get; set; }
            public bool Check { get; set; }
            public DateTime RegisterDate { get; set; }
            public int ClosedOrdersCount { get; set; }
        }

        public class ExecutorDetails
        {
            public string Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public bool? Online { get; set; }
            public string Rating { get; set; }
            public string AvatarUri { get; set; }
            public bool? Check { get; set; }
            public DateTime? RegisterDate { get; set; }
            public int? ClosedOrdersCount { get; set; }
            public int? RegionId { get; set; }
            public string RegionName { get; set; }
            public int GenderId { get; set; }
            public string GenderName { get; set; }
            public DateTime? Birthday { get; set; }

            public int? CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int? SpecializationId { get; set; }
            public string SpecializationName { get; set; }
            public ExecutorType? ExecutorType { get; set; }
            public string PhoneNumber { get; set; }
            public IEnumerable<string> PhotoUris { get; set; }
            public Dictionary<string, int> Services { get; set; }
            public List<ExecutorReview> Reviews { get; set; }
            public string YouTubeVideoUrl { get; set; }
        }

        public class ExecutorReview
        {
            public string ClientAvatarUri { get; set; }
            public int StartCountOf5 { get; set; }
            public DateTime ReviewedAt { get; set; }
            public string ReviewText { get; set; }
        }

        public class ExecutorResponse
        {
            public string ExecutorId { get; set; }
            public string Lastname { get; set; }
            public string Firstname { get; set; }
            public string Fathername { get; set; }
            public string PhoneNumber { get; set; }
            public bool Online { get; set; }
            public string Rating { get; set; }
            public string AvatarUri { get; set; }
            public bool Check { get; set; }
            public DateTime RegisterDate { get; set; }
            public int ClosedOrdersCount { get; set; }
            public int OrderId { get; set; }
            public DateTime CreateAt { get; set; }
            public string ExecutorMessage { get; set; }
        }
    }
}