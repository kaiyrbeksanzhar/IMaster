using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string ExecutorId { get; set; }
        public Executor Executor { get; set; }
    }
}