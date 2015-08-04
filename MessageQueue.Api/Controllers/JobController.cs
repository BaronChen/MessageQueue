using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessageQueue.Api.Services.Details;
using MessageQueue.Api.Services.Interface;

namespace MessageQueue.Api.Controllers
{
    public class JobController : ApiController
    {

	    private readonly IJobQueueService jobQueueService;

	    public JobController(IJobQueueService jobQueueService)
	    {
		    this.jobQueueService = jobQueueService;
	    }

        // GET: api/Job
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Job/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Job
        public IHttpActionResult Post(JobDetail jobDetail)
        {
	        jobDetail = jobQueueService.CreateJob(jobDetail);

	        return Ok<JobDetail>(jobDetail);
        }

        // PUT: api/Job/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Job/5
        public void Delete(int id)
        {
        }
    }
}
