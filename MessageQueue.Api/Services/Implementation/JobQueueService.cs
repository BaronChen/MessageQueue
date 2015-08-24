using System;
using System.Security.Policy;
using MessageQueue.Api.Models;
using MessageQueue.Api.Services.Details;
using MessageQueue.Api.Services.Interface;
using MessageQueue.Common.Model;
using MessageQueue.MessageHelper.Model;
using MessageQueue.MessageHelper.Sender;

namespace MessageQueue.Api.Services.Implementation
{
	public class JobQueueService : IJobQueueService
	{
		private MyDbContext MyDbContext;

		private const string QueueName = "testqueue";

		public JobQueueService(MyDbContext myDbContext)
		{
			this.MyDbContext = myDbContext;

		}

		public JobDetail CreateJob(JobDetail detail, string doJobUrl, string updateStatusUrl, string completeJobUrl)
		{
			var job = new Job()
			{
				Id = Guid.NewGuid(),
				RequestDateTime = detail.RequestDateTime,
				CompletedDateTime = detail.CompletedDateTime
			};

			var messageSender = new MessageSender("testqueue");


			var callBackMessage = new CallBackMessage()
			{
				Url = doJobUrl,
				PlayLoad = "abcd",
				JobId = job.Id.ToString(),
				UpdateJobStatusUrl = updateStatusUrl,
				CompleteJobUrl = completeJobUrl
			};

			try
			{
				messageSender.SendMessage(callBackMessage, job.Id.ToString());

				MyDbContext.Jobs.Add(job);
				MyDbContext.SaveChanges();

				detail.Id = job.Id;
				detail.ProcessResult.Messages.Add("Job Created.");

				return detail;
			}
			catch (Exception e)
			{
				detail.ProcessResult.Errors.Add("Fail to crete job: " + e.Message +"\n" + e.StackTrace);
				return detail;
			}
		}

		public ProcessResult UpdateJobResultStatus(UpdateJobResultStatusModel model)
		{
			var result = new ProcessResult();

			try
			{
				var job = MyDbContext.Jobs.Find(new Guid(model.Id));

				if (job == null)
				{
					result.Errors.Add("Invalid Job Id");
					return result;
				}

				job.ResultStatus = model.ResultStatus;

				MyDbContext.SaveChanges();

			}
			catch (Exception e)
			{
				result.Errors.Add("Fail to update job: " + e.Message);
			}

			return result;

		}

		public ProcessResult UpdateJobProcessStatus(UpdateJobProcessStatusModel model)
		{
			var result = new ProcessResult();

			try
			{
				var job = MyDbContext.Jobs.Find(new Guid(model.Id));

				if (job == null)
				{
					result.Errors.Add("Invalid Job Id");
					return result;
				}

				job.ProcessStatus = model.ProcessStatus;
				job.CompletedDateTime = DateTime.Now;

				MyDbContext.SaveChanges();

			}
			catch (Exception e)
			{
				result.Errors.Add("Fail to update job: " + e.Message);
			}

			return result;
		}
	}
}