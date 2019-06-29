using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IClientOrder
    {
        ClientOrderDetailsView GetClientOrderDetailsView(int id);
        ClientOrderItemView GetClientOrderItemView(int id);
        List<ClientOrderItemView> GetList();
        int Create(ClientOrderCreate item);
        List<ClientOrderItemView> GetListForClient(string clientId);
        List<ClientOrderItemView> GetListForExecutor(string executorId);
        ClientCommonOrderDetailsView GetClientCommonOrderDetailsView(int orderId);
        void SendPhotoToOrder(string url, int orderId);
        bool ClientMyOrderFinish(ClientMyOrderFinish item);
        bool CancelClientMyOrder(int orderId, CancelReason cancelReason);
    }

    public class ClientMyOrderFinish
    {
        public int OrderId { get; set; }
        public FinishReason FinishReason { get; set; }
        public int StarsCountOf5 { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewedAt { get; set; }
    }

    public enum FinishReason
    {
        ExecutorDoneOrder = 1,
        ExecutorDontPhone,
        ExecutorCancelOrder,
        ExecutorCantDone
    }

    public enum CancelReason
    {
        ExecutorHimselfContactedMe = 1,
        IFoundExecutorThroughAnotherService,
        OrderNoRelevant,
        Another,
    }
}