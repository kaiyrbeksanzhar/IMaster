using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class ReviewModel
    {
        public class ReviewSelect
        {
            public int Id { get; set; }
            public int Rating { get; set; }
            public string ReviewText { get; set; }

            public string CustomerId { get; set; }
            public string Customer { get; set; }

            public string ExecutorId { get; set; }
            public string Executor { get; set; }
        }

        public class ReviewDetail
        {
            public int Id { get; set; }
            public int Rating { get; set; }
            public string ReviewText { get; set; }

            public string CustomerId { get; set; }
            public string Customer { get; set; }

            public string ExecutorId { get; set; }
            public string Executor { get; set; }
        }
    }
}