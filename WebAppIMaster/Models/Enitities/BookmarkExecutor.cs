using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIMaster.Models
{
    public class BookmarkExecutor
    {
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Key, Column(Order = 2)]
        public string ExecutorId { get; set; }
        public virtual Executor Executor { get; set; }
    }
}