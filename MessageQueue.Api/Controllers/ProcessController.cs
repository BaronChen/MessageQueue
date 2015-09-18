using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
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
			Thread.Sleep(1000);
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

			return Ok(new {jobId = jobDetail.Id, message = "Process Task Created."});
	    }

	    [HttpPost]
		[HttpPut]
	    public IHttpActionResult QueryProcessTaskStatus(JobsInQueue jobsInQueue)
	    {
		    var details = jobQueueService.QueryJobsStatus(jobsInQueue.CreatedJobIds);

		    jobsInQueue.CompleteJobIds = details.Where(x => x.ProcessStatus == ProcessStatus.Completed).Select( x=> x.Id.ToString()).ToList();
		    jobsInQueue.ErrorJobIds = details.Where(x => x.ProcessStatus == ProcessStatus.FailToPublish || x.ResultStatus == ResultStatus.Error).Select( x=> x.Id.ToString()).ToList();
		    jobsInQueue.ProcessingJobIds = details.Where(x => x.ProcessStatus == ProcessStatus.Processing).Select( x=> x.Id.ToString()).ToList();
		    jobsInQueue.SuccessJobIds = details.Where(x => x.ResultStatus == ResultStatus.Success).Select( x=> x.Id.ToString()).ToList();

		    return Ok(jobsInQueue);
	    }



    }
}
