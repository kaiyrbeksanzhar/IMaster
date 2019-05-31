using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class ExecutorService : IExecutorService
    {
        public ExecutorMdl.ExecutorProfile GetById( string id )
        {
            throw new NotImplementedException();
        }

        public ExecutorMdl.ExecutorProfile GetByPhoneNumber( string phoneNumber )
        {
            throw new NotImplementedException();
        }

        public string Register( ExecutorMdl.ExecutorRegister item )
        {
            throw new NotImplementedException();
        }

        public void SendCheckingCodeForUpdatePhoneNumber( string newPhoneNumber )
        {
            throw new NotImplementedException();
        }

        public bool UpdatePhoneNumber( string executorId, string newPhoneNumber, string checkingCode )
        {
            throw new NotImplementedException();
        }

        public void UpdatePhotoFiles( Dictionary<byte[], string> actualPhotoFiles )
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile( ExecutorMdl.ExecutorProfileEdit item )
        {
            throw new NotImplementedException();
        }

        public void UpdateServices( List<ExecutorMdl.ExecutiveService> actualServices )
        {
            throw new NotImplementedException();
        }

        public void UpdateType( ExecutorMdl.ExecutorTypeEdit item )
        {
            throw new NotImplementedException();
        }
    }
}