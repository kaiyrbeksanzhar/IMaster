using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAppIMaster.Models.Enitities
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("WebAppIMaster", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressLang> AddressLangs { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityLang> CityLangs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Executor> Executors { get; set; }
        public DbSet<ExecutorSpecialization> ExecutorSpecializations { get; set; }
        public DbSet<ExecutorService> ExecutorServices { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsLang> NewsLangs { get; set; }
        public DbSet<OrganizationPromotionAndDiscount> OrganizationPromotionAndDiscounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceLang> ServiceLangs { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }
        public DbSet<UnitMeasurementLang> UnitMeasurementLangs { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLang> CategoryLangs { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<SpecializationLang> SpecializationLangs { get; set; }
        public DbSet<ExecutorPassportFiles> ExecutorPassportFiles { get; set; }
        public DbSet<ExecutorPhotoFiles> ExecutorPhotoFiles { get; set; }
        public DbSet<HowDidYouAboutUs> HowDidYouAboutUes { get; set; }
        public DbSet<HowDidYouAboutUsLang> HowDidYouAboutUsLangs { get; set; }
        public DbSet<IPOrganizationPrice> iPOrganizationPrices { get; set; }
        public DbSet<IPOrganizationSalesAndDiscount> iPOrganizationSalesAndDiscounts { get; set; }
        public DbSet<IPPhotosFiles> IPPhotosFilies { get; set; }
        public DbSet<OrderExecutor> orderExecutors { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationLang> OrganizationLangs { get; set; }
        public DbSet<PriceCategoryAndSpecialization> priceCategoryAndSpecializations { get; set; }
        public DbSet<PriceCategoryAndSpecializationType> categoryAndSpecializationTypes { get; set; }
        public DbSet<SendBonus> SendBonuses { get; set; }
        public DbSet<TransitionExecutorCost> transitionExecutorCosts { get; set; }
        public DbSet<CallToClient> CallToClients { get; set; }
        public DbSet<CancelOrder> CancelOrders { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<ExecutorPhone> ExecutorPhonies { get; set; }
        public DbSet<ClientPhone> ClientPhones { get; set; }
        public DbSet<PhoneCheckingCode> phoneCheckingCodes { get; set; }
        public DbSet<BookmarkOrder> BookmarkOrders { get; set; }
        public DbSet<BookmarkExecutor> BookmarkExecutors { get; set; }
        public DbSet<PopulationCategory> populationCategories { get; set; }
        public DbSet<PopulationCategoryLangs> populationCategoryLangs { get; set; }
        public DbSet<PopulationQuestion> populationQuestions { get; set; }
        public DbSet<PopulationQuestionLangs> PopulationQuestionLangs { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<UserAgreement> userAgreements { get; set; }
    }
}