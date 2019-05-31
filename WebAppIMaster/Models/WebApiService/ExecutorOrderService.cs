using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities.Enums;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class ExecutorOrderService : IExecutorOrderService
    {
        public void CallToClient( string executorId, int orderId )
        {
            throw new NotImplementedException();
        }

        public void CancelOrder( string executorId, int orderId, OrderCancelType cancelType )
        {
            throw new NotImplementedException();
        }

        public ExecutorOrderMdl.ExecutorOrderDetails GetDetails( int orderId )
        {
            throw new NotImplementedException();
        }

        public List<ExecutorOrderMdl.ExecutorOrderItem> GetItemList( List<int> CategoryIds, List<int> SpecializationIds )
        {
            throw new NotImplementedException();
        }

        public void Response( string executorId, int orderId, string responseComment )
        {
            throw new NotImplementedException();
        }
    }
}