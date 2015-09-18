using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageQueue.Api.Models;
using MessageQueue.Api.Services.Details;
using MessageQueue.Common.Model;

namespace MessageQueue.Api.Services.Interface
{
	public interface IJobQueueService
	{
		JobDetail CreateJob(JobDetail detail, string doJobUrl, string updateStatusUrl, string completeJobUrl);

		ProcessResult UpdateJobResultStatus(UpdateJobResultStatusModel model);
		ProcessResult UpdateJobProcessStatus(UpdateJobProcessStatusModel model);

		List<JobDetail> QueryJobsStatus(List<string> jobIds);
	}
}