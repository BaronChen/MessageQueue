using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageQueue.Api.Services.Details;

namespace MessageQueue.Api.Services.Interface
{
	public interface IJobQueueService
	{
		JobDetail CreateJob(JobDetail detail);
	}
}