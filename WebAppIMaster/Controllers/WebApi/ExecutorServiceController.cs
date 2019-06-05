 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiModel;
using WebAppIMaster.Models.WebApiService;
using static WebAppIMaster.Models.WebApiModel.ExecutorServiceMdl;

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
        public void SendCheckingCodeForUpdatePhoneNumber( string newPhoneNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            repository.SendCheckingCodeForUpdatePhoneNumber(newPhoneNumber);
        }

        // GET: api/ExecutorService/5
        public ExecutorProfile GetById(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetById(id);
            return model;
        }

        // GET: api/ExecutorService/5
        public ExecutorProfile GetByPhoneNumber( string phoneNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ExecutorService repository = new ExecutorService(db);
            var model = repository.GetByPhoneNumber(phoneNumber);
            return model;
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
