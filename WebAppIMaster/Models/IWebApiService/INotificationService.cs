using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.NotificationItemViewMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface INotificationService
    {
        NotificationItemView Get( int id );
        List<NotificationItemView> GetList();
    }
}