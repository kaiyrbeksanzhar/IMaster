using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities.Enums
{
    public enum Estimate
    {
        [Display(Name = "Помогло")]
        like = 1,
        [Display(Name = "Не помогло")]
        dislike
    }
}