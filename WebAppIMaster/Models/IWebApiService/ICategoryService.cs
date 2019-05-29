using WebAppIMaster.Models.WebApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface ICategoryService
    {
        List<CategoryServiceMdl> GetList();
        CategoryServiceMdl Get(int id);

    }
}