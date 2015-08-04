using System;
using MessageQueue.Api.Models;
using MessageQueue.Api.Services.Details;
using MessageQueue.Api.Services.Interface;

namespace MessageQueue.Api.Services.Implementation
{
	public class JobQueueService : IJobQueueService
	{
		private MyDbContext MyDbContext;

		public JobQueueService(MyDbContext myDbContext)
		{
			this.MyDbContext = myDbContext;
		}

		public JobDetail CreateJob(JobDetail detail)
		{
			var job = new Job()
			{
				Id = Guid.NewGuid(),
				RequestDateTime = detail.RequestDateTime,
				CompletedDateTime = detail.CompletedDateTime
			};

			MyDbContext.Jobs.Add(job);
			MyDbContext.SaveChanges();

			detail.Id = job.Id;
			detail.ProcessResult.Messages.Add("Job Created.");

			return detail;

		}
	}
}