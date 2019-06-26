using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIMaster.Models
{
    public class BookmarkOrder
    {
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Key, Column(Order = 2)]
        public int OrderId { get; set; }
        public virtual CustomerOrder Order { get; set; }
    }
}