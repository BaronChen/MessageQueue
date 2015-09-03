using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageQueue.Common.Enum;
using MessageQueue.Common.Model;
using MessageQueue.MessageHelper.Model;
using Newtonsoft.Json;

namespace MessageQueue.MessageHelper.MessageProcessor
{
	public class CallBackMessageProcessor
	{

		private EventLog _consumerEventLog;

		public CallBackMessageProcessor(EventLog consumerEventLog)
		{
			_consumerEventLog = consumerEventLog;
		}


		public void ProcessCallBackMessage(CallBackMessage message)
		{
			var result = PostManager.PostRequest(message.Url, message.PlayLoad);

			if (result.Success)
			{
				UpdateJobStatus(message.JobId, message.UpdateJobStatusUrl, ResultStatus.Success);
			}
			else
			{
				UpdateJobStatus(message.JobId, message.UpdateJobStatusUrl, ResultStatus.Error);
				var msg = GetBadResultMessage(result.Result);

				_consumerEventLog.WriteEntry("Fail to update job status (" + message.JobId +"): "+msg);
			}

			CompleteJob(message.JobId, message.CompleteJobUrl);
		}

		private void UpdateJobStatus(string jobId, string url, ResultStatus status)
		{
			var updateJobResultStatus = new UpdateJobResultStatusModel()
			{
				ResultStatus = status,
				Id = jobId
			};

			var updateResult = PostManager.PostRequest(url, JsonConvert.SerializeObject(updateJobResultStatus));

			if (!updateResult.Success)
			{
				var msg = GetBadResultMessage(updateResult.Result);

				_consumerEventLog.WriteEntry("Fail to update job status (" + jobId + "): " + msg);
			}
			else
			{
				_consumerEventLog.WriteEntry("Successful to update job status (" + jobId + ").");
			}
		}

		private void CompleteJob(string jobId, string url)
		{
			var compleJobModel = new UpdateJobProcessStatusModel()
			{
				Id = jobId,
				ProcessStatus = ProcessStatus.Completed
			};

			var completeResult = PostManager.PostRequest(url, JsonConvert.SerializeObject(compleJobModel));

			if (!completeResult.Success)
			{
				var msg = GetBadResultMessage(completeResult.Result);

				_consumerEventLog.WriteEntry("Fail to complete jobs (" + jobId + "): " + msg);
			}
			else
			{
				_consumerEventLog.WriteEntry("Successful to complete job (" + jobId + ").");
			}
		}

		private string GetBadResultMessage(string str)
		{
			try
			{
				var result = JsonConvert.DeserializeObject<ProcessResult>(str);
				if (!result.Errors.Any())
				{
					return str;
				}
				return string.Join(";", result.Errors);
			}
			catch (Exception e)
			{
				return str;
			}

		}


	}
}
