using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.Enitities.Enums
{
    public enum TypeMessage
    {
        [Display(Name = "Жалоба")]
        appeal = 1,
        [Display(Name = "Вопрос")]
        question
    }
}