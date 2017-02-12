using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    public class RequestController : ApiController
    {
        // GET: api/Request
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Request/5
        public Request Get(int id)
        {
            Request request = new Request();

            request.Id = id;
            request.Name = "";
            request.Creator = "";
            request.Approver = "";
            return request;
        }

        // POST: api/Request
        public void Post([FromBody]Request value)
        {
        }

        // PUT: api/Request/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Request/5
        public void Delete(int id)
        {
        }
    }
}
