using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyFirstWebApi.Controllers
{
    public class SampleApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get_Data()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public void Post_Data([FromBody]string value)
        {
            //Add your logic
        }

        // PUT api/values/5
        [HttpPut]
        public void Put_Data(int id, [FromBody]string value)
        {
            //Add your logic
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete_Data(int id)
        {
            //Add your logic
        }

     
        
    }
}
