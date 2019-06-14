using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;
using static WebAppIMaster.Models.WebApiModel.ExecutorOrderMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IExecutorOrderService 
    {
        List<ExecutorOrderItem> GetItemList( List<int> CategoryIds, List<int> SpecializationIds );
        ExecutorOrderDetails GetDetails( int orderId );
        void Response( string executorId, int orderId, string responseComment );
        void CallToClient( string executorId, int orderId );
        void CancelOrder( string executorId, int orderId, OrderCancelType cancelType );
        void AddBookmark(string userId, int orderId );
        void RemoveBookmark(string  userId, int orderId );

    }
}