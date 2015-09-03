using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessageQueue.Api.Services.Details;
using MessageQueue.Api.Services.Interface;
using MessageQueue.Common.Enum;

namespace MessageQueue.Api.Controllers
{
    public class ProcessController : ApiController
    {
		 private readonly IJobQueueService jobQueueService;

	    public ProcessController(IJobQueueService jobQueueService)
	    {
		    this.jobQueueService = jobQueueService;
	    }

	    [HttpPost]
	    public IHttpActionResult DoSomething()
	    {
			return Ok();
	    }

	    [HttpPost]
	    public IHttpActionResult ProcessTask()
	    {
			var jobDetail = new JobDetail()
			{
				RequestDateTime = DateTime.Now,
				ResultStatus = ResultStatus.Requested,
				ProcessStatus = ProcessStatus.Processing
			};

			jobDetail = jobQueueService.CreateJob(jobDetail, Url.Link("DefaultApi", new { controller = "Process", action = "DoSomething" }),
				Url.Link("DefaultApi", new { controller = "Job", action = "UpdateJobResultStatus" }), Url.Link("DefaultApi", new { controller = "job", action = "UpdateJobProcessStatus" }));

			if (jobDetail.ProcessResult.HasError())
			{
				return BadRequest(string.Join(";", jobDetail.ProcessResult.Errors));
			}

			return Ok(new {message = "Process Task Created."});
	    }

    }
}
