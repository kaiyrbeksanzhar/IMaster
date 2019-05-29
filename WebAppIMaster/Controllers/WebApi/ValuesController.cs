using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppIMaster.Controllers
{
    /// <summary>
    /// Looks up some data by ID.
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Возращает полностью таблицу
        /// </summary>
        /// <param>Параметр нет</param>
        // GET: api/Values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// id  передайте возращает один элемент
        /// </summary>
        /// <param name="id">The ID of the data.</param>
        // GET: api/Values/5
        public string Get( int id )
        {
            return "value";
        }
    }
}
