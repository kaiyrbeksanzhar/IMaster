using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface INewsItemService
    {
        NewsItemMdl Get(int id);
        List<NewsItemMdl> GetList();
        List<NewsItemMdl> GetSearch(String searchText);
    }
}