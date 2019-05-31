using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class City
    {
        public City()
        {
            Langs = new HashSet<CityLang>();
            Users = new HashSet<UserProfile>();
            CustomerOrders = new HashSet<CustomerOrder>();
            Organizations = new HashSet<Organization>();
            Customers = new HashSet<Customer>();
            Executors = new HashSet<Executor>();
        }

        public int Id { get; set; }

        public string Latitude { get; set; }
        public string Longitudey{ get; set; }
        public int Preority { get; set; }

        public ICollection<CityLang> Langs { get; set; }
        public ICollection<UserProfile> Users { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<Customer>  Customers { get; set; }
        public ICollection<Executor> Executors { get; set; }
    }
}
