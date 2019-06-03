using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Controllers.WebApi
{
    public class PhoneVerifyController : ApiController
    {
        // GET: api/PhoneVerify
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PhoneVerify/5
        public void Get(string PhoneNumber)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ManageController manageController = new ManageController();
            AddPhoneNumberViewModel phone = new AddPhoneNumberViewModel()
            {
                Number = PhoneNumber
            };
            manageController.AddPhoneNumber(phone);
        }

        // POST: api/PhoneVerify
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PhoneVerify/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PhoneVerify/5
        public void Delete(int id)
        {
        }
    }
}
