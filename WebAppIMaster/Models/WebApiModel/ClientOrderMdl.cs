using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;
using WebAppIMaster.Models.Enums;

namespace WebAppIMaster.Models.WebApiModel
{
    public class ClientOrderCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public OrderStartDateType StartDateType { get; set; }
        public DateTime StartedDate { get; set; }
        public decimal Cost { get; set; }
        public OrderCostType CostType { get; set; }
        public int RegionId { get; set; }
        public bool ReceiveOnlyResponses { get; set; }
        public int CategoryId { get; set; }
        public bool PayWithBounce { get; set; }
        public string Address { get; set; }
        public string CustomerId { get; set; }
        public Dictionary<byte[], string> Photos { get; set; }
    }
    public class ClientOrderDetailsView
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public OrderStartDateType OrderStartDateType { get; set; }
        public DateTime StartedDate { get; set; }
        public OrderCostType OrderCostType { get; set; }
        public int Cost { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public OrderStatus OrderType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ReceiveOnlyResponses { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool PayWithBounce { get; set; }
        public string Address { get; set; }
        public Dictionary<byte[], string> Photos { get; set; }

        /* Executor */
        public string ExecutorId { get; set; }
        public string ExecutorLastname { get; set; }
        public string ExecutorFirstname { get; set; }
        public string ExecutorFathername { get; set; }
        public bool ExecotorOnline { get; set; }
        public string ExecotorRating { get; set; }
        public byte[] ExecutorAvatarFile { get; set; }
        public string ExecotorAvatarFileType { get; set; }
        public bool ExecutorCheck { get; set; }
        public DateTime ExecutorRegisterDate { get; set; }
        public int ExecutorClosedOrdersCount { get; set; }
        public int ExecutorRegionId { get; set; }
        public string ExecutorRegionName { get; set; }
        public int ExecutorGenderId { get; set; }
        public string ExecutorGenderName { get; set; }
        public DateTime ExecoturBirthday { get; set; }
    }
    public class ClientOrderItemView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string StartingDatetime { get; set; }
        public int Cost { get; set; }
        public OrderStatus Type { get; set; }
        public int NewNotifications { get; set; }

    }

    public class ClientCommonOrderDetailsView
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
        public OrderStartDateType OrderStartDateType { get; set; }
        public DateTime StartedDate { get; set; }
        public OrderCostType OrderCostType { get; set; }
        public int Cost { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public int ViewCount { get; set; }
        public OrderStatus OrderType { get; set; }
        public DateTime CreateAt { get; set; }

        public class Photo
        {
            public byte[] File { get; set; }
            public string Type { get; set; }
        }
    }
}