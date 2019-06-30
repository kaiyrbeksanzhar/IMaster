using System.Collections.Generic;
using WebAppIMaster.Models.Enitities.Enums;
using static WebAppIMaster.Models.WebApiModel.ExecutorOrderMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IExecutorOrderService 
    {
        List<ExecutorOrderItem> GetItemList(List<int> CategoryIds, List<int> SpecializationIds, string userId);
        ExecutorOrderDetails GetDetails( int orderId, string userId );
        void Response( string executorId, int orderId, string responseComment );
        void CallToClient( string executorId, int orderId );
        void CancelOrder( string executorId, int orderId, string cancelReason);
        void AddBookmark(string userId, int orderId );
        void RemoveBookmark(string  userId, int orderId );

    }
}