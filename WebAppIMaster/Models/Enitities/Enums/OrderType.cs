﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities.Enums
{
    public enum OrderType
    {
        Moderation = 1,
        Published,
        Processing,
        Closed,
        Rejected
    }
}