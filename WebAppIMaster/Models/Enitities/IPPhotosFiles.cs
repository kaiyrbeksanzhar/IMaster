﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppIMaster.Models
{
    public class IPPhotosFiles
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }

        public virtual Organization Organization { get; set; }
        public int OrganizationId { get; set; }
    }
}
