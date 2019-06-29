using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.ClientExecutorServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IClientExecutorService
    {
        List<ExecutorItem> GetItemList();
        List<ExecutorItem> GetItemListForCategory( int categoryId );
        List<ExecutorItem> GetItemListForSpecialty( int specialtyId );
        ExecutorDetails GetDetails( string executorId );
        List<ExecutorResponse> GetResponseList( string clientId, int lastResponseId = -1);
        void AddExecutorToOrder( string clientId, int orderId, string executorId );
        void CancelExecutorResponse( string clientId, int orderId, string executorId );
        List<ExecutorResponse> GetResponseListForOrder(int orderId);
    }
}