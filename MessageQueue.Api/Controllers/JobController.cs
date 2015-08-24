using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessageQueue.Api.Services.Details;
using MessageQueue.Api.Services.Interface;
using MessageQueue.Common.Model;

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
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/Job
        public IHttpActionResult Post(JobDetail jobDetail)
        {
			jobDetail.RequestDateTime = DateTime.Now;

			jobDetail = jobQueueService.CreateJob(jobDetail, Url.Link("DefaultApi", new { controller = "Process", action = "DoSomething" }),
				Url.Link("DefaultApi", new { controller = "Job", action = "UpdateJobResultStatus" }), Url.Link("DefaultApi", new { controller = "job", action = "UpdateJobProcessStatus" }));

	        if (jobDetail.ProcessResult.HasError())
	        {
		         return BadRequest(string.Join(";", jobDetail.ProcessResult.Errors));
	        }

	        return Ok<JobDetail>(jobDetail);
        }

        // PUT: api/Job/5
		public void Put(string id, [FromBody]string value)
        {
        }

        // DELETE: api/Job/5
		public void Delete(string id)
        {
        }

		[HttpPost]
		[HttpPut]
	    public IHttpActionResult UpdateJobResultStatus(UpdateJobResultStatusModel updateJobResultStatusModel)
		{
			var processResult = jobQueueService.UpdateJobResultStatus(updateJobResultStatusModel);
			if (processResult.HasError())
			{
				return BadRequest(string.Join(";", processResult.Errors));
			}

		    return Ok(processResult);
	    }
		
		[HttpPost]
		[HttpPut]
		public IHttpActionResult UpdateJobProcessStatus(UpdateJobProcessStatusModel updateJobProcessStatusModel)
	    {
			var processResult = jobQueueService.UpdateJobProcessStatus(updateJobProcessStatusModel);
			if (processResult.HasError())
			{
				return BadRequest(string.Join(";", processResult.Errors));
			}
		    return Ok(processResult);
	    }

    }
}
