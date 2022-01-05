using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class TestController : ApiController
    {
        public IHttpActionResult GetTest()
        {
            var Arr = new string[] {
                "POS Application",
                "Version - 2021.09.29",
                "Developed by ASTEN LABS",
                "Website: http://www.astenlabs.com/"
            };
            return Ok(Arr);

        }
    }
}
