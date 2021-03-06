﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.ExecutorServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IExecutorService
    {
        List<ExecutorItem> GetItemList();
        List<ExecutorItem> GetItemListSuitableForOrder( int orderId );
        List<ExecutorItem> GetItemListForSpecialization( int specializationId );
        string Register( ExecutorRegister item );
        ExecutorProfile GetById( string id );
        ExecutorProfile GetByPhoneNumber( string phoneNumber );
        void UpdateProfile( ExecutorProfileEdit item );
        void UpdateType( ExecutorTypeEdit item ,string executorId );
        void SendCheckingCodeForUpdatePhoneNumber( string newPhoneNumber );
        bool UpdatePhoneNumber( string executorId, string newPhoneNumber, string checkingCode );
        void UpdatePhotoFiles( string executorId, Dictionary<byte[], String> actualPhotoFiles );
        void UpdateServices( List<Models.WebApiModel.ExecutorServiceMdl.ExecutiveService> actualServices, string executorId );
    }
}