using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MessageQueue.Api.Controllers
{
    public class ProcessController : ApiController
    {

	    [HttpPost]
	    public IHttpActionResult DoSomething()
	    {
			return Ok();
	    }
		
    }
}
