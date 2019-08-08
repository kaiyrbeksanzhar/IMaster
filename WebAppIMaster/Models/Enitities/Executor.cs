using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Models
{
    public class Executor
    {
        public Executor()
        {
            Services = new HashSet<ExecutorService>();
            Categories = new HashSet<Category>();
            ExecutorPhotoFiles = new HashSet<ExecutorPhotoFiles>();
            ExecutorPasswordFiles = new HashSet<ExecutorPassportFiles>();
            specializations = new HashSet<Specialization>();
            Responses = new HashSet<Response>();
            CallToClients = new HashSet<CallToClient>();
            BookmarkExecutors = new HashSet<BookmarkExecutor>();
            executorWorkPhotos = new HashSet<ExecutorWorkPhoto>();
        }

        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? BirthDay { get; set; }
        public Gender? Gender { get; set; }
        public bool? ExecutorCheck { get; set; }
        public string PhoneNumber { get; set; }
        public ExecutorType? ExecutorType { get; set; }
        public ExecutorStatus? ExecutorStatus { get; set; }
        public int? Rating { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? RegistrationDateTime { get; set; }
        public string YouTubeVideoUrl { get; set; }
        public bool? Banned { get; set; }
        public DateTime? BannedDateTime { get; set; }
        public string OrganizationName { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }

        public bool? ExecotorOnline { get; set; }

        public int? ExecutorClosedOrdersCount { get; set; }

        public ICollection<ExecutorService> Services { get; set; }
        public ICollection<ExecutorPhotoFiles> ExecutorPhotoFiles { get; set; }
        public ICollection<ExecutorPassportFiles> ExecutorPasswordFiles { get; set; }
        public ICollection<CustomerOrder> Orders { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Specialization> specializations { get; set; }
        public ICollection<ExecutorSpecialization>  ExecutorSpecializations { get; set; }
        public ICollection<Response> Responses { get; set; }
        public ICollection<CallToClient> CallToClients { get; set; }
        public ICollection<BookmarkExecutor> BookmarkExecutors { get; set; }
        public ICollection<ExecutorWorkPhoto>  executorWorkPhotos { get; set; }
        
    }
}
