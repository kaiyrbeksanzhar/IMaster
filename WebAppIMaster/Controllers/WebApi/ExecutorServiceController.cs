 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;

namespace WebAppIMaster.Controllers.WebApi
{
    public class ExecutorServiceController : ApiController
    {
        // GET: api/ExecutorService
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExecutorService/5
        public void Get(string newPhoneNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.SendCheckingCodeForUpdatePhoneNumber(newPhoneNumber);
        }

        // POST: api/ExecutorService
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ExecutorService/5
        public string Put([FromBody]ExecutorServiceMdl.ExecutorRegister item )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            string id = repository.Register(item);
            return id;
        }

        // DELETE: api/ExecutorService/5
        public void Delete(int id)
        {
        }
    }
}
