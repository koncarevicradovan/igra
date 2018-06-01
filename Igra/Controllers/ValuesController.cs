using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Igra.DAL;
using Igra.VM;

namespace Igra.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        private IgraContext db = new IgraContext();
        // GET api/values
        public IEnumerable<string> Get()
        {
            int a = db.Users.Count();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]LoginRequest loginRequest)
        {
           
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
