using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppIMaster.Models.WebApiModel.ExecutorServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IExecutorService
    {
        string Register( ExecutorRegister item );
        ExecutorProfile GetById( string id );
        ExecutorProfile GetByPhoneNumber( string phoneNumber );
        void UpdateProfile( ExecutorProfileEdit item );
        void UpdateType( ExecutorTypeEdit item );
        void SendCheckingCodeForUpdatePhoneNumber( string newPhoneNumber );
        bool UpdatePhoneNumber( string executorId, string newPhoneNumber, string checkingCode );
        void UpdatePhotoFiles( Dictionary<byte[], String> actualPhotoFiles );
        void UpdateServices( List<Models.WebApiModel.ExecutorServiceMdl.ExecutiveService> actualServices );
    }
}