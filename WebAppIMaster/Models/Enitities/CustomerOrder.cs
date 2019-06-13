using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.Enitities.Enums;

namespace WebAppIMaster.Models
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public string Photo1Url { get; set; }
        public string Photo2Url { get; set; }
        public string Photo3Url { get; set; }
        public string Photo4Url { get; set; }
        public OrderStartDateType StartDateType { get; set; }
        public DateTime StartedDate { get; set; }
        public OrderCostType CostType { get; set; }
        public decimal Cost { get; set; }
        public OrderState OrderState { get; set; }
        public int NewNotifications { get; set; }
        public OrderType OrderType { get; set; }

        public int InCityId { get; set; }
        public virtual City InCity { get; set; }

        public int AddressId { get; set; }
        public virtual Address  Address { get; set; }

        public bool ReceiveOnlyResponses { get; set; }

        public bool PayWithBounce { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category{ get; set; }

        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }



        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }


        public DateTime EndedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal Bonus { get; set; }
        public int ViewCount { get; set; }
        public string CustomerReview { get; set; }
        public DateTime CustomerReviewedDateTime { get; set; }
        public string ExecutorComment { get; set; }
        public DateTime ExecutorCommentedDateTime { get; set; }
        public EvaluationPerformerWork EvaluationPerformerWork { get; set; }
    }
}
