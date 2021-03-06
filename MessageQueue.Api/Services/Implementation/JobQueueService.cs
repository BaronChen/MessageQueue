﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using MessageQueue.Api.Models;
using MessageQueue.Api.Services.Details;
using MessageQueue.Api.Services.Interface;
using MessageQueue.Common.Enum;
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

			job = MyDbContext.Jobs.Add(job);
			MyDbContext.SaveChanges();

			try
			{
				var messageSender = new MessageSender("testqueue");
				var callBackMessage = new CallBackMessage()
				{
					Url = doJobUrl,
					PlayLoad = "abcd",
					JobId = job.Id.ToString(),
					UpdateJobStatusUrl = updateStatusUrl,
					CompleteJobUrl = completeJobUrl
				};
				messageSender.SendMessage(callBackMessage, job.Id.ToString());

				detail.Id = job.Id;
				detail.ProcessResult.Messages.Add("Job Created.");

				return detail;
			}
			catch (Exception e)
			{
				detail.ProcessResult.Errors.Add("Fail to crete job: " + e.Message +"\n" + e.StackTrace);
				job.ProcessStatus = ProcessStatus.FailToPublish;
				MyDbContext.SaveChanges();
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

		public List<JobDetail> QueryJobsStatus(List<string> jobIds)
		{
			var jobs = MyDbContext.Jobs.Where(x => jobIds.Contains(x.Id.ToString()));

			var details = jobs.Select(MapToDetail).ToList();

			return details;
		}

		private JobDetail MapToDetail(Job job)
		{
			return new JobDetail()
			{
				Id = job.Id,
				CompletedDateTime = job.CompletedDateTime,
				ProcessStatus = job.ProcessStatus,
				RequestDateTime = job.RequestDateTime,
				ResultStatus = job.ResultStatus
			};
		}
	}
}