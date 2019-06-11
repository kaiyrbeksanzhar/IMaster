using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiService;
using WebAppIMaster.Providers;

namespace WebAppIMaster.Controllers.WebApi
{

    public class VerifyPhoneController : ApiController
    {

        // GET: api/VerifyPhone/5
        public bool GetChekingCode(string PhoneNumber)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            VerifyService repository = new VerifyService(db);
            bool result = repository.PhoneVerify(PhoneNumber);
            return result;
        }


    }
}
