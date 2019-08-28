using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class ResponseModels
    {
        public class ResponseList
        {
            public string ExecutorId { get; set; }
            public string ExecutorName { get; set; }

            public int OrderId { get; set; }
            public string OrderUserName { get; set; }

            public DateTime? CreatedAt_Date { get; set; }
            public string ResponseComment { get; set; }
        }

        public class ResponseSelect
        {
            public string ExecutorId { get; set; }
            public int OrderId { get; set; }

            //---------- Customer
            public string CustomerName { get; set; }
            public string AvatarUrl { get; set; }
            public string OrderName { get; set; }
            public string SpecilizationName { get; set; }
            public int Price { get; set; }
            public DateTime StartTime { get; set; }
            public string State { get; set; }

            //----------- Executor
            public string ExecutorUrl { get; set; }
            public string ExecutorName { get; set; }
            public int Rating { get; set; }
            public DateTime WorkTime { get; set; }
            public bool DocState { get; set; }
        }
    }
}