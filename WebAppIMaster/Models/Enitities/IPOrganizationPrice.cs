using WebAppIMaster.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class IPOrganizationPrice
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public TypePriceOrganization Type { get; set; }

        public string FromPrice { get; set; }
        public string ToPrice { get; set; }

        public OrganizationIpTarif TarifType { get; set; }

        public virtual Organization Organization { get; set; }
        public int OrganizationId { get; set; }
    }
}
